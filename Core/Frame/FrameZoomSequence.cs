using System.Numerics;

namespace Core.Frame
{
  public class FrameZoomSequence
  {
    private readonly List<FrameConfiguration> _configurations;
    private readonly List<FrameData> _frames;

    public FrameZoomSequence(
      FrameData firstFrameData,
      FrameData lastFrameData,
      Complex center,
      double initialHorizontalRadius,
      double initialVerticalRadius,
      double zoomFactor
    )
    {
      _frames = FrameData.InterpolateBetween(
        firstFrameData,
        lastFrameData
      );

      _configurations = new();
      FrameConfiguration baseConfiguration = new(
        center,
        initialHorizontalRadius,
        initialVerticalRadius,
        firstFrameData.MaxIterationCount
      );

      for (int k = 0; k < _frames.Count; k++)
      {
        double magnitude = Math.Pow(zoomFactor, firstFrameData.FrameIndex + k - 1);
        FrameConfiguration configuration = baseConfiguration.Zoom(
          magnitude,
          _frames[k].MaxIterationCount
        );
        _configurations.Add(configuration);
      }
    }

    public int Size
    {
      get
      {
        return _configurations.Count;
      }
    }

    public int LastFrameIndex
    {
      get
      {
        return _frames.Last().FrameIndex;
      }
    }

    public FrameConfiguration FrameConfigurationAt(int index)
    {
      return _configurations.ElementAt(index);
    }

    public FrameData FrameDataAt(int index)
    {
      return _frames.ElementAt(index);
    }
  }
}
