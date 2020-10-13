namespace MLNet.classification
{
    public class ImageDataInMemory
    {
        /**
         * Slicno kao ImageData ali neophodno za procesiranje eksternih slika
         * slika koje su van dataseta
         */

        public ImageDataInMemory(byte[] image, string label, string imagePath)
        {
            Image = image;
            Label = label;
            ImagePath = imagePath;
        }

        public readonly byte[] Image;

        public readonly string Label;

        public readonly string ImagePath;
    }
}