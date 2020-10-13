using Microsoft.ML.Data;

namespace MLNet.detection
{
    /**
     * ImageNetData je ulazna klasa sa string poljima
     */

    public class ImageNetData
    {
        /**
         * ImagePath za putanju do slike
         */

        [LoadColumn(0)]
        public string ImagePath;

        /**
         * Label sadrzi ime datoteke
         */

        [LoadColumn(1)]
        public string Label;
    }
}