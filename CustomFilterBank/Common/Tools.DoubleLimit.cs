using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public partial class Tools
    {
        public static double[,] Limit(double[,] matrix, double min, double max)
        {
            int Width = matrix.GetLength(0);
            int Height = matrix.GetLength(1);

            double[,] Output = new double[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Output[i, j] = Math.Max(0, Math.Min(matrix[i, j], max));
                }
            }

            return Output;
        }
    }
}
