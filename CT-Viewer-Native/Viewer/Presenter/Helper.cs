using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Viewer.Presenter
{
    public unsafe static class Helper
    {
        private static void NormalizeImage(short** pixelData, int gw_center, int gw_width, IntPtr ptr, int height, int width)
        {
            int lower = gw_center - (gw_width / 2);
            int upper = gw_center + (gw_width / 2);
            int i = 0;
            short current = 0;
            float quot = 1.0f;
            int value = 0;
            int* p = (int*)ptr.ToPointer();

            if (gw_width >= 1)
            {
                quot = 255.0f / gw_width;
            }

            for (int x = 0; x < width; x++) 
            {
                for (int y = 0; y < height; y++)
                {
                    current = pixelData[x][y];
                    *(p + i) = 0;
                    if (current <= lower)
                    {
                        value = 0;
                    }
                    else if (current >= upper)
                    {
                        value = 255;
                    }
                    else
                    {
                        value = (int)((current - lower) * quot);
                    }

                    if (value > 255)
                    {
                        value = 255;
                    }
                    *(p + i) = (255 << 24) | (value << 16) | (value << 8) | value;
                    i++;
                }
            }
        }

        public static Image GetImage(short** pixelData, int gw_center, int gw_width, int dimension, int height, int width)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            NormalizeImage(pixelData, gw_center, gw_width, ptr, height, width);
            bmp.UnlockBits(bmpData);
            return bmp;
        }
    }
}
