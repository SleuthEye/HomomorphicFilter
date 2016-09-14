using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace CustomFilterBank_Test
{
    public static partial class ImageDataConverter
    {
        #region Bitmap ToBitmap32(int[,] image)
        //Tested
        ///Working fine. 
        public static Bitmap ToBitmap32(int[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);
            int i, j;
            Bitmap bitmap = new Bitmap(Width, Height);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = sizeof(int);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (i = 0; i < bitmapData.Height; i++)
                {
                    for (j = 0; j < bitmapData.Width; j++)
                    {
                        byte[] bytes = BitConverter.GetBytes(image[j, i]);

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            address[k] = bytes[k];
                        }
                        //4 bytes per pixel
                        address += bytesPerPixel;
                    }//end for j

                    //4 bytes per pixel
                    address += (bitmapData.Stride - (bitmapData.Width * bytesPerPixel));
                }//end for i
            }//end unsafe
            bitmap.UnlockBits(bitmapData);
            return bitmap;// col;
        }
        #endregion

        #region int[,] ToInteger32(Bitmap bitmap)
        //Tested
        ///Working fine. 
        public static int[,] ToInteger32(Bitmap bitmap)
        {
            int[,] array2D = new int[bitmap.Width, bitmap.Height];

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                     ImageLockMode.ReadWrite,
                                                     PixelFormat.Format32bppRgb);
            int bytesPerPixel = sizeof(int);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                int paddingOffset = bitmapData.Stride - (bitmap.Width * bytesPerPixel);//4 bytes per pixel

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        byte[] temp = new byte[bytesPerPixel];

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            temp[k] = address[k];
                        }
                        array2D[j, i] = BitConverter.ToInt32(temp, 0);

                        //4-bytes per pixel
                        address += bytesPerPixel;//4-channels
                    }
                    address += paddingOffset;
                }
            }
            bitmap.UnlockBits(bitmapData);

            return array2D;
        }
        #endregion

        public static int[,] ToInteger(Bitmap image)
        {
            Bitmap bitmap = (Bitmap)image.Clone();

            int[,] array2D = new int[bitmap.Width, bitmap.Height];

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                     ImageLockMode.ReadWrite,
                                                     PixelFormat.Format8bppIndexed);
            int bytesPerPixel = sizeof(byte);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                int paddingOffset = bitmapData.Stride - (bitmap.Width * bytesPerPixel);

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        byte[] temp = new byte[bytesPerPixel];

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            temp[k] = address[k];
                        }

                        int iii = 0;

                        if (bytesPerPixel >= sizeof(int))
                        {
                            iii = BitConverter.ToInt32(temp, 0);
                        }
                        else
                        {
                            iii = (int)temp[0];
                        }

                        array2D[j, i] = iii;

                        address += bytesPerPixel;
                    }
                    address += paddingOffset;
                }
            }
            bitmap.UnlockBits(bitmapData);

            return array2D;
        }

        public static Bitmap ToBitmap(int[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);
            int i, j;

            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

            int bytesPerPixel = sizeof(byte);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (i = 0; i < bitmapData.Height; i++)
                {
                    for (j = 0; j < bitmapData.Width; j++)
                    {
                        byte[] bytes = BitConverter.GetBytes(image[j, i]);

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            address[k] = bytes[k];
                        }

                        address += bytesPerPixel;
                    }

                    address += (bitmapData.Stride - (bitmapData.Width * bytesPerPixel));
                }
            }
            bitmap.UnlockBits(bitmapData);

            Grayscale.SetGrayscalePalette(bitmap);

            return bitmap;
        }

        public static int[,] ToInteger(Complex[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            int[,] integer = new int[Width, Height];


            for (int j = 0; j <= Height - 1; j++)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    integer[i, j] = ((int)image[i, j].Magnitude);
                }
            }

            return integer;
        }

        public static int[,] ToInteger(double[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            int[,] integer = new int[Width, Height];


            for (int j = 0; j <= Height - 1; j++)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    integer[i, j] = ((int)image[i, j]);
                }
            }

            return integer;
        }

        public static Complex[,] ToComplex(int[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            Complex[,] comp = new Complex[Width, Height];

            for (int j = 0; j <= Height - 1; j++)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    Complex tempComp = new Complex((double)image[i, j], 0.0);
                    comp[i, j] = tempComp;
                }
            }

            return comp;
        }

        public static Complex[,] ToComplex(Bitmap image)
        {
            Bitmap bitmap = (Bitmap)image.Clone();

            Complex[,] comp = new Complex[bitmap.Width, bitmap.Height];

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                     ImageLockMode.ReadWrite,
                                                     PixelFormat.Format8bppIndexed);
            int bytesPerPixel = sizeof(byte);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                int paddingOffset = bitmapData.Stride - (bitmap.Width * bytesPerPixel);

                for (int j = 0; j < bitmap.Height; j++)
                {
                    for (int i = 0; i < bitmap.Width; i++)
                    {

                        byte[] temp = new byte[bytesPerPixel];

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            temp[k] = address[k];
                        }

                        int iii = 0;

                        if (bytesPerPixel >= sizeof(int))
                        {
                            iii = BitConverter.ToInt32(temp, 0);
                        }
                        else
                        {
                            iii = (int)temp[0];
                        }

                        Complex tempComp = new Complex((double)iii, 0.0);
                        comp[i, j] = tempComp;

                        address += bytesPerPixel;
                    }

                    address += paddingOffset;
                }
            }

            bitmap.UnlockBits(bitmapData);

            return comp;
        }

        public static Bitmap ToBitmap(Complex[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);
            int i, j;

            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

            int bytesPerPixel = sizeof(byte);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (i = 0; i < bitmapData.Height; i++)
                {
                    for (j = 0; j < bitmapData.Width; j++)
                    {

                        int integer = ((int)image[j, i].Magnitude);

                        byte[] bytes = BitConverter.GetBytes(integer);

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            address[k] = bytes[k];
                        }

                        address += bytesPerPixel;
                    }

                    address += (bitmapData.Stride - (bitmapData.Width * bytesPerPixel));
                }
            }
            bitmap.UnlockBits(bitmapData);

            Grayscale.SetGrayscalePalette(bitmap);

            return bitmap;
        }

        public static double[,] ToDouble(Bitmap input)
        {
            BitmapData bitmapData = input.LockBits(new Rectangle(0, 0, input.Width, input.Height),
                                                    ImageLockMode.ReadOnly,
                                                    PixelFormat.Format8bppIndexed);

            int width = input.Width;
            int height = input.Height;
            int pixelSize = Bitmap.GetPixelFormatSize(input.PixelFormat) / 8;
            int offset = bitmapData.Stride - bitmapData.Width * pixelSize;

            double[,] output = new double[width, height];

            double Min = 0.0;
            double Max = 255.0;

            try
            {
                unsafe
                {
                    fixed (double* ptrData = output)
                    {
                        double* dst = ptrData;

                        byte* src = (byte*)bitmapData.Scan0;

                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                byte srscsc = (byte)(*src);

                                double scaled = Tools.Scale(srscsc, 0, 255, Min, Max);

                                *dst = scaled;

                                src += pixelSize;

                                dst++;
                            }

                            src += offset;
                        }
                    }
                }
            }
            finally
            {
                input.UnlockBits(bitmapData);
            }

            return output;
        }

        public static Bitmap ToBitmap(double[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);

            Bitmap output = Grayscale.CreateGrayscaleImage(width, height);

            BitmapData data = output.LockBits(new Rectangle(0, 0, width, height),
                                                ImageLockMode.WriteOnly,
                                                output.PixelFormat);

            int pixelSize = System.Drawing.Image.GetPixelFormatSize(PixelFormat.Format8bppIndexed) / 8;

            int offset = data.Stride - width * pixelSize;

            double Min = 0.0;
            double Max = 255.0;

            unsafe
            {
                byte* address = (byte*)data.Scan0.ToPointer();

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        double v = 255 * (input[x, y] - Min) / (Max - Min);

                        byte value = unchecked((byte)v);

                        for (int c = 0; c < pixelSize; c++, address++)
                        {
                            *address = value;
                        }
                    }

                    address += offset;
                }
            }

            output.UnlockBits(data);

            return output;
        }

        public static double[,] ToDouble(Complex[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            double[,] integer = new double[Width, Height];


            for (int x = 0; x <= Width - 1; x++)
            {
                for (int y = 0; y <= Height - 1; y++)
                {
                    integer[x, y] = image[x, y].Magnitude;
                }
            }

            return integer;
        }

        public static Complex[,] ToComplex(double[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            Complex[,] comp = new Complex[Width, Height];


            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    Complex tempComp = new Complex(image[i, j], 0.0);
                    comp[i, j] = tempComp;
                }
            }

            return comp;
        }

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
                    byte* src = (byte*)bitmapData.Scan0.ToPointer();

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