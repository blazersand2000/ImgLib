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

      int midX = bmp.Pixels.NumColumns / 2;
      int midY = bmp.Pixels.NumRows / 2;

      for (int row = 0; row < bmp.Pixels.NumRows; row++)
      {
        for (int col = 0; col < bmp.Pixels.NumColumns; col++)
        {
          Pixel px = bmp.Pixels[row, col];

          int distFromMidX = Math.Abs(midX - col);
          int distFromMidY = Math.Abs(midY - row);
          int distance = (int)Math.Sqrt(Math.Pow(distFromMidX, 2) + Math.Pow(distFromMidY, 2));

          //Simple pixel manipulation example
          byte value = (byte)RestrictRange(0, 255, (int)(((float)distance / Math.Max(midY, 1)) * 255) - 100);
          px.Red = value;
          px.Green = value;
          px.Blue = value;
        }
      }

      bmp.SaveFile(outFile);
    }

    private static int RestrictRange(int min, int max, int input)
    {
      return Math.Min(Math.Max(input, min), max);
    }
  }
}
