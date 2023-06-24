using System.Numerics;

namespace Core
{
  public class Configuration
  {
    private readonly Complex _center;
    private readonly double _horizontalRadius;
    private readonly double _verticalRadius;
    private readonly double _ratio;
    private readonly int _maxIterationCount;

    public Configuration(
      Complex center,
      double horizontalDiameter,
      double ratio,
      int maxIterationCount
    )
    {
      _center = center;
      _horizontalRadius = horizontalDiameter / 2;
      _verticalRadius = _horizontalRadius * ratio;
      _ratio = ratio;
      _maxIterationCount = maxIterationCount;
    }

    public Configuration Zoom(Complex focalPoint, double magnitude, int maxIterationCount)
    {
      Configuration zoomedConfiguration = new(
        focalPoint,
        2 * _horizontalRadius / magnitude,
        _ratio,
        maxIterationCount
      );
      return zoomedConfiguration;
    }

    public Configuration Zoom(double magnitude)
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
