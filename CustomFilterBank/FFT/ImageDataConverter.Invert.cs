using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace CustomFilterBank_Test
{
    partial class ImageDataConverter
    {
        #region void Invert(BitmapData image)
        private static void Invert(BitmapData image)
        {
            int Left = 0;
            int Top = 0;

            int Width = image.Width;
            int Height = image.Height;

            int pixelSize = 1;//1 byte

            int startY = Top;
            int stopY = startY + Height;

            int startX = Left * pixelSize;
            int stopX = startX + Width * pixelSize;

            unsafe
            {
                byte* basePtr = (byte*)image.Scan0.ToPointer();

                if (
                    (image.PixelFormat == PixelFormat.Format8bppIndexed) ||
                    (image.PixelFormat == PixelFormat.Format24bppRgb))
                {
                    int offset = image.Stride - (stopX - startX);

                    // allign pointer to the first pixel to process
                    byte* ptr = basePtr + (startY * image.Stride + Left * pixelSize);

                    // invert
                    for (int y = startY; y < stopY; y++)
                    {
                        for (int x = startX; x < stopX; x++, ptr++)
                        {
                            // ivert each pixel
                            *ptr = (byte)(255 - *ptr);
                        }
                        ptr += offset;
                    }
                }
                else
                {
                    throw new Exception("8bpp edge required");
                }
            }
        } 
        #endregion

        public static void Invert(Bitmap image)
        {

            BitmapData bitmapData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                int offset = bitmapData.Stride - bitmapData.Width;

                unsafe
                {
                    // allign pointer to the first pixel to process
                    byte* src = (byte*) bitmapData.Scan0.ToPointer();

                    // invert
                    for (int y = 0; y < bitmapData.Height; y++)
                    {
                        for (int x = 0; x < bitmapData.Width; x++, src++)
                        {
                            // ivert each pixel
                            *src = (byte)(255 - *src);
                        }

                        src += offset;
                    }
                }
            }
            else
            {
                throw new Exception("8bpp edge required");
            }

            image.UnlockBits(bitmapData);
        }
    }
}
