using Microsoft.ML.Data;

namespace MLNet.detection
{
    /**
     * ImageNetPrediction je klasa za predikciju
     */

    public class ImageNetPrediction
    {
        /**
         * PredictedLabel sadrzi dimenizje, skor objekta
         * i verovatnoce klasa za sve detektovane box-ove na slici
         */

        [ColumnName("grid")]
        public float[] PredictedLabels;
    }
}