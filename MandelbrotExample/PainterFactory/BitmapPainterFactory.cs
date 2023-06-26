using Core.PainterFactory;
using Core.PixelPainter;
using MandelbrotExample.PixelPainter;
using MandelbrotExample.Util;
using System.Drawing;
using System.Runtime.Versioning;

namespace MandelbrotExample.PainterFactory
{
  public class BitmapPainterFactory : IPainterFactory
  {
    private readonly Bitmap _bitmap;
    private readonly ColorInterpolator _colorInterpolator;

    public BitmapPainterFactory(
      Bitmap bitmap,
      ColorInterpolator colorInterpolator
    )
    {
      _bitmap = bitmap;
      _colorInterpolator = colorInterpolator;
    }

    public IPixelPainter MakePixelPainter(int row, int col)
    {
      return new BitmapPixelPainter(
        _bitmap,
        _colorInterpolator,
        row,
        col
      );
    }

    [SupportedOSPlatform("windows")]
    public void Save(string filePath)
    {
      _bitmap.Save(filePath);
    }
  }
}
