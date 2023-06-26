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
      double initialHorizontalDiameter,
      double ratio,
      double zoomFactor
    )
    {
      _frames = FrameData.InterpolateBetween(
        firstFrameData,
        lastFrameData
      );

      _configurations = new();
      FrameConfiguration initialConfiguration = new(
          center,
          initialHorizontalDiameter,
          ratio,
          firstFrameData.MaxIterationCount
      );

      for (int k = 0; k < _frames.Count; k++)
      {
        double magnitude = Math.Pow(zoomFactor, firstFrameData.FrameIndex + k - 1);
        FrameConfiguration configuration = initialConfiguration.Zoom(
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
