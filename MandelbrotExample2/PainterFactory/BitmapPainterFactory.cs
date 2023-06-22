using Core.PainterFactory;
using Core.PixelPainter;
using MandelbrotExample2.PixelPainter;
using MandelbrotExample2.Util;
using System.Drawing;

namespace MandelbrotExample2.PainterFactory
{

  internal class BitmapPainterFactory : IPainterFactory
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

    public IPixelPainter MakePixelPainter(int row, int col, int? maxRow, int? maxCol)
    {
      return new BitmapPixelPainter(
        _bitmap,
        _colorInterpolator,
        row,
        col
      );
    }
  }
}
