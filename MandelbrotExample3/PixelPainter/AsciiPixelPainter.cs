using Core.PixelPainter;
using Core.Util;
using MandelbrotExample3.Util;

namespace MandelbrotExample3.PixelPainter
{
  internal class AsciiPixelPainter : IPixelPainter
  {
    private readonly StringGrid _stringGrid;
    private readonly int _yPixel;
    private readonly int _xPixel;
    private static readonly string DENSITY = " .'`^\",:;Il!i><~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";

    public AsciiPixelPainter(StringGrid stringGrid, int yPixel, int xPixel)
    {
      _stringGrid = stringGrid;
      _yPixel = yPixel;
      _xPixel = xPixel;
    }

    private static char GenerateChar(double divergenceRate)
    {
      int index = (int)Math.Floor((DENSITY.Length - 1) * divergenceRate);
      int safeIndex = ScalarUtils.Clamp(index, 0, DENSITY.Length - 1);
      return DENSITY.ElementAt(safeIndex);
    }

    public void Repaint(double divergenceRate)
    {
      _stringGrid.SetChar(_xPixel, _yPixel, GenerateChar(divergenceRate));
    }
  }
}
