using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public partial class ImageDataConverter
    {
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
            public static Complex[,] ToComplex(Bitmap image)
            {
                return ImageDataConverter.ToComplex(ImageDataConverter.ToInteger(image));
            }

            public static Bitmap ToBitmap(Complex[,] image)
            {
                return ImageDataConverter.ToBitmap(ImageDataConverter.ToInteger(image));
            }
        
        public static int [,] ToInteger(Complex [,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);

            int[,] integer = new int[Width, Height];

            for (int x = 0; x <= Width - 1; x++)
            {
            for (int y = 0; y <= Height - 1; y++)
            {
                    integer[x,y] =  ((int)image[x,y].Magnitude);
                }
            }

            return integer;
        }

        public static Complex [,] ToComplex(int[,] image)
        {
            int Width = image.GetLength(0);
            int Height = image.GetLength(1);
            
            Complex[,] comp = new Complex[Width, Height];

            
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                    Complex tempComp = new Complex((double)image[i,j], 0.0);
                    comp[i,j] = tempComp;
                }
            }

            return comp;
        }
    }
}
