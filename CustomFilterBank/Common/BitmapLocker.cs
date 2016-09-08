using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CustomFilterBank_Test
{
    //http://www.codeproject.com/Tips/240428/Work-with-Bitmaps-Faster-in-Csharp
    public class BitmapLocker
    {
        //private properties
        Bitmap _bitmap = null;
        bool _isLocked = false;
        BitmapData _bitmapData = null;
        private byte[] _imageData = null;

        //public properties
        public IntPtr IntegerPointer { get; private set; }
        public int Width { get { return _bitmap.Width; } }
        public int Height { get { return _bitmap.Height; } }
        public int Stride { get { return _bitmapData.Stride; } }
        public int ColorDepth { get { return Bitmap.GetPixelFormatSize(_bitmap.PixelFormat); } }
        public int PaddingOffset { get { return _bitmapData.Stride - (_bitmap.Width * ColorDepth / 8); } }
        public PixelFormat ImagePixelFormat { get { return _bitmap.PixelFormat; } }
        public bool IsGrayscale { get { return Grayscale.IsGrayscale(_bitmap); } }

        //Constructor
        public BitmapLocker(Bitmap source)
        {
            IntegerPointer = IntPtr.Zero;
            this._bitmap = source;
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = ColorDepth / 8;

            // Get start index of the specified pixel
            int i = (Height - y - 1) * Stride + x * cCount;

            if (i > _imageData.Length - cCount)
            {
                throw new IndexOutOfRangeException();
            }

            if (ColorDepth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = _imageData[i];
                byte g = _imageData[i + 1];
                byte r = _imageData[i + 2];
                byte a = _imageData[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (ColorDepth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = _imageData[i];
                byte g = _imageData[i + 1];
                byte r = _imageData[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (ColorDepth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = _imageData[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /*
        //https://github.com/LuizZak/FastBitmap/blob/master/FastBitmap.cs
        public void SetPixel(int x, int y, Color color)
        {
            SetPixel(x, y, color.ToArgb());
        }

        public void SetPixel(int x, int y, int color)
        {
            SetPixel(x, y, (uint)color);
        }

        public unsafe void SetPixel(int x, int y, uint color)
        {
            if (!_isLocked)
            {
                throw new InvalidOperationException("The FastBitmap must be locked before any pixel operations are made");
            }

            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException("The X component must be >= 0 and < width");
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException("The Y component must be >= 0 and < height");
            }

           // uint* address = (uint*)IntegerPointer.ToPointer();

            *(uint*)(IntegerPointer + x + y * Stride) = color;
        }*/

        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = ColorDepth / 8;

            // Get start index of the specified pixel
            int i = (Height - y ) * Stride + x * cCount;

            if (ColorDepth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                _imageData[i] = color.B;
                _imageData[i + 1] = color.G;
                _imageData[i + 2] = color.R;
                _imageData[i + 3] = color.A;
            }
            if (ColorDepth == 24) // For 24 bpp set Red, Green and Blue
            {
                _imageData[i] = color.B;
                _imageData[i + 1] = color.G;
                _imageData[i + 2] = color.R;
            }
            if (ColorDepth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                _imageData[i] = color.B;
            }
        }

        /// Lock bitmap
        public void Lock()
        {
            if (_isLocked == false)
            {
                try
                {
                    // Lock bitmap (so that no movement of data by .NET framework) and return bitmap data
                    _bitmapData = _bitmap.LockBits(
                                                    new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                                                    ImageLockMode.ReadWrite,
                                                    _bitmap.PixelFormat);

                    // Create byte array to copy pixel values
                    int noOfBitsNeededForStorage = _bitmapData.Stride * _bitmapData.Height;

                    _imageData = new byte[noOfBitsNeededForStorage * ColorDepth / 8];//# of bytes needed for storage

                    IntegerPointer = _bitmapData.Scan0;

                    // Copy data from __IntegerPointer to PixelArray
                    Marshal.Copy(IntegerPointer, _imageData, 0, _imageData.Length);

                    _isLocked = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Bitmap is already locked.");
            }
        }

        /// Unlock bitmap
        public void Unlock()
        {
            if (_isLocked == true)
            {
                try
                {
                    // Copy data from PixelArray to __IntegerPointer
                    Marshal.Copy(_imageData, 0, IntegerPointer, _imageData.Length);

                    // Unlock bitmap data
                    _bitmap.UnlockBits(_bitmapData);

                    _isLocked = false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("Bitmap is not locked.");
            }
        }

        //public void Show()
        //{
        //    if (_isLocked == true)
        //    {
        //        Console.WriteLine("ImagePixelFormat = " + ImagePixelFormat.ToString());
        //        Console.WriteLine("Width = " + Width + " pixels");
        //        Console.WriteLine("Height = " + Height + " pixels");
        //        Console.WriteLine("_imageData.Length = " + _imageData.Length + " memorySize");
        //        Console.WriteLine("Stride = " + Stride + " memorySize");
        //        Console.WriteLine("Color Depth = " + ColorDepth + " bits");
        //        Console.WriteLine("PaddingOffset = " + PaddingOffset + " memorySize");
        //        Console.WriteLine();
        //    }
        //    else
        //    {
        //        throw new Exception("Bitmap is not locked.");
        //    }
        //}
    }
}
