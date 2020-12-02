namespace ImgLib.ColorConversions
{
   public class HSVConverter : IHSVConverter
   {
      private IConverterHelper _converterHelper;

      public HSVConverter(IConverterHelper converterHelper)
      {
         _converterHelper = converterHelper;
      }

      #region RGB to HSV

      public (float Hue, float Saturation, float Value) GetHSV(byte r, byte g, byte b)
      {
         return _converterHelper.CalculateHSV(r, g, b);
      }

      public float GetHue(byte r, byte g, byte b)
      {
         return _converterHelper.CalculateHue(r, g, b);
      }

      public float GetSaturation(byte r, byte g, byte b)
      {
         return _converterHelper.CalculateSaturation(r, g, b);
      }

      public float GetValue(byte r, byte g, byte b)
      {
         return _converterHelper.CalculateValue(r, g, b);
      }

      #endregion

      #region HSV to RGB

      public byte GetRed(float hue, float saturation, float value)
      {
         return _converterHelper.CalculateRed(hue, saturation, value);
      }

      public byte GetGreen(float hue, float saturation, float value)
      {
         return _converterHelper.CalculateGreen(hue, saturation, value);
      }

      public byte GetBlue(float hue, float saturation, float value)
      {
         return _converterHelper.CalculateBlue(hue, saturation, value);
      }

      public (byte Red, byte Green, byte Blue) GetRGB(float hue, float saturation, float value)
      {
         return _converterHelper.CalculateRGB(hue, saturation, value);
      }

      #endregion
   }
}
