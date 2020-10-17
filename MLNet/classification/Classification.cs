using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Vision;
using System.Drawing;

namespace MLNet.classification
{
    public class Classification
    {
        private MLContext mlContext;

        private ITransformer trainedModel;

        public void Process()
        {
            Console.WriteLine();
            Console.WriteLine("Initializing Image-classification..");
            /**
             *  Putanje do dataseta, lokacija na koju se smestaju bottleneck vrednosti i .pb modela
             */
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            var workspaceRelativePath = Path.Combine(projectDirectory, "classification/workspace");
            var assetsRelativePath = Path.Combine(projectDirectory, "dataset/seg_train/seg_train");

            /**
             * MLContext klasa je polazna tacka inicijalizacije
             * kreira novo ML.NET okruzenje i perzistira model koji se trenira
             */
            mlContext = new MLContext();

            Console.WriteLine("Loading and shuffling dataset..");

            /**
             * Podaci se ucitavaju redosledom kojim su u direktorijumima
             * da bi se balansirao ulaz treba odradi shuffling nad njima
             */
            IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: assetsRelativePath, useFolderNameAsLabel: true);

            IDataView imageData = mlContext.Data.LoadFromEnumerable(images);

            IDataView shuffledData = mlContext.Data.ShuffleRows(imageData);

            /**
             * Model ocekuje da ulazi budu u numerickom obliku -> LabelAsKey
             * EstimatorChain se pravi od MapValueToKey i LoadRawImageBytes transofrmacija
             */
            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "Label",
                    outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                    outputColumnName: "Image",
                    imageFolder: assetsRelativePath,
                    inputColumnName: "ImagePath"));

            /**
             * Fit metoda za dosatvljanje podataka preprocessingPipeline-u praceno Transform metodom
             * koja vraca IDataView koji sadrzi pred-procesirane podatke
             */
            IDataView preProcessedData = preprocessingPipeline
                                .Fit(shuffledData)
                                .Transform(shuffledData);

            Console.WriteLine("Splitting dataset to train/vaild subsets..");

            /**
             *  Kvalitet dobijenih procena se meri na osnovu validacionog podskupa
             *  Pred-procesirani podaci se prema tome dele
             *  70% se koristi za treniranje
             *  30% za validaciju
             */
            TrainTestData trainSplit = mlContext.Data.TrainTestSplit(data: preProcessedData, testFraction: 0.3);
            TrainTestData validationTestSplit = mlContext.Data.TrainTestSplit(trainSplit.TestSet);

            IDataView trainSet = trainSplit.TrainSet;
            IDataView validationSet = validationTestSplit.TrainSet;
            IDataView testSet = validationTestSplit.TestSet;

            /**
             * FeatureColumnName je kolona koriscena kao ulazna nad modelom
             * LabelColumnName je kolona za vradnost koja se predvidja
             */
            var classifierOptions = new ImageClassificationTrainer.Options()
            {
                FeatureColumnName = "Image",
                LabelColumnName = "LabelAsKey",
                ValidationSet = validationSet,
                Arch = ImageClassificationTrainer.Architecture.ResnetV2101,
                MetricsCallback = (metrics) => Console.WriteLine(metrics),
                TestOnTrainSet = false,
                ReuseTrainSetBottleneckCachedValues = true,
                ReuseValidationSetBottleneckCachedValues = true,
                WorkspacePath = workspaceRelativePath
            };

            /**
             * Image Classification API se koristi za treniranje modela. Zatim, kodirana labela
             * PredictedLabel se vraca u originalnu kategoricku vrednost
             * koriscenjem MapKeyToValue transformacije
             */
            var trainingPipeline = mlContext.MulticlassClassification.Trainers.ImageClassification(classifierOptions)
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Console.WriteLine("Beginning to train the model..");

            /**
             * Treniranje modela
             */
            trainedModel = trainingPipeline.Fit(trainSet);

            ClassifySingleImage(testSet);

            ClassifyImages(testSet);

            Console.WriteLine("End of Image-clasification..");
            Console.Out.Flush();
        }

        public void ProcessTestInBatch()
        {
            Console.WriteLine();
            Console.WriteLine("Evaluating Image-classification..");
            Console.Out.Flush();
            /**
             *  Putanje do dataseta, lokacija na koju se smestaju bottleneck vrednosti i .pb modela
             */
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            var workspaceRelativePath = Path.Combine(projectDirectory, "classification/workspace");
            var assetsRelativePath = Path.Combine(projectDirectory, "dataset/seg_test/seg_test");

            IEnumerable<ImageData> images = LoadImagesFromDirectory(folder: assetsRelativePath, useFolderNameAsLabel: true);

            IDataView imageData = mlContext.Data.LoadFromEnumerable(images);
            var preprocessingPipeline = mlContext.Transforms.Conversion.MapValueToKey(
                    inputColumnName: "Label",
                    outputColumnName: "LabelAsKey")
                .Append(mlContext.Transforms.LoadRawImageBytes(
                    outputColumnName: "Image",
                    imageFolder: assetsRelativePath,
                    inputColumnName: "ImagePath"));
            IDataView preProcessedData = preprocessingPipeline
                                .Fit(imageData)
                                .Transform(imageData);

            IDataView predictionData = trainedModel.Transform(preProcessedData);

            IEnumerable<ModelOutput> predictions = mlContext.Data.CreateEnumerable<ModelOutput>(predictionData, reuseRowObject: true);

            double numTested = 0;
            double numPassed = 0;

            foreach (var prediction in predictions)
            {
                OutputPrediction(prediction);
                Console.Out.Flush();
                if (prediction.PredictedLabel.Equals(prediction.Label))
                {
                    numPassed++;
                }
                numTested++;
            }

            double perc = (numPassed / numTested) * 100;

            Console.WriteLine($"Evaluation success percentage is {perc}");
            Console.WriteLine("End of evaluation..");
            Console.Out.Flush();
        }

        /**
         * ClassifySingleImage se koristi za klasifikaciju jedne slike
         */

        public void ClassifySingleImage(IDataView data)
        {
            /**
             * PredictionEngine radi predikciju nad jednom instancom podataka
             */
            PredictionEngine<ModelInput, ModelOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

            ModelInput image = mlContext.Data.CreateEnumerable<ModelInput>(data, reuseRowObject: true).First();

            ModelOutput prediction = predictionEngine.Predict(image);

            Console.WriteLine("Classifying an image..");
            OutputPrediction(prediction);
        }

        /**
         * ClassifyExternalImage se koristi za klasifikaciju slike iz eksternog fajla
         */

        public void ClassifyExternalImage(Bitmap image)
        {
            PredictionEngine<ModelInput, ModelOutput> predictionEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(trainedModel);

            byte[] imageData;

            using (var imageMemoryStream = new MemoryStream())
            {
                image.Save(imageMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageData = imageMemoryStream.ToArray();
            }

            var imageInputData = new ModelInput() { Image = imageData, ImagePath = "External", Label = "Unknown" };

            ModelOutput prediction = predictionEngine.Predict(imageInputData);

            Console.WriteLine();
            Console.WriteLine("Classifying external image..");
            OutputPrediction(prediction);
        }

        /**
         * ClassifyImages se koristi za klasifikaciju vise slika
        */

        public void ClassifyImages(IDataView data)
        {
            IDataView predictionData = trainedModel.Transform(data);

            IEnumerable<ModelOutput> predictions = mlContext.Data.CreateEnumerable<ModelOutput>(predictionData, reuseRowObject: true).Take(10);

            Console.WriteLine("Classifying multiple images..");
            foreach (var prediction in predictions)
            {
                OutputPrediction(prediction);
                Console.Out.Flush();
            }
        }

        private void OutputPrediction(ModelOutput prediction)
        {
            string imageName = Path.GetFileName(prediction.ImagePath);
            Console.WriteLine($"Image: {imageName} | Actual Value: {prediction.Label} | Predicted Value: {prediction.PredictedLabel}");
        }

        /**
         * Slike se na pocetku nalaze u pod-direktorijumima
         * Pre ucitavanja treba preformatirati ulaz u listu ImageData objekata
         */

        public static IEnumerable<ImageData> LoadImagesFromDirectory(string folder, bool useFolderNameAsLabel = true)
        {
            var files = Directory.GetFiles(folder, "*",
                searchOption: SearchOption.AllDirectories);

            foreach (var file in files)
            {
                if ((Path.GetExtension(file) != ".jpg") && (Path.GetExtension(file) != ".png"))
                    continue;

                var label = Path.GetFileName(file);

                if (useFolderNameAsLabel)
                    label = Directory.GetParent(file).Name;
                else
                {
                    for (int index = 0; index < label.Length; index++)
                    {
                        if (!char.IsLetter(label[index]))
                        {
                            label = label.Substring(0, index);
                            break;
                        }
                    }
                }

                yield return new ImageData()
                {
                    ImagePath = file,
                    Label = label
                };
            }
        }
    }
}