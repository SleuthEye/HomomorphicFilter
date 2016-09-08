using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public partial class FourierShifter
    {
        public static Bitmap ShiftBitmap(Complex[,] shiftedFftImage)
        {
            return ImageDataConverter.ToBitmap(ShiftFft(shiftedFftImage));
        }

        public static Complex[,] ShiftFft(Complex[,] fftImage)
        {
            int Width = fftImage.GetLength(0);
            int Height = fftImage.GetLength(1);

            Complex[,] shifted = new Complex[Width, Height];

            int halfOfWidth = (int)Math.Ceiling((double)Width / 2.0);
            int halfOfHeight = (int)Math.Ceiling((double)Height / 2.0);

            for (int j = 0; j < halfOfHeight; j++)
            {
            for (int i = 0; i < halfOfWidth; i++)
            {
                
                    int x = i + halfOfWidth;
                    int y = j + halfOfHeight;

                    shifted[x, y] = fftImage[i, j];
                    shifted[i, j] = fftImage[x, y];
                    shifted[x, j] = fftImage[i, y];
                    shifted[i, y] = fftImage[x, j];
                }
            }

            return shifted;
        }

        /////////////////////////////
        //Never used in this solution
        #region Complex[,] RemoveFFTShift(Complex[,] FFTShifted)
        public static Complex[,] RemoveFFTShift(Complex[,] shiftedImage)
        {
            int nx = shiftedImage.GetLength(0);
            int ny = shiftedImage.GetLength(1);

            int i, j;
            Complex[,] FFTNormal = new Complex[nx, ny];

            for (i = 0; i <= (nx / 2) - 1; i++)
                for (j = 0; j <= (ny / 2) - 1; j++)
                {
                    FFTNormal[i + (nx / 2), j + (ny / 2)] = shiftedImage[i, j];
                    FFTNormal[i, j] = shiftedImage[i + (nx / 2), j + (ny / 2)];
                    FFTNormal[i + (nx / 2), j] = shiftedImage[i, j + (ny / 2)];
                    FFTNormal[i, j + (ny / 2)] = shiftedImage[i + (nx / 2), j];
                }
            return FFTNormal;
        } 
        #endregion
    }
}
