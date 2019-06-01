using ImgLib;
using System;

namespace ImgConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      string inFile = args[0];
      string outFile = args[1];

      Bitmap bmp = Bitmap.FromFile(inFile);

      for (int row = 0; row < bmp.Pixels.NumRows; row++)
      {
        for (int col = 0; col < bmp.Pixels.NumColumns; col++)
        {
          Pixel px = bmp.Pixels[row, col];

          //Simple pixel manipulation example
          px.Red = (byte)Math.Min(255, px.Red + 50);
          px.Green = (byte)Math.Min(255, px.Green + 50);
          px.Blue = (byte)Math.Min(255, px.Blue + 50);
        }
      }

      bmp.SaveFile(outFile);
    }
  }
}
