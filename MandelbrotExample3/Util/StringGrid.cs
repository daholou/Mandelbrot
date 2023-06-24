using Core.Util;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace MandelbrotExample3.Util
{
  internal class StringGrid
  {
    private readonly int _width;
    private readonly int _height;
    private readonly string[] _data;
    public StringGrid(int width, int height)
    {
      _width = width;
      _height = height;
      _data = new string[height];
      for (int i = 0; i < height; ++i)
      {
        _data[i] = new string(' ', width);
      }
    }

    public void SetChar(int x, int y, char c)
    {
      int colIndex = ScalarUtils.Clamp(x, 0, _width - 1);
      int rowIndex = ScalarUtils.Clamp(_height - 1 - y, 0, _height - 1);
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
    public void Save(
      int wRes,
      int hRes,
      string filename,
      ImageFormat format
    )
    {
      Bitmap bitmap = new(wRes, hRes);
      float wChar = bitmap.Width / (float)_width;
      float hChar = bitmap.Height / (float)_height;
      Font asciiFont = new(FontFamily.GenericMonospace, wChar*1.0f, GraphicsUnit.Pixel);
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
          graphics.DrawString(symbol, asciiFont, Brushes.White, textLocation);
        }
      }
      bitmap.Save(filename, format);
    }
  }
}
