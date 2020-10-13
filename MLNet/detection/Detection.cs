using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Microsoft.ML;
using System.Windows.Media.Imaging;
using MLNet.common;

namespace MLNet.detection
{
    public class Detection
    {
        private MLContext mlContext;

        private OnnxModelScorer modelScorer;

        private string modelFilePath;

        public void Process()
        {
            Console.WriteLine();
            Console.WriteLine("Initializing Object-detection..");

            /**
             *  Inicijalizacija modela
             */
            var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../"));
            modelFilePath = Path.Combine(projectDirectory, "detection/model", "TinyYolo2_model.onnx");

            /**
             * MLContext klasa je polazna tacka inicijalizacije
             * kreira novo ML.NET okruzenje i perzistira model koji se trenira
             */
            mlContext = new MLContext();

            Console.WriteLine("End of Object-detection..");
            Console.Out.Flush();
        }

        public BitmapImage ProcessExternalImage(string selectedImagePath)
        {
            /**
             * Ucitavanje podataka u IDataView
             */
            var imageInputData = new ImageNetData() { ImagePath = selectedImagePath, Label = "Unknown" };
            IDataView imageDataView = mlContext.Data.LoadFromEnumerable(new[] { imageInputData });

            /**
            * Potrebna je instanca OnnxModelScorer-a kako bi se ocenio iliti
            * 'scorovao' ulaz koji se prethodno prepakovau u IDataView
            */
            modelScorer = new OnnxModelScorer(modelFilePath, mlContext);
            IEnumerable<float[]> probabilities = modelScorer.Score(imageDataView);

            OutputParser parser = new OutputParser();
            var boundingBoxes =
                probabilities
                .Select(probability => parser.ParseOutputs(probability))
                .Select(boxes => parser.FilterBoundingBoxes(boxes, 5, .5F));

            IList<BoundingBox> detectedObjects = boundingBoxes.ElementAt(0);

            LogDetectedObjects(selectedImagePath, detectedObjects);

            Bitmap bitmap = BitmapWithBoundingBox(selectedImagePath, detectedObjects);

            return ImageUtils.GetBitmapImageFromBitmap(bitmap);
        }

        /**
         * Crtanje bounding box-ova za predikcije
         */

        private static Bitmap BitmapWithBoundingBox(string inputImageLocation, IList<BoundingBox> filteredBoundingBoxes)
        {
            /**
             * Ucitavanje slike i nalazenje visine/sirine DrawBoundingBox metodom
             */
            Image image = Image.FromFile(inputImageLocation);

            var originalImageHeight = image.Height;
            var originalImageWidth = image.Width;

            /**
             * Iteracija preko svakog bounding box-a iz modela
             */
            foreach (var box in filteredBoundingBoxes)
            {
                /**
                 * Kako dimenzije box-ova odgovaraju ulaznom modelu od 416 x 416
                 * treba ih skalirati da se poklapaju stvarnoj velicini slike
                 * na kraju se reskalira slika
                 */
                var x = (uint)Math.Max(box.Dimensions.X, 0);
                var y = (uint)Math.Max(box.Dimensions.Y, 0);
                var width = (uint)Math.Min(originalImageWidth - x, box.Dimensions.Width);
                var height = (uint)Math.Min(originalImageHeight - y, box.Dimensions.Height);

                x = (uint)originalImageWidth * x / OnnxModelScorer.ImageNetSettings.imageWidth;
                y = (uint)originalImageHeight * y / OnnxModelScorer.ImageNetSettings.imageHeight;
                width = (uint)originalImageWidth * width / OnnxModelScorer.ImageNetSettings.imageWidth;
                height = (uint)originalImageHeight * height / OnnxModelScorer.ImageNetSettings.imageHeight;

                /**
                 * String templejt za tekst koji ce se naci iznad svakog bounding box-a
                 */
                string text = $"{box.Label} ({(box.Confidence * 100).ToString("0")}%)";

                using (Graphics thumbnailGraphic = Graphics.FromImage(image))
                {
                    thumbnailGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    Font drawFont = new Font("Arial", 12, FontStyle.Bold);
                    SizeF size = thumbnailGraphic.MeasureString(text, drawFont);
                    SolidBrush fontBrush = new SolidBrush(Color.Black);
                    Point atPoint = new Point((int)x, (int)y - (int)size.Height - 1);

                    Pen pen = new Pen(box.BoxColor, 3.2f);
                    SolidBrush colorBrush = new SolidBrush(box.BoxColor);

                    /**
                     * Crtanje i bojenje pravougaonika oko bounding box-a
                     * dodatno ispisivanje stringa koji ce opisati bounding box
                     */
                    thumbnailGraphic.FillRectangle(colorBrush, (int)x, (int)(y - size.Height - 1), (int)size.Width, (int)size.Height);
                    thumbnailGraphic.DrawString(text, drawFont, fontBrush, atPoint);
                    thumbnailGraphic.DrawRectangle(pen, x, y, width, height);
                }
            }

            return new Bitmap(image);
        }

        /**
         * Logovanje svih detektovanih objekata zajedno sa score/confidence vrednoscu
         */

        private static void LogDetectedObjects(string imageName, IList<BoundingBox> boundingBoxes)
        {
            Console.WriteLine($"The objects detected in image {imageName}..");

            foreach (var box in boundingBoxes)
            {
                Console.WriteLine($"{box.Label} w/ confidence: {box.Confidence}");
            }

            Console.WriteLine();
            Console.Out.Flush();
        }
    }
}