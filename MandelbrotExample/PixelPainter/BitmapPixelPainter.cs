using Core.PixelPainter;
using MandelbrotExample.Util;
using System.Drawing;
using System.Runtime.Versioning;

namespace MandelbrotExample.PixelPainter
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

    [SupportedOSPlatform("windows")]
    public void Repaint(double divergenceRate)
    {
      Color color = _colorInterpolator.GetColor(divergenceRate);
      _bitmap.SetPixel(_xPixel, _yPixel, color);
    }
  }
}
