using System.Collections.Generic;

namespace ImgLib
{
  public class PixelMatrix
  {
    private List<List<Pixel>> pixels;

    /// <summary>
    /// Access the <see cref="Pixel"/> at the specified row and column
    /// </summary>
    /// <param name="row">Zero-based row index</param>
    /// <param name="col">Zero-based column index</param>
    /// <returns></returns>
    public Pixel this[int row, int col]
    {
      get { return pixels[row][col]; }
      set { pixels[row][col] = value; }
    }

    /// <summary>
    /// Access the <see cref="Pixel"/> at the specified index
    /// </summary>
    /// <param name="index">Zero-based index</param>
    /// <returns></returns>
    public Pixel this[int index]
    {
      get { return pixels[index / pixels.Count][index % pixels.Count]; }
      set { pixels[index / pixels.Count][index % pixels.Count] = value; }
    }

    /// <summary>
    /// Gets the number of rows in the <see cref="PixelMatrix"/>
    /// </summary>
    public int NumRows
    {
      get => pixels.Count;
    }

    /// <summary>
    /// Gets the number of columns in the <see cref="PixelMatrix"/>
    /// </summary>
    public int NumColumns
    {
      get => pixels.Count == 0 ? 0 : (pixels[0] == null ? 0 : pixels[0].Count);
    }

    /// <summary>
    /// Creates an empty <see cref="PixelMatrix"/>
    /// </summary>
    public PixelMatrix() : this(0, 0)
    {
      
    }

    /// <summary>
    /// Creates a new <see cref="PixelMatrix"/> with the specified dimensions
    /// </summary>
    /// <param name="width">Number of pixels pixels horizontally</param>
    /// <param name="height">Number of pixels vertically</param>
    public PixelMatrix(int width, int height)
    {
      pixels = GeneratePixelMatrix(width, height);
    }

    private List<List<Pixel>> GeneratePixelMatrix(int width, int height)
    {
      List<List<Pixel>> matrix = new List<List<Pixel>>(height);
      for (int row = 0; row < height; row++)
      {
        matrix.Add(new List<Pixel>(width));
        for (int column = 0; column < width; column++)
        {
          matrix[row].Add(new Pixel(0, 0, 0));
        }
      }

      return matrix;
    }
  }
}