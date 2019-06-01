namespace ImgLib
{
  public class Pixel
  {
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