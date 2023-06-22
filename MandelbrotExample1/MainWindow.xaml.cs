using Core;
using Core.PainterFactory;
using MandelbrotExample1.PainterFactory;
using System.Windows;

namespace MandelbrotExample1
{
  public partial class MainWindow : Window
  {
    private readonly IPainterFactory _painterFactory;

    public MainWindow()
    {
      InitializeComponent();
      _painterFactory = new PainterFactory1(drawingCanvas, 4, 4);
      MandelbrotPixelGrid mandelbrotGrid = new(_painterFactory, 128, 128);
      mandelbrotGrid.UpdateCoordinates(
        new System.Numerics.Complex(-2, 2),
        new System.Numerics.Complex(2, -2)
      );
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
    }
  }
}
