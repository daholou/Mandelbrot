using Core.PixelPainter;
using MandelbrotExample2.Util;
using System.Drawing;

namespace MandelbrotExample2.PixelPainter
{

  internal class BitmapPixelPainter : IPixelPainter
  {
    private readonly Bitmap _bitmap;
    private readonly ColorInterpolator _colorInterpolator;
    private readonly int _yPixel;
    private readonly int _xPixel;

    public BitmapPixelPainter(
      Bitmap bitmap,
      ColorInterpolator colorInterpolator,
      int yPixel,
      int xPixel
    )
    {
      _bitmap = bitmap;
      _colorInterpolator = colorInterpolator;
      _yPixel = yPixel;
      _xPixel = xPixel;
    }

    public void Repaint(double divergenceRate)
    {
      Color color = _colorInterpolator.GetColor(divergenceRate);
      if (OperatingSystem.IsWindows())
      {
        _bitmap.SetPixel(_xPixel, _yPixel, color);
      }
    }
  }
}
