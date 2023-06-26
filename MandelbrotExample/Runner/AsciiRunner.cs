using Core.Frame;
using Core.Runner;
using MandelbrotExample.PainterFactory;
using MandelbrotExample.Util;

namespace MandelbrotExample.Runner
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

    public override string GetFramesDirectory()
    {
      return "ascii-frames";
    }

    public override string GetZoomFrameSequenceFileName(FrameData frameData)
    {
      return $"ascii_frame_{frameData.GetFormattedIndex()}.png";
    }

    public override string GetSingleFrameFileName(int maxIterationCount)
    {
      string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
      return $"ascii_single_frame_{maxIterationCount}_{timeStamp}.png";
    }
  }
}
