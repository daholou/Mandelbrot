using System.Numerics;

namespace Core.Frame
{
  public class FrameConfiguration
  {
    private readonly Complex _center;
    private readonly double _horizontalDiameter;
    private readonly double _horizontalRadius;
    private readonly double _verticalRadius;
    private readonly double _ratio;
    private readonly int _maxIterationCount;

    public FrameConfiguration(
      Complex center,
      double horizontalDiameter,
      double ratio,
      int maxIterationCount
    )
    {
      _center = center;
      _horizontalDiameter = horizontalDiameter;
      _horizontalRadius = horizontalDiameter / 2;
      _verticalRadius = _horizontalRadius * ratio;
      _ratio = ratio;
      _maxIterationCount = maxIterationCount;
    }

    public FrameConfiguration Zoom(Complex focalPoint, double magnitude, int maxIterationCount)
    {
      FrameConfiguration zoomedConfiguration = new(
        focalPoint,
        _horizontalDiameter / magnitude,
        _ratio,
        maxIterationCount
      );
      return zoomedConfiguration;
    }

    public FrameConfiguration Zoom(double magnitude, int maxIterationCount)
    {
      return Zoom(_center, magnitude, maxIterationCount);
    }

    public FrameConfiguration Zoom(double magnitude)
    {
      return Zoom(_center, magnitude, _maxIterationCount);
    }

    public Complex Center
    {
      get
      {
        return _center;
      }
    }

    public double HorizontalRadius
    {
      get
      {
        return _horizontalRadius;
      }
    }

    public double VerticalRadius
    {
      get
      {
        return _verticalRadius;
      }
    }

    public int MaxIterationCount
    {
      get
      {
        return _maxIterationCount;
      }
    }
  }
}
