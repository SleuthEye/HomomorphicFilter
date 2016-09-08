using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFilterBank_Test
{
    public partial class Grayscale
    {
        public static unsafe Bitmap ToGrayscale(Bitmap colorBitmap)
        {
            int Width = colorBitmap.Width;
            int Height = colorBitmap.Height;

            Bitmap grayscaleBitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

            grayscaleBitmap.SetResolution(colorBitmap.HorizontalResolution,
                                 colorBitmap.VerticalResolution);

            ///////////////////////////////////////
            // Set grayscale palette
            ///////////////////////////////////////
            ColorPalette colorPalette = grayscaleBitmap.Palette;
            for (int i = 0; i < colorPalette.Entries.Length; i++)
            {
                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
            }

            grayscaleBitmap.Palette = colorPalette;

            BitmapData bitmapData = grayscaleBitmap.LockBits(
                new Rectangle(Point.Empty, grayscaleBitmap.Size),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            Byte* pPixel = (Byte*)bitmapData.Scan0;
            
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color clr = colorBitmap.GetPixel(x,y);

                    Byte byPixel = (byte)((30 * clr.R + 59 * clr.G + 11 * clr.B) / 100);

                    pPixel[x] = byPixel;
                }

                pPixel += bitmapData.Stride;
            }

            grayscaleBitmap.UnlockBits(bitmapData);

            return grayscaleBitmap;
        } 

        public static int[,] ToGrayscale2(Bitmap colorBitmap)
        {
            Bitmap bmp = Grayscale.ToGrayscale(colorBitmap);

            return ImageDataConverter.ToInteger(bmp);
        }

        public static Bitmap CreateGrayscaleImage(int width, int height)
        {
            // create new image
            Bitmap image = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            // set palette to grayscale
            SetGrayscalePalette(image);
            // return new image
            return image;
        }

        public static Bitmap Fill(Bitmap image, Color fill)
        {
            BitmapLocker locker = new BitmapLocker(image);
            locker.Lock();

            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    locker.SetPixel(i, j, fill);
                }
            }

            locker.Unlock();

            return image;
        }

        public static Bitmap CreateGrayscaleImage(int width, int height, Color fill)
        {
            // create new image
            Bitmap image = CreateGrayscaleImage(width, height);
            // set palette to grayscale
            image = Fill(image, fill);
            // return new image
            return image;
        }

        public static void SetGrayscalePalette(Bitmap image)
        {
            // check pixel format
            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                // get palette
                ColorPalette cp = image.Palette;

                // init palette
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }

                // set palette back
                image.Palette = cp;
            }
            else
            {
                throw new Exception("8 bit grayscaleImage required");
            }
        }

        public static bool IsGrayscale(Bitmap bitmap)
        {
            return bitmap.PixelFormat == PixelFormat.Format8bppIndexed ? true : false;
        }

        public static bool IsDimensionsPowerOf2(Bitmap bitmap)
        {
            return (Tools.IsPowerOf2(bitmap.Width) && Tools.IsPowerOf2(bitmap.Height));
        }

        internal static void Fill(double[,] image, double color)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    image[i, j] = color;
                }
            }
        }
    }
}
