using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace CustomFilterBank_Test
{
    public partial class Tools
    {
        public static Complex[,] MultiplyComplex(Complex[,] comp1, Complex[,] comp2)
        {
            int Width = comp1.GetLength(0);
            int Height = comp1.GetLength(1);

            Complex[,] Output = new Complex[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Output[i, j] = comp1[i, j] * comp2[i, j];
                }
            }

            return Output;
        }
    }
}
