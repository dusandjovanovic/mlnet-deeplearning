using System.Drawing;

namespace MLNet.detection
{
    public class BoundingBoxDimensions : DimensionsBase { }

    /**
     * Klasa za okvirne boxove oko detektovanih objekata
     */

    public class BoundingBox
    {
        /**
         * Dimensions sadrzi dimenizje bounding box-a
         */
        public BoundingBoxDimensions Dimensions { get; set; }

        /**
         * Label sadrzi klasu objekta koji se nalazi unutar box-a
         */
        public string Label { get; set; }

        /**
         * Confidence opisuje sigurnost u predvidjenu klasu
         */
        public float Confidence { get; set; }

        /**
         * Rect sadrzi pravougaonu reprezentaciju box-a
         */

        public RectangleF Rect
        {
            get { return new RectangleF(Dimensions.X, Dimensions.Y, Dimensions.Width, Dimensions.Height); }
        }

        /**
         * BoxColor opisuje boju koja se vezuje za klasu
         */
        public Color BoxColor { get; set; }
    }
}