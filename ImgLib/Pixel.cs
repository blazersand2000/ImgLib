using ImgLib.ColorConversions;

namespace ImgLib
{
   public class Pixel
   {
      private HSVConverter _converter = new HSVConverter(ColorConversionsFactory.MakeColorConverter());
      
      /// <summary>
      /// Red component
      /// </summary>
      public byte Red { get; set; } = 0;

      /// <summary>
      /// Green component
      /// </summary>
      public byte Green { get; set; } = 0;

      /// <summary>
      /// Blue component
      /// </summary>
      public byte Blue { get; set; } = 0;

      public (byte Red, byte Green, byte Blue) RGB
      {
         get => (Red, Green, Blue);
         set
         {
            Red = value.Red;
            Green = value.Green;
            Blue = value.Blue;
         }
      }

      /// <summary>
      /// Hue
      /// </summary>
      public float Hue
      {
         get => _converter.GetHue(Red, Green, Blue);
         set => HSV = (value, Saturation, Value);
      }

      /// <summary>
      /// Saturation
      /// </summary>
      public float Saturation
      {
         get => _converter.GetSaturation(Red, Green, Blue);
         set => HSV = (Hue, value, Value);
      }

      /// <summary>
      /// Value
      /// </summary>
      public float Value
      {
         get => _converter.GetValue(Red, Green, Blue);
         set => HSV = (Hue, Saturation, value);
      }

      /// <summary>
      /// Hue, Saturation, Value triple
      /// </summary>
      public (float Hue, float Saturation, float Value) HSV
      {
         get => _converter.GetHSV(Red, Green, Blue);
         set => RGB = _converter.GetRGB(value.Hue, value.Saturation, value.Value);
      }


      /// <summary>
      /// Creates a <see cref="Pixel"/> out of the RGB triple
      /// </summary>
      /// <param name="r">red component</param>
      /// <param name="g">green component</param>
      /// <param name="b">blue component</param>
      public Pixel(byte r, byte g, byte b)
      {
         Red = r;
         Green = g;
         Blue = b;
      }

      
   }
}