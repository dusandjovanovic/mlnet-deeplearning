namespace MLNet.detection
{
    /**
     * Koordinate i dimenzije box-a oko objekata
     */

    public class DimensionsBase
    {
        /**
         * X kao pozicija na x-osi
         */
        public float X { get; set; }

        /**
         * Y kao pozicija na y-osi
         */
        public float Y { get; set; }

        /**
         * Height je visina objekta
         */
        public float Height { get; set; }

        /**
         * Width je sirina objekta
         */
        public float Width { get; set; }
    }
}