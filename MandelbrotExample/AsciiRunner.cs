using Core;
using Core.Util;
using MandelbrotExample.PainterFactory;
using MandelbrotExample.Util;
using System.Runtime.Versioning;

namespace MandelbrotExample
{
  public class AsciiRunner : MandelbrotRunner
  {
    private static AsciiPainterFactory BuildPainterFactory(
      int width,
      int height,
      int horizontalResolution,
      int verticalResolution
    )
    {
      StringGrid stringGrid = new(width, height, horizontalResolution, verticalResolution);
      return new AsciiPainterFactory(stringGrid);
    }

    public AsciiRunner(
      int width,
      int height,
      int horizontalResolution,
      int verticalResolution
    ) : base(width, height, BuildPainterFactory(width, height, horizontalResolution, verticalResolution))
    {
    }

    [SupportedOSPlatform("windows")]
    public override void Execute(
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
        string filename = $"ascii_frame_{frame.GetFormattedIndex()}.png";
        _painterFactory.Save(FileUtils.GetFilePath(folderPath, filename));
        _stopwatch.Stop();
        Console.WriteLine($"  - bitmap created in : {_stopwatch.ElapsedMilliseconds} ms");
      }
    }

    public override string GetFramesDirectory()
    {
      return "ascii-frames";
    }
  }
}
