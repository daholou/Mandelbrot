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
      initialConfiguration = initialConfiguration.Zoom(Math.Pow(zoomFactor, firstFrameData.FrameIndex - 1));
      _configurations.Add(initialConfiguration);

      for (int k = 1; k < _frames.Count; k++)
      {
        Configuration configuration = _configurations[k - 1].Zoom(zoomFactor);
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
