namespace Core
{

  public class FrameData
  {
    private readonly int _frameIndex;
    private readonly int _maxIterationCount;

    public FrameData(
      int frameIndex,
      int maxIterationCount
    )
    {
      _frameIndex = frameIndex;
      _maxIterationCount = maxIterationCount;
    }

    public int FrameIndex
    {
      get
      {
        return _frameIndex;
      }
    }

    public int MaxIterationCount
    {
      get
      {
        return _maxIterationCount;
      }
    }

    public static List<FrameData> InterpolateBetween(
      FrameData firstFrame,
      FrameData lastFrame
    )
    {
      List<FrameData> frames = new();

      // safeguard
      if (firstFrame.FrameIndex == lastFrame.FrameIndex)
      {
        return frames;
      }

      int sign = firstFrame.FrameIndex < lastFrame.FrameIndex ? 1 : -1;
      int numberOfFrames = Math.Abs(lastFrame.FrameIndex - firstFrame.FrameIndex);
      int deltaIterations = lastFrame.MaxIterationCount - firstFrame.MaxIterationCount;
      double delta = deltaIterations / (double)numberOfFrames;
      if (numberOfFrames > 0)
      {
        for (int frame = 0; frame < numberOfFrames; frame++)
        {
          int frameIndex = firstFrame.FrameIndex + sign * frame;
          int maxIterationCount = (int)Math.Round(firstFrame.MaxIterationCount + delta * frame);
          frames.Add(new FrameData(frameIndex, maxIterationCount));
        }
      }
      return frames;
    }

    public string GetFormattedIndex()
    {
      string formattedFrameIndex = "" + _frameIndex;
      while (formattedFrameIndex.Length < 8)
      {
        formattedFrameIndex = "0" + formattedFrameIndex;
      }
      return formattedFrameIndex;
    }
  }
}
