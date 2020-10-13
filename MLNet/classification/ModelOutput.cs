namespace MLNet.classification
{
    internal class ModelOutput
    {
        /**
         * ImagePath je putanja do slike
         */
        public string ImagePath { get; set; }

        /**
         * Label je kategorija kojoj pripada slika
         * ova vrednost se predvidja
         */
        public string Label { get; set; }

        /**
         * PredictedLabel je vrednost predvidjena od strane modela
         */
        public string PredictedLabel { get; set; }
    }
}