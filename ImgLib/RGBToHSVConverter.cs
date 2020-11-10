using System;
using System.Collections.Generic;
using System.Text;

namespace ImgLib
{
   public class RGBToHSVConverter
   {


      //public RGBToHSVConverter(byte red, byte green, byte blue)
      //{

      //}

      public float ConvertByteValueToFraction(byte b)
      {
         return (float)b/255;
      }

      public float CMax(float red, float green, float blue)
      {
         return Math.Max(Math.Max(red, green), blue);
      }

      public float CMin(float red, float green, float blue)
      {
         return Math.Min(Math.Min(red, green), blue);
      }

      public float Delta(float cMin, float cMax)
      {
         return cMax - cMin;
      }
   }
}
