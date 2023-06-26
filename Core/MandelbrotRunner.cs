using Core.PainterFactory;
using System.Diagnostics;

namespace Core
{
  public abstract class MandelbrotRunner
  {
    protected readonly int _width;
    protected readonly int _height;
    protected readonly double _ratio;
    protected readonly Stopwatch _stopwatch = new();
    protected readonly IPainterFactory _painterFactory;
    protected readonly MandelbrotPixelGrid _mandelbrotGrid;

    public MandelbrotRunner(
      int width,
      int height,
      IPainterFactory painterFactory
    )
    {
      _width = width;
      _height = height;
      _ratio = _height / (double)_width;
      _painterFactory = painterFactory;
      _mandelbrotGrid = new(painterFactory, width, height);
    }

    public int Width
    {
      get
      {
        return _width;
      }
    }

    public int Height
    {
      get
      {
        return _height;
      }
    }

    public double Ratio
    {
      get
      {
        return _ratio;
      }
    }

    public abstract void Execute(
      ZoomConfiguration zoomConfiguration,
      string folderPath
    );

    public abstract string GetFramesDirectory();
  }
}
