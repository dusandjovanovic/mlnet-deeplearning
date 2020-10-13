using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using MLNet.common;
using MLNet.classification;
using MLNet.detection;

namespace MLNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /**
         * Instance klase za klasifikaciju
         */
        private Classification classification = new Classification();

        /**
         * Instance klase za detekciju objekata
         */
        private Detection detection = new Detection();

        /**
         * Override klasa konzolnog ulaza
         */
        private NativeConsole nativeConsole;

        /**
         * Izabrana slika
         */
        private Bitmap selectedImage;

        /**
         * Putanja izabrane slike
         */
        private string selectedImagePath;

        public MainWindow()
        {
            InitializeComponent();
            nativeConsole = new NativeConsole(consoleLog);
            Console.SetOut(nativeConsole);
            ContentRendered += MainWindow_Rendered;
        }

        private void MainWindow_Rendered(object sender, EventArgs e)
        {
            Console.WriteLine("Initiating Image-classification and Object-detection modules...");
            var timer = new System.Threading.Timer(Initiate, "Initiate", 0, 1000);
        }

        private void InitiateInputs(bool state = true)
        {
            ClassificationButton.IsEnabled = state;
            ObjectDetectionButton.IsEnabled = state;
        }

        private void Initiate(object state)
        {
            classification.Process();
            detection.Process();
            Console.Out.Flush();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedImagePath = dialog.FileName;
                FileNameLabel.Content = dialog.FileName;
                ImageViewer.Source = ImageUtils.GetBitmapImageFromURI(dialog.FileName);
                selectedImage = ImageUtils.GetBitmapFromURI(dialog.FileName);
                InitiateInputs();
            }
        }

        private void ClassificationButton_Click(object sender, RoutedEventArgs e)
        {
            classification.ClassifyExternalImage(selectedImage);
        }

        private void ObjectDetectionButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer.Source = detection.ProcessExternalImage(selectedImagePath);
        }
    }
}