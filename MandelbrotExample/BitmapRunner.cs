using Core;
using Core.Util;
using MandelbrotExample.PainterFactory;
using MandelbrotExample.Util;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace MandelbrotExample
{
  [SupportedOSPlatform("windows")]
  public class BitmapRunner
  {
    private readonly int _width;
    private readonly int _height;
    private readonly int _paletteSize;
    private readonly PositionColor[] _breakpoints;
    private readonly Bitmap _bitmap;
    private readonly ColorInterpolator _colorInterpolator;
    private readonly BitmapPainterFactory _painterFactory;
    private readonly MandelbrotPixelGrid _mandelbrotGrid;
    private readonly Stopwatch _stopwatch;

    public BitmapRunner(
      int width,
      int height,
      int paletteSize,
      PositionColor[] breakpoints
    )
    {
      _width = width;
      _height = height;
      _paletteSize = paletteSize;
      _breakpoints = new PositionColor[breakpoints.Length];
      for (int i = 0; i < breakpoints.Length; i++)
      {
        _breakpoints[i] = breakpoints[i];
      }
      _bitmap = new(_width, _height);
      _colorInterpolator = new(_breakpoints, _paletteSize);
      _painterFactory = new(_bitmap, _colorInterpolator);
      _mandelbrotGrid = new(_painterFactory, _width, _height);
      _stopwatch = new();
    }

    public void Execute(
      ZoomConfiguration zoomConfiguration,
      string folderPath
    )
    {
      for (int k = 0; k < zoomConfiguration.Size(); ++k)
      {
        Configuration configuration = zoomConfiguration.ConfigurationAt(k);
        FrameData frame = zoomConfiguration.FrameAt(k);
        Console.WriteLine($"  Working on frame {frame.FrameIndex} with {frame.MaxIterationCount} iterations...");
        _stopwatch.Restart();
        _mandelbrotGrid.UpdateConfiguration(configuration);
        string filename = $"bitmap_frame_{frame.GetFormattedIndex()}.png";
        _bitmap.Save(FileUtils.GetFilePath(folderPath, filename), ImageFormat.Png);
        _stopwatch.Stop();
        Console.WriteLine($"  - bitmap created in : {_stopwatch.ElapsedMilliseconds} ms");
      }
    }
  }
}
