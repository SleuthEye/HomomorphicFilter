using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public class ImagePadder
    {
        public static Bitmap Pad(Bitmap input, int newWidth, int newHeight)
        {
            Bitmap image = (Bitmap)input.Clone();

            int width = image.Width;
            int height = image.Height;
            /*
             It is always guaranteed that,
                 
                    width < newWidth
                 
                        and
                  
                    height < newHeight                  
             */
            if ((width < newWidth && height < newHeight)
                    || (width < newWidth && height == newHeight)
                    || (width == newWidth && height < newHeight))
            {
                Bitmap paddedImage = new Bitmap(newWidth, newHeight, image.PixelFormat); // Grayscale.CreateGrayscaleImage(newWidth, newHeight);

                BitmapLocker inputImageLocker = new BitmapLocker(image);
                BitmapLocker paddedImageLocker = new BitmapLocker(paddedImage);

                inputImageLocker.Lock();
                paddedImageLocker.Lock();

                int startPointX = (int)Math.Ceiling((double)(newWidth - width) / (double)2) - 1;
                int startPointY = (int)Math.Ceiling((double)(newHeight - height) / (double)2) - 1;

                for (int y = startPointY; y < (startPointY + height); y++)
                {
                    for (int x = startPointX; x < (startPointX + width); x++)
                    {
                        int xxx = x - startPointX;
                        int yyy = y - startPointY;

                        paddedImageLocker.SetPixel(x, y, inputImageLocker.GetPixel(xxx, yyy));

                        string str = string.Empty;
                    }
                }

                paddedImageLocker.Unlock();
                inputImageLocker.Unlock();

                return paddedImage;
            }
            else if (width == newWidth && height == newHeight)
            {
                return image;
            }
            else
            {
                throw new Exception("Pad() -- threw an exception");
            }
        }

        public static double[,] Pad(double[,] image, int newWidth, int newHeight)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            /*
             It is always guaranteed that,
                 
                    width < newWidth
                 
                        and
                  
                    height < newHeight                  
             */
            if ((width < newWidth && height < newHeight)
                    || (width < newWidth && height == newHeight)
                    || (width == newWidth && height < newHeight))
            {
                double[,] resizedImage = new double[newWidth, newHeight];

                double color = 0.0;

                Grayscale.Fill(resizedImage, color);

                int startPointX = ((newWidth - width) / 2) - 1;
                int startPointY = ((newHeight - height) / 2) - 1;

                for (int y = startPointY; y < startPointY + height; y++)
                {
                    for (int x = startPointX; x < startPointX + width; x++)
                    {
                        int xxx = x - startPointX;
                        int yyy = y - startPointY;

                        resizedImage[x, y] = image[xxx, yyy];
                    }
                }

                return resizedImage;
            }
            else if (width == newWidth && height == newHeight)
            {
                return image;
            }
            else
            {
                throw new Exception("Pad() -- threw an exception");
            }
        }

        public static Complex[,] ZeroOut(Complex[,] cPaddedKernel, int unpaddedWidth, int unpaddedHeight, int newWidth, int newHeight)
        {
            Complex[,] cKernel = (Complex[,])cPaddedKernel.Clone();

            int startPointX = (int)Math.Ceiling((double)(newWidth - unpaddedWidth) / (double)2) - 1;
            int startPointY = (int)Math.Ceiling((double)(newHeight - unpaddedHeight) / (double)2) - 1;
            for (int j = 0; j < newHeight; j++)
            {
                for (int i = 0; i < startPointX; i++)
                {
                    cKernel[i, j] = 0;
                }
                for (int i = startPointX + unpaddedWidth; i < newWidth; i++)
                {
                    cKernel[i, j] = 0;
                }
            }
            for (int i = startPointX; i < startPointX + unpaddedWidth; i++)
            {
                for (int j = 0; j < startPointY; j++)
                {
                    cKernel[i, j] = 0;
                }
                for (int j = startPointY + unpaddedHeight; j < newHeight; j++)
                {
                    cKernel[i, j] = 0;
                }
            }

            return cKernel;
        }

        #region public static Complex[,] Pad(Complex[,] image, int newMaskWidth, int newMaskHeight, int value)
        public static Complex[,] Pad(Complex[,] image, int newWidth, int newHeight)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            /*
             It is always guaranteed that,
                 
                    width < newWidth
                 
                        and
                  
                    height < newHeight                  
             */
            if ((width < newWidth && height < newHeight)
                    || (width < newWidth && height == newHeight)
                    || (width == newWidth && height < newHeight))
            {
                Complex[,] resizedImage = new Complex[newWidth, newHeight];

                //double color = 0.0;
                //Grayscale.Fill(resizedImage, color);

                int startPointX = ((newWidth - width) / 2) - 1;
                int startPointY = ((newHeight - height) / 2) - 1;

                for (int y = startPointY; y < startPointY + height; y++)
                {
                    for (int x = startPointX; x < startPointX + width; x++)
                    {
                        int xxx = x - startPointX;
                        int yyy = y - startPointY;

                        resizedImage[x, y] = new Complex(image[xxx, yyy].Real, image[xxx, yyy].Imaginary);
                    }
                }

                return resizedImage;
            }
            else if (width == newWidth && height == newHeight)
            {
                return image;
            }
            else
            {
                throw new Exception("Pad() -- threw an exception");
            }
        }
        #endregion

        #region
        //public static int[,] Pad(int[,] image, int newWidth, int newHeight)
        //{
        //    int width = image.GetLength(0);
        //    int height = image.GetLength(1);

        //    if ((width == height) && (width < newWidth && height < newHeight))
        //    {
        //        int[,] resizedImage = new int[width, height];

        //        int padValue = Color.Black.ToArgb();

        //        for (int j = 0; j < height; j++)
        //        {
        //            for (int i = 0; i < width; i++)
        //            {
        //                resizedImage[j,i] = padValue;
        //            }
        //        }

        //        if (newWidth != width || newHeight != height)
        //        {
        //            int startPointX = (newWidth - width) / 2;
        //            int startPointY = (newHeight - height) / 2;

        //            for (int y = startPointY; y < startPointY + height; y++)
        //            {
        //                for (int x = startPointX; x < startPointX + width; x++)
        //                {
        //                    int temp = image[y - startPointY, x - startPointX];
        //                    resizedImage[y, x] = temp;
        //                }
        //            }

        //            string str = string.Empty;
        //        }
        //        else
        //        {
        //            for (int j = 0; j < height; j++)
        //            {
        //                for (int i = 0; i < width; i++)
        //                {
        //                    resizedImage[j,i] = image[j,i];
        //                }
        //            }
        //        }

        //        return resizedImage;
        //    }
        //    else
        //    {
        //        throw new Exception("Pad() - threw an exception!");
        //    }
        //} 
        #endregion
    }
}
