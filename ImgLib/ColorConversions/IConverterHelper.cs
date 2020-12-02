namespace ImgLib
{
   public interface IConverterHelper
   {
      (float Hue, float Saturation, float Value) CalculateHSV(byte r, byte g, byte b);
      float CalculateHue(byte r, byte g, byte b);
      float CalculateSaturation(byte r, byte g, byte b);
      float CalculateValue(byte r, byte g, byte b);
      byte CMax(byte red, byte green, byte blue);
      float CMax(float red, float green, float blue);
      byte CMin(byte red, byte green, byte blue);
      float CMin(float red, float green, float blue);
      float ConvertByteValueToFraction(byte b);
      byte ConvertFractionValueToByte(float f);
      byte Delta(byte cMin, byte cMax);
      byte Delta(byte r, byte g, byte b);
      float Delta(float cMin, float cMax);
      float NormalizeAngle(float angle);
      byte CalculateRed(float hue, float saturation, float value);
      byte CalculateGreen(float hue, float saturation, float value);
      byte CalculateBlue(float hue, float saturation, float value);
      (byte Red, byte Green, byte Blue) CalculateRGB(float hue, float saturation, float value);
      float C(float saturation, float value);
      float X(float hue, float saturation, float value);
      float M(float saturation, float value);
   }
}