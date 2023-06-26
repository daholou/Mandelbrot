using System.Numerics;

namespace Core
{
  public class ZoomConfiguration
  {
    private readonly List<Configuration> _configurations;
    private readonly List<FrameData> _frames;

    public ZoomConfiguration(
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
      Configuration initialConfiguration = new(
          center,
          initialHorizontalDiameter,
          ratio,
          firstFrameData.MaxIterationCount
      );

      for (int k = 0; k < _frames.Count; k++)
      {
        double magnitude = Math.Pow(zoomFactor, firstFrameData.FrameIndex + k - 1);
        Configuration configuration = initialConfiguration.Zoom(
          magnitude,
          _frames[k].MaxIterationCount
        );
        _configurations.Add(configuration);
      }
    }

    public int Size()
    {
      return _configurations.Count;
    }

    public Configuration ConfigurationAt(int index)
    {
      return _configurations.ElementAt(index);
    }

    public FrameData FrameAt(int index)
    {
      return _frames.ElementAt(index);
    }
  }
}
