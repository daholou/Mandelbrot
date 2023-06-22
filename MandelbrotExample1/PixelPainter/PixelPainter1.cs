using Core.PixelPainter;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MandelbrotExample1.PixelPainter
{
  internal class PixelPainter1 : IPixelPainter
  {
    private readonly Rectangle _rectangle;

    public PixelPainter1(Rectangle rectangle)
    {
      _rectangle = rectangle;
    }

    public void Repaint(double divergenceRate)
    {
      Color color = divergenceRate < 0.5
        ? Color.FromRgb(111, 0, 0)
        : Color.FromRgb(0, 111, 111);
      _rectangle.Fill = new SolidColorBrush(color);
    }
  }
}
