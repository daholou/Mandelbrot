using Core.Frame;
using Core.PainterFactory;
using Core.PixelGrid;
using Core.Util;
using System.Diagnostics;

namespace Core.Runner
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

    public void SaveZoomSequence(
      FrameZoomSequence frameZoomSequence,
      string folderPath
    )
    {

      for (int k = 0; k < frameZoomSequence.Size; ++k)
      {
        FrameConfiguration configuration = frameZoomSequence.FrameConfigurationAt(k);
        FrameData frameData = frameZoomSequence.FrameDataAt(k);
        Console.WriteLine($" > Working on frame {frameData.FrameIndex} / {frameZoomSequence.LastFrameIndex} ...");
        string filename = GetZoomFrameSequenceFileName(frameData);
        SaveFrame(configuration, folderPath, filename);
      }
    }

    public void SaveSingleFrame(
      FrameConfiguration configuration,
      string folderPath
    )
    {
      string filename = GetSingleFrameFileName(configuration.MaxIterationCount);
      SaveFrame(configuration, folderPath, filename);
    }

    protected void SaveFrame(
      FrameConfiguration configuration,
      string folderPath,
      string filename
    )
    {
      Console.WriteLine($"  Working on a single frame with {configuration.MaxIterationCount} iterations...");
      _stopwatch.Restart();
      _mandelbrotGrid.UpdateConfiguration(configuration);
      _painterFactory.Save(FileUtils.GetFilePath(folderPath, filename));
      _stopwatch.Stop();
      Console.WriteLine($"  - frame saved in : {_stopwatch.ElapsedMilliseconds} ms");
    }

    public abstract string GetFramesDirectory();

    public abstract string GetZoomFrameSequenceFileName(FrameData frameData);

    public abstract string GetSingleFrameFileName(int maxIterationCount);
  }
}
