using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public enum NormalizeType
    {
        Magnitude, Phase
    }

    public partial class FourierNormalizer
    {
        public static int[,] Normalize(Complex[,] Output, NormalizeType normalizeType)
        {
            int Width = Output.GetLength(0);
            int Height = Output.GetLength(1);

            double[,] FourierDouble = new double[Width, Height];
            double[,] FourierLogDouble = new double[Width, Height];
            int[,] FourierNormalizedInteger = new int[Width, Height];

            double max = 0;

            if (normalizeType == NormalizeType.Magnitude)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        FourierDouble[i, j] = Output[i, j].Magnitude;
                        FourierLogDouble[i, j] = (double)Math.Log(1 + FourierDouble[i, j]);
                    }
                }

                max = FourierLogDouble[0, 0];
            }
            else
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        FourierDouble[i, j] = Output[i, j].Phase;
                        FourierLogDouble[i, j] = (double)Math.Log(1 + Math.Abs(FourierDouble[i, j]));
                    }
                }

                FourierLogDouble[0, 0] = 0;
                max = FourierLogDouble[1, 1];
            }

            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    if (FourierLogDouble[i, j] > max)
                    {
                        max = FourierLogDouble[i, j];
                    }
                }
            }

            for (int i = 0; i <= Width - 1; i++)
            {
                for (int j = 0; j <= Height - 1; j++)
                {
                    FourierLogDouble[i, j] = FourierLogDouble[i, j] / max;
                }
            }

            if (normalizeType == NormalizeType.Magnitude)
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        FourierNormalizedInteger[i, j] = (int)(2000 * FourierLogDouble[i, j]);
                    }
                }
            }
            else
            {
                for (int i = 0; i <= Width - 1; i++)
                {
                    for (int j = 0; j <= Height - 1; j++)
                    {
                        FourierNormalizedInteger[i, j] = (int)(255 * FourierLogDouble[i, j]);
                    }
                }
            }

            return FourierNormalizedInteger;
        }
    }
}
