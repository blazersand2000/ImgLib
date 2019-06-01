using System;
using System.IO;

namespace ImgLib
{
  public class Bitmap
  {
    private char[] FH_Type { get; set; }
    private uint FH_Size { get; set; }
    private ushort FH_Reserved1 { get; set; }
    private ushort FH_Reserved2 { get; set; }
    private uint FH_OffBits { get; set; }

    private uint IH_Size { get; set; }
    private int IH_Width { get; set; }
    private int IH_Height { get; set; }
    private ushort IH_Planes { get; set; }
    private ushort IH_BitCount { get; set; }
    private uint IH_Compression { get; set; }
    private uint IH_SizeImage { get; set; }
    private int IH_XPelsPerMeter { get; set; }
    private int IH_YPelsPerMeter { get; set; }
    private uint IH_ClrUsed { get; set; }
    private uint IH_ClrImportant { get; set; }

    public PixelMatrix Pixels { get; set; }

    /// <summary>
    /// Creates a <see cref="Bitmap"/> object from a bitmap (.bmp) file
    /// </summary>
    /// <param name="path">The path to the bitmap file</param>
    /// <returns><see cref="Bitmap"/> object created from the file</returns>
    public static Bitmap FromFile(string path)
    {
      if (!path.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
      {
        throw new Exception("Must be a .bmp file");
      }

      Bitmap bmp = new Bitmap();
      byte[] bytes = File.ReadAllBytes(path);

      bmp.FH_Type = new char[2] { (char)bytes[0], (char)bytes[1] };
      bmp.FH_Size = BitConverter.ToUInt32(bytes, 2);
      bmp.FH_Reserved1 = BitConverter.ToUInt16(bytes, 6);
      bmp.FH_Reserved2 = BitConverter.ToUInt16(bytes, 8);
      bmp.FH_OffBits = BitConverter.ToUInt32(bytes, 10);
      bmp.IH_Size = BitConverter.ToUInt32(bytes, 14);
      bmp.IH_Width = BitConverter.ToInt32(bytes, 18);
      bmp.IH_Height = BitConverter.ToInt32(bytes, 22);
      bmp.IH_Planes = BitConverter.ToUInt16(bytes, 26);
      bmp.IH_BitCount = BitConverter.ToUInt16(bytes, 28);
      bmp.IH_Compression = BitConverter.ToUInt32(bytes, 30);
      bmp.IH_SizeImage = BitConverter.ToUInt32(bytes, 34);
      bmp.IH_XPelsPerMeter = BitConverter.ToInt32(bytes, 38);
      bmp.IH_YPelsPerMeter = BitConverter.ToInt32(bytes, 42);
      bmp.IH_ClrUsed = BitConverter.ToUInt32(bytes, 46);
      bmp.IH_ClrImportant = BitConverter.ToUInt32(bytes, 50);

      bmp.Pixels = new PixelMatrix(bmp.IH_Width, bmp.IH_Height);
      int rowOffset, pixelOffset;
      for (int row = 0; row < bmp.IH_Height; row++)
      {
        rowOffset = 54 + (row * (bmp.IH_Width * 3 + DiffFromNextMultiple(bmp.IH_Width * 3, 4)));
        for (int col = 0; col < bmp.IH_Width; col++)
        {
          pixelOffset = rowOffset + (col * 3);
          byte r = bytes[pixelOffset + 2];
          byte g = bytes[pixelOffset + 1];
          byte b = bytes[pixelOffset + 0];
          bmp.Pixels[bmp.IH_Height - 1 - row, col] = new Pixel(r, g, b);
        }
      }

      return bmp;
    }

    /// <summary>
    /// Serializes the <see cref="Bitmap"/> object to a file
    /// </summary>
    /// <param name="path">The path of the bitmap file to write</param>
    public void SaveFile(string path)
    {
      if (!path.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
      {
        throw new Exception("Must be a .bmp file");
      }

      byte[] bytes = new byte[54 + ((IH_Width * 3 + DiffFromNextMultiple(IH_Width * 3, 4)) * IH_Height)];

      BitConverter.GetBytes(FH_Type[0]).CopyTo(bytes, 0);
      BitConverter.GetBytes(FH_Type[1]).CopyTo(bytes, 1);
      BitConverter.GetBytes(FH_Size).CopyTo(bytes, 2);
      BitConverter.GetBytes(FH_Reserved1).CopyTo(bytes, 6);
      BitConverter.GetBytes(FH_Reserved2).CopyTo(bytes, 8);
      BitConverter.GetBytes(FH_OffBits).CopyTo(bytes, 10);
      BitConverter.GetBytes(IH_Size).CopyTo(bytes, 14);
      BitConverter.GetBytes(IH_Width).CopyTo(bytes, 18);
      BitConverter.GetBytes(IH_Height).CopyTo(bytes, 22);
      BitConverter.GetBytes(IH_Planes).CopyTo(bytes, 26);
      BitConverter.GetBytes(IH_BitCount).CopyTo(bytes, 28);
      BitConverter.GetBytes(IH_Compression).CopyTo(bytes, 30);
      BitConverter.GetBytes(IH_SizeImage).CopyTo(bytes, 34);
      BitConverter.GetBytes(IH_XPelsPerMeter).CopyTo(bytes, 38);
      BitConverter.GetBytes(IH_YPelsPerMeter).CopyTo(bytes, 42);
      BitConverter.GetBytes(IH_ClrUsed).CopyTo(bytes, 46);
      BitConverter.GetBytes(IH_ClrImportant).CopyTo(bytes, 50);

      int rowOffset, pixelOffset = 0;
      int rowWidth = IH_Width * 3 + DiffFromNextMultiple(IH_Width * 3, 4);
      for (int row = 0; row < IH_Height; row++)
      {
        rowOffset = 54 + (row * rowWidth);
        for (int col = 0; col < IH_Width; col++)
        {
          pixelOffset = rowOffset + (col * 3);
          bytes[pixelOffset + 2] = Pixels[IH_Height - 1 - row, col].Red;
          bytes[pixelOffset + 1] = Pixels[IH_Height - 1 - row, col].Green;
          bytes[pixelOffset + 0] = Pixels[IH_Height - 1 - row, col].Blue;
        }
        for (int paddingIndex = pixelOffset + 3; paddingIndex < rowOffset + rowWidth; paddingIndex++)
        {
          bytes[paddingIndex] = 0;
        }
      }
      File.WriteAllBytes(path, bytes);
    }

    /// <summary>
    /// Gives the difference between a given number and the next multiple of a given multiple 
    /// </summary>
    /// <param name="number">The starting number</param>
    /// <param name="multiple">The multiple to use</param>
    /// <returns>The difference between the given number and the next multiple</returns>
    private static int DiffFromNextMultiple(int number, int multiple)
    {
      return number % multiple == 0 ? 0 : multiple - (number % multiple);
    }
  }
}
