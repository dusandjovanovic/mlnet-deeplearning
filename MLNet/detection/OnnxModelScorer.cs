using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace MLNet.detection
{
    /**
     * Klasa koja opisuje logiku 'scoringa'
     */

    internal class OnnxModelScorer
    {
        private readonly string modelLocation;
        private readonly MLContext mlContext;

        private IList<BoundingBox> _boundingBoxes = new List<BoundingBox>();

        public OnnxModelScorer(string modelLocation, MLContext mlContext)
        {
            this.modelLocation = modelLocation;
            this.mlContext = mlContext;
        }

        public struct ImageNetSettings
        {
            public const int imageHeight = 416;
            public const int imageWidth = 416;
        }

        public struct TinyYoloModelSettings
        {
            public const string ModelInput = "image";

            public const string ModelOutput = "grid";
        }

        /**
         * ML.NET pipeline-ovi treba da znaju semu ulaznih podataka pri pozivu Fit()
         * koristice se proces slican treniranju uz male razlike
         * kako nema pravog treniranja koristi se prazan IDataView
         */

        private ITransformer LoadModel(string modelLocation)
        {
            Console.WriteLine("Reading the model..");
            Console.WriteLine($"Models has image size=({ImageNetSettings.imageWidth},{ImageNetSettings.imageHeight})..");

            /**
             * prazan IDataView kako bi se uzela sema ulaznih podataka
             */
            var data = mlContext.Data.LoadFromEnumerable(new List<ImageNetData>());

            /**
             * Pipeline ima 4 transformacije:
             *  LoadImages za ucitavanje Bitmap slike
             *  ResizeImages za reskaliranje ucitane slike (ovde je to 416 x 416)
             *  ExtractPixels menja reprezentaciju piksela u numericki vektor
             *  ApplyOnnxModel ucitava ONNX model i koristi ga za score-ing
             */
            var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "image", imageFolder: "", inputColumnName: nameof(ImageNetData.ImagePath))
                            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "image", imageWidth: ImageNetSettings.imageWidth, imageHeight: ImageNetSettings.imageHeight, inputColumnName: "image"))
                            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "image"))
                            .Append(mlContext.Transforms.ApplyOnnxModel(modelFile: modelLocation, outputColumnNames: new[] { TinyYoloModelSettings.ModelOutput }, inputColumnNames: new[] { TinyYoloModelSettings.ModelInput }));

            /**
             * Instanciranje modela
             */
            var model = pipeline.Fit(data);

            return model;
        }

        /**
         * Metoda za predikciju
         */

        private IEnumerable<float[]> PredictDataUsingModel(IDataView testData, ITransformer model)
        {
            Console.WriteLine();
            Console.WriteLine("Detecting objects..");
            Console.WriteLine();

            /**
             * Transformacija metoda za 'score' podataka
             */
            IDataView scoredData = model.Transform(testData);

            /**
             * Izvlacenje predvidjenih verovatnoca
             */
            IEnumerable<float[]> probabilities = scoredData.GetColumn<float[]>(TinyYoloModelSettings.ModelOutput);

            return probabilities;
        }

        /**
         * Metoda za ucitavanje slike i izvrsenje predikcije
         */

        public IEnumerable<float[]> Score(IDataView data)
        {
            var model = LoadModel(modelLocation);

            return PredictDataUsingModel(data, model);
        }
    }
}