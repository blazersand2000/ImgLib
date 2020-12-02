namespace ImgLib.ColorConversions
{
   public interface IHSVConverter
   {
      (float Hue, float Saturation, float Value) GetHSV(byte r, byte g, byte b);
      float GetHue(byte r, byte g, byte b);
      float GetSaturation(byte r, byte g, byte b);
      float GetValue(byte r, byte g, byte b);
      (byte Red, byte Green, byte Blue) GetRGB(float hue, float saturation, float value);
      byte GetRed(float hue, float saturation, float value);
      byte GetGreen(float hue, float saturation, float value);
      byte GetBlue(float hue, float saturation, float value);
   }
}