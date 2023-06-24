using Core.PainterFactory;
using Core.PixelPainter;
using MandelbrotExample3.PixelPainter;
using MandelbrotExample3.Util;

namespace MandelbrotExample3.PainterFactory
{
  internal class AsciiPainterFactory : IPainterFactory
  {
    private readonly StringGrid _stringGrid;

    public AsciiPainterFactory(StringGrid stringGrid)
    {
      _stringGrid = stringGrid;
    }

    public IPixelPainter MakePixelPainter(int row, int col, int? maxRow, int? maxCol)
    {
      return new AsciiPixelPainter(_stringGrid, row, col);
    }
  }
}
