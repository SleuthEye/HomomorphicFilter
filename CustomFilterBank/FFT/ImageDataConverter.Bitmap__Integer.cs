using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

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
            
            Bitmap bitmap = new Bitmap(Width, Height);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            int bytesPerPixel = sizeof(int);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (int y = 0; y < bitmapData.Height; y++)
                {
                    for (int x = 0; x < bitmapData.Width; x++)
                    {
                        byte[] bytes = BitConverter.GetBytes(image[y,x]);

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


                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        byte[] temp = new byte[bytesPerPixel];

                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            temp[k] = address[k];
                        }

                        array2D[y, x] = BitConverter.ToInt32(temp, 0);

                        address += bytesPerPixel;
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

                //Debug.WriteLine("{0},,,,{1}", bitmap.Width, bitmap.Height);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int iii = 0;

                        //If there are more than 1 channels...
                        //(Would actually never happen)
                        if (bytesPerPixel >= sizeof(int))
                        {
                            byte[] temp = new byte[bytesPerPixel];

                            //Handling the channels.
                            //PixelFormat.Format8bppIndexed == 1 channel == 1 bytesPerPixel == byte
                            //PixelFormat.Format32bpp       == 4 channel == 4 bytesPerPixel == int
                            for (int k = 0; k < bytesPerPixel; k++)
                            {
                                temp[k] = address[k];
                            }

                            iii = BitConverter.ToInt32(temp, 0);
                        }
                        else// If there is only one channel:
                        {
                            iii = (int)(*address);
                        }

                        //Debug.Write("("+x+","+y+")");

                        array2D[x,y] = iii;

                        address += bytesPerPixel;
                    }

                    address += paddingOffset;

                    Debug.WriteLine(@"\Height\t");
                }
            }
            bitmap.UnlockBits(bitmapData);

            return array2D;
        }

        public static Bitmap ToBitmap(int[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);
            int y, x;

            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                     ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

            int bytesPerPixel = sizeof(byte);

            unsafe
            {
                byte* address = (byte*)bitmapData.Scan0;

                for (y = 0; y < bitmapData.Height; y++)
                {
                    for (x = 0; x < bitmapData.Width; x++)
                    {
                        //Converting int to an array of bytes
                        byte[] bytes = BitConverter.GetBytes(image[x,y]);//?????????

                        //placing the array of bytes into memory
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
    }
}
