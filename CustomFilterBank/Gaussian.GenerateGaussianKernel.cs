using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomFilterBank_Test
{
    public static partial class Gaussian
    {
        public static double[,] GenerateGaussianKernel(int N, double Sigma, double Slope, out double Weight)
        {
            double pi;
            pi = (double)Math.PI;
            int i, j;
            int SizeofKernel = N;
            double[,] GaussianKernel = new double[N, N]; ;
            double[,] Kernel = new double[N, N];

            double[,] OP = new double[N, N];
            double D1, D2;

            D1 = 1 / (2 * pi * Sigma * Sigma);
            D2 = 2 * Sigma * Sigma;

            double min = 1000, max = 0;

            for (i = -SizeofKernel / 2; i <= SizeofKernel / 2 - 1; i++)
            {
                for (j = -SizeofKernel / 2; j <= SizeofKernel / 2 - 1; j++)
                {
                    Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = ((1 / D1) * (double)Math.Exp(-Slope * (i * i + j * j) / D2));

                    if (Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] < min)
                        min = Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                    if (Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] > max)
                        max = Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                }
            }
            //Converting to the scale of 0-1
            double sum = 0;
            for (i = -SizeofKernel / 2; i <= SizeofKernel / 2 - 1; i++)
            {
                for (j = -SizeofKernel / 2; j <= SizeofKernel / 2 - 1; j++)
                {
                    GaussianKernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] = (Kernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j] - min) / (max - min);
                    sum = sum + GaussianKernel[SizeofKernel / 2 + i, SizeofKernel / 2 + j];
                }
            }
            //Normalizing kernel Weight
            Weight = sum;

            return GaussianKernel;
        }

        /// <summary>
        /// Generates Gaussian Filter Kernel
        /// </summary>
        /// <param name="Width">Size of the Filter</param>
        /// <param name="Sigma">Spread of the Gaussian</param>
        /// <param name="Slope">Harpness of the Slope of the Gaussian</param>
        /// <param name="Weight">Weight of the Filter Kernel (Out Variable)</param>
        /// <returns>GAussian Kernel</returns>
        public static double[,] GaussianKernelHPF(int Width, int Height, double Sigma, double Slope, out double Weight)
        {
            double pi = (double)Math.PI;
            double[,] GaussianKernel = new double[Width, Height]; ;
            double[,] Kernel = new double[Width, Height];
            //double[,] OP = new double[Width, Height];
            //double D1, D2;
            double D1 = 1 / (2 * pi * Sigma * Sigma);
            double D2 = 2 * Sigma * Sigma;
            double K = 1 / D1;
            double S = Width * Height / (Math.PI * Math.PI * D2 * D2);

            int halfOfWidth = Width / 2;
            int halfOfHeight = Height / 2;

            //Compute the low-pass kernel
            for (int i = -halfOfWidth ; i < halfOfWidth ; i++)
            {
                for (int j = -halfOfHeight; j < halfOfHeight ; j++)
                {
                    int x = halfOfWidth + i;
                    int y = halfOfHeight + j;

                    Kernel[x, y] = (K * (double)Math.Exp(-Slope * (i * i + j * j) / D2));
                }
            }
            //Converting to high-pass kernel
            double sum = 0;
            for (int i = -halfOfWidth; i < halfOfWidth; i++)
            {
                for (int j = -halfOfHeight; j < halfOfHeight; j++)
                {
                    int x = halfOfWidth + i;
                    int y = halfOfHeight + j;

                    if (i == 0 && j == 0)
                    {
                        GaussianKernel[x, y] = Width * Height + (K / D1 - Kernel[x, y]) * S;
                    }
                    else
                    {
                        GaussianKernel[x, y] = -Kernel[x, y] * S;
                    }
                    sum = sum + GaussianKernel[x, y];
                }

            }
            //Normalizing kernel Weight
            Weight = sum;

            //Swap halves so the peak is at pixel (0,0)
            double[,] shifted = new double[Width, Height];
            for (int j = 0; j < halfOfHeight; j++)
            {
                for (int i = 0; i < halfOfWidth; i++)
                {
                    int x = i + halfOfWidth;
                    int y = j + halfOfHeight;

                    shifted[x, y] = GaussianKernel[i, j];
                    shifted[i, j] = GaussianKernel[x, y];
                    shifted[x, j] = GaussianKernel[i, y];
                    shifted[i, y] = GaussianKernel[x, j];
                }
            }
            return shifted;
        }

    }
}
