namespace MLNet.classification
{
    public class ImageData
    {
        /**
         * ImagePath je puna putanja do slike
         */
        public string ImagePath { get; set; }

        /**
         * Label je kategorija kojoj pripada slika
         * ova vrednost se predvidja
         */
        public string Label { get; set; }
    }
}