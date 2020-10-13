using System;

namespace MLNet.classification
{
    /**
     * Samo Image i LabelAsKey se koriste za treniranje modela i predikcije
     */

    internal class ModelInput
    {
        /**
         * Image je byte[] reprezentacija slike
         */
        public byte[] Image { get; set; }

        /**
         * LabelAsKey je numericka reprezentacija Labele
         */
        public UInt32 LabelAsKey { get; set; }

        /**
         * ImagePath je putanja do slike
         */
        public string ImagePath { get; set; }

        /**
         * Label je kategorija kojoj pripada slika
         * ova vrednost se predvidja
         */
        public string Label { get; set; }
    }
}