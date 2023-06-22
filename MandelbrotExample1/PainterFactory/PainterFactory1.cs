using Core.PainterFactory;
using Core.PixelPainter;
using MandelbrotExample1.PixelPainter;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MandelbrotExample1.PainterFactory
{
  internal class PainterFactory1 : IPainterFactory
  {
    private readonly Canvas _canvas;
    private readonly int _pixelWidth;
    private readonly int _pixelHeight;

    public PainterFactory1(
      Canvas canvas,
      int pixelWidth = 20,
      int pixelHeight = 20
    )
    {
      _canvas = canvas;
      _pixelWidth = pixelWidth;
      _pixelHeight = pixelHeight;
    }

    public IPixelPainter MakePixelPainter(int row, int col, int? maxRow, int? maxCol)
    {
      Rectangle rectangle = new()
      {
        Width = _pixelWidth,
        Height = _pixelHeight
      };
      _canvas.Children.Add(rectangle);
      Canvas.SetTop(rectangle, row * _pixelHeight);
      Canvas.SetLeft(rectangle, col * _pixelWidth);
      return new PixelPainter1(rectangle);
    }
  }
}
