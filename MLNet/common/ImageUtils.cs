using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace MLNet.common
{
    /**
     * Pomocna klasa sa util metodama za upravljanje slikama
     */

    public static class ImageUtils
    {
        public static BitmapImage GetBitmapImageFromURI(string uri)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(uri);
            bitmap.EndInit();
            return bitmap;
        }

        public static Bitmap GetBitmapFromURI(string uri)
        {
            return new Bitmap(uri);
        }

        public static BitmapImage GetBitmapImageFromBitmap(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}