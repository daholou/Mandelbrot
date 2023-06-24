using Core.PainterFactory;
using Core.PixelPainter;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Core
{
  /// <summary>
  /// Represents a grid of pixels that covers a rectangle (complex coordinates)
  /// </summary>
  public class MandelbrotPixelGrid
  {
    private readonly int _widthInPixels;
    private readonly int _heightInPixels;
    private Complex _topLeftCoordinates;
    private Complex _bottomRightCoordinates;
    private readonly List<MandelbrotPixel> _pixels = new();

    public MandelbrotPixelGrid(
      IPainterFactory painterFactory,
      int widthInPixels = 20,
      int heightInPixels = 20
    )
    {
      _widthInPixels = widthInPixels;
      _heightInPixels = heightInPixels;
      _pixels = new();
      for (int i = 0; i < heightInPixels; ++i)
      {
        for (int j = 0; j < widthInPixels; ++j)
        {
          IPixelPainter pixelPainter = painterFactory.MakePixelPainter(
            i,
            j,
            _heightInPixels,
            _widthInPixels
          );
          _pixels.Add(new MandelbrotPixel(pixelPainter, 0));
        }
      }
    }

    public void UpdateConfiguration(Configuration configuration)
    {
      _topLeftCoordinates = configuration.Center
        + new Complex(-configuration.HorizontalRadius, configuration.VerticalRadius);
      _bottomRightCoordinates = configuration.Center
        + new Complex(configuration.HorizontalRadius, -configuration.VerticalRadius);
      Complex delta = _bottomRightCoordinates - _topLeftCoordinates;
      Complex ux = new(delta.Real / _widthInPixels, 0);
      Complex uy = new(0, delta.Imaginary / _heightInPixels);
      Complex origin = _topLeftCoordinates + (uy + ux) / 2;

      for (int i = 0; i < _heightInPixels; ++i)
      {
        for (int j = 0; j < _widthInPixels; ++j)
        {
          MandelbrotPixel pixel = _pixels[i * _widthInPixels + j];
          pixel.Update(origin + uy * i + ux * j, configuration.MaxIterationCount);
          pixel.Repaint();
        }
      }
    }
  }
}
