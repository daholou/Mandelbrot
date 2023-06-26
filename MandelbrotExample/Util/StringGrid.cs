using Core.Util;
using System.Drawing;
using System.Runtime.Versioning;

namespace MandelbrotExample.Util
{
  public class StringGrid
  {
    private readonly int _width;
    private readonly int _height;
    private readonly int _horizontalResolution;
    private readonly int _verticalResolution;
    private readonly string[] _data;

    public StringGrid(
      int width,
      int height,
      int horizontalResolution,
      int verticalResolution
    )
    {
      _width = width;
      _height = height;
      _horizontalResolution = horizontalResolution;
      _verticalResolution = verticalResolution;
      _data = new string[height];
      for (int i = 0; i < height; ++i)
      {
        _data[i] = new string(' ', width);
      }
    }

    public void SetChar(int x, int y, char c)
    {
      int colIndex = ScalarUtils.Clamp(x, 0, _width - 1);
      int rowIndex = ScalarUtils.Clamp(y, 0, _height - 1);
      _data[rowIndex] = _data[rowIndex].Remove(colIndex, 1).Insert(colIndex, $"{c}");
    }

    public void Display()
    {
      for (int i = 0; i < _data.Length; ++i)
      {
        Console.WriteLine(_data[i]);
      }
    }

    [SupportedOSPlatform("windows")]
    public void Save(string filePath)
    {
      Bitmap bitmap = new(_horizontalResolution, _verticalResolution);
      float wChar = bitmap.Width / (float)_width;
      float hChar = bitmap.Height / (float)_height;
      Font asciiFont = new("Courier", (int)Math.Floor(wChar * 0.75), GraphicsUnit.Pixel);
      StringFormat drawFormat = new()
      {
        Alignment = StringAlignment.Center
      };
      Graphics graphics = Graphics.FromImage(bitmap);
      graphics.Clear(Color.Black);
      PointF textLocation = new(0.0f, 0.0f);
      for (int i = 0; i < _height; ++i)
      {
        textLocation.Y = i * hChar;
        textLocation.X = 0;
        for (int j = 0; j < _width; ++j)
        {
          textLocation.X = j * wChar;
          string symbol = "" + _data[i].ElementAt(j);
          graphics.DrawString(symbol, asciiFont, Brushes.White, textLocation, drawFormat);
        }
      }
      bitmap.Save(filePath);
    }
  }
}
