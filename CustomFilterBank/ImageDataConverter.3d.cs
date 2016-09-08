using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace CustomFilterBank_Test
{
    public partial class ImageDataConverter
    {
        public static int[, ,] ToInteger3d_32bit(Bitmap bitmap)
        {
            int i, j, Width, Height;

            Width = bitmap.Width;
            Height = bitmap.Height;

            int[, ,] GreyImage = new int[3, Width, Height];

            BitmapData bitmapData1 = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;

                for (i = 0; i < bitmapData1.Height; i++)
                {
                    for (j = 0; j < bitmapData1.Width; j++)
                    {
                        GreyImage[0, j, i] = (int)imagePointer1[0];
                        GreyImage[1, j, i] = (int)imagePointer1[1];
                        GreyImage[2, j, i] = (int)imagePointer1[2];
                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            bitmap.UnlockBits(bitmapData1);
            return GreyImage;
        }

        public static Bitmap ToBitmap3d_32bit(int[, ,] image)
        {
            Bitmap output = new Bitmap(image.GetLength(1), image.GetLength(2));
            BitmapData bitmapData1 = output.LockBits(new Rectangle(0, 0, image.GetLength(1), image.GetLength(2)),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        imagePointer1[0] = (byte)image[0, j, i];
                        imagePointer1[1] = (byte)image[1, j, i];
                        imagePointer1[2] = (byte)image[2, j, i];
                        imagePointer1[3] = 255;
                        //4 bytes per pixel
                        imagePointer1 += 4;
                    }//end for j
                    //4 bytes per pixel
                    imagePointer1 += (bitmapData1.Stride - (bitmapData1.Width * 4));
                }//end for i
            }//end unsafe
            output.UnlockBits(bitmapData1);
            return output;// col;
        }

        public static Bitmap ToBitmap32bit(int[,] image)
        {
            Bitmap bitmap = new Bitmap(image.GetLength(0), image.GetLength(1));
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, image.GetLength(0), image.GetLength(1)),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (int i = 0; i < bitmapData.Height; i++)
                {
                    for (int j = 0; j < bitmapData.Width; j++)
                    {
                        address[0] = 0;
                        address[1] = (byte)image[j, i];
                        address[2] = 0;
                        address[3] = 255;
                        //4 bytes per pixel
                        address += 4;
                    }//end for j
                    //4 bytes per pixel
                    address += (bitmapData.Stride - (bitmapData.Width * 4));
                }//end for i
            }//end unsafe

            bitmap.UnlockBits(bitmapData);

            return bitmap;// col;
        }
    }
}
