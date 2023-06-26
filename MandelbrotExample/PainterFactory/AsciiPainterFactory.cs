using Core.PainterFactory;
using Core.PixelPainter;
using MandelbrotExample.PixelPainter;
using MandelbrotExample.Util;
using System.Runtime.Versioning;

namespace MandelbrotExample.PainterFactory
{
  public class AsciiPainterFactory : IPainterFactory
  {
    private readonly StringGrid _stringGrid;

    public AsciiPainterFactory(StringGrid stringGrid)
    {
      _stringGrid = stringGrid;
    }

    public IPixelPainter MakePixelPainter(int row, int col)
    {
      return new AsciiPixelPainter(_stringGrid, row, col);
    }

    [SupportedOSPlatform("windows")]
    public void Save(string filePath)
    {
      _stringGrid.Save(filePath);
    }
  }
}
