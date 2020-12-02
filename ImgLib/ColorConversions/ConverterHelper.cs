using System;
using System.Collections.Generic;
using System.Text;

namespace ImgLib
{
   public class ConverterHelper : IConverterHelper
   {
      public float ConvertByteValueToFraction(byte b)
      {
         return (float)b / byte.MaxValue;
      }

      public byte ConvertFractionValueToByte(float f)
      {
         return (byte)Math.Round(f * byte.MaxValue);
      }

      public float CMax(float red, float green, float blue)
      {
         return Math.Max(Math.Max(red, green), blue);
      }

      public byte CMax(byte red, byte green, byte blue)
      {
         return Math.Max(Math.Max(red, green), blue);
      }

      public float CMin(float red, float green, float blue)
      {
         return Math.Min(Math.Min(red, green), blue);
      }

      public byte CMin(byte red, byte green, byte blue)
      {
         return Math.Min(Math.Min(red, green), blue);
      }

      public float Delta(float cMin, float cMax)
      {
         return cMax - cMin;
      }

      public byte Delta(byte cMin, byte cMax)
      {
         return (byte)(cMax - cMin);
      }

      public byte Delta(byte r, byte g, byte b)
      {
         var cMin = CMin(r, g, b);
         var cMax = CMax(r, g, b);
         return Delta(cMin, cMax);
      }

      public float NormalizeAngle(float angle)
      {
         return ((angle % 360) + 360) % 360;
      }

      public float CalculateHue(byte r, byte g, byte b)
      {
         var delta = Delta(r, g, b);
         var cMax = CMax(r, g, b);

         if (delta == 0)
         {
            return 0f;
         }
         else if (cMax == r)
         {
            return NormalizeAngle(HueCalculationCMaxIsRed(g, b, delta));
         }
         else if (cMax == g)
         {
            return NormalizeAngle(HueCalculationCMaxIsGreen(r, b, delta));
         }
         else if (cMax == b)
         {
            return NormalizeAngle(HueCalculationCmaxIsBlue(r, g, delta));
         }

         throw new InvalidOperationException("While calculating hue, an unexpected max value was calculated for the RGB triple.");
      }

      public float CalculateSaturation(byte r, byte g, byte b)
      {
         var cMax = CMax(r, g, b);

         if (cMax == 0)
         {
            return 0;
         }
         else
         {
            return ConvertByteValueToFraction(Delta(r, g, b)) / ConvertByteValueToFraction(cMax);
         }
      }

      public float CalculateValue(byte r, byte g, byte b)
      {
         return ConvertByteValueToFraction(CMax(r, g, b));
      }

      public (float Hue, float Saturation, float Value) CalculateHSV(byte r, byte g, byte b)
      {
         return (CalculateHue(r, g, b), CalculateSaturation(r, g, b), CalculateValue(r, g, b));
      }

      private float HueCalculationCmaxIsBlue(byte r, byte g, byte delta)
      {
         return 60f * (((ConvertByteValueToFraction(r) - ConvertByteValueToFraction(g)) / ConvertByteValueToFraction(delta)) + 4);
      }

      private float HueCalculationCMaxIsGreen(byte r, byte b, byte delta)
      {
         return 60f * (((ConvertByteValueToFraction(b) - ConvertByteValueToFraction(r)) / ConvertByteValueToFraction(delta)) + 2);
      }

      private float HueCalculationCMaxIsRed(byte g, byte b, byte delta)
      {
         return 60f * (((ConvertByteValueToFraction(g) - ConvertByteValueToFraction(b)) / ConvertByteValueToFraction(delta)) % 6);
      }

      public byte CalculateRed(float hue, float saturation, float value)
      {
         return CalculateRGB(hue, saturation, value).Red;
      }

      public byte CalculateGreen(float hue, float saturation, float value)
      {
         return CalculateRGB(hue, saturation, value).Green;
      }

      public byte CalculateBlue(float hue, float saturation, float value)
      {
         return CalculateRGB(hue, saturation, value).Blue;
      }

      public (byte Red, byte Green, byte Blue) CalculateRGB(float hue, float saturation, float value)
      {
         var c = C(saturation, value);
         var x = X(hue, saturation, value);
         var m = M(saturation, value);
         (float RPrime, float GPrime, float BPrime) rgbPrimes;

         switch (hue)
         {
            case float h when h >= 0f && h < 60f:
               rgbPrimes = (c, x, 0f);
               break;
            case float h when h >= 60f && h < 120f:
               rgbPrimes = (x, c, 0f);
               break;
            case float h when h >= 120f && h < 180f:
               rgbPrimes = (0f, c, x);
               break;
            case float h when h >= 180f && h < 240f:
               rgbPrimes = (0f, x, c);
               break;
            case float h when h >= 240f && h < 300f:
               rgbPrimes = (x, 0f, c);
               break;
            case float h when h >= 300f && h < 360f:
               rgbPrimes = (c, 0f, x);
               break;
            default:
               throw new ArgumentOutOfRangeException("Hue must be >= 0.0 and < 360.0.");
         }

         return (ConvertFractionValueToByte(rgbPrimes.RPrime + m), ConvertFractionValueToByte(rgbPrimes.GPrime + m), ConvertFractionValueToByte(rgbPrimes.BPrime + m));

      }

      public float C(float saturation, float value)
      {
         return value * saturation;
      }

      public float X(float hue, float saturation, float value)
      {
         var c = C(saturation, value);
         return c * (1 - Math.Abs(((hue / 60f) % 2) - 1));
      }

      public float M(float saturation, float value)
      {
         return value - C(saturation, value);
      }
   }
}
