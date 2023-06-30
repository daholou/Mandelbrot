using System.Numerics;

namespace Core.Frame
{
  public class FrameConfiguration
  {
    private readonly Complex _center;
    private readonly double _horizontalRadius;
    private readonly double _verticalRadius;
    private readonly int _maxIterationCount;
    private readonly double _magnitude;

    public FrameConfiguration(
      Complex center,
      double horizontalRadius,
      double verticalRadius,
      int maxIterationCount,
      double magnitude = 1
    )
    {
      _center = center;
      _horizontalRadius = horizontalRadius;
      _verticalRadius = verticalRadius;
      _maxIterationCount = maxIterationCount;
      _magnitude = magnitude;
    }

    public FrameConfiguration Zoom(
      Complex focalPoint,
      double magnitude,
      int maxIterationCount
    )
    {
      FrameConfiguration zoomedConfiguration = new(
        focalPoint,
        _horizontalRadius / magnitude,
        _verticalRadius / magnitude,
        maxIterationCount,
        _magnitude * magnitude
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

    public double Magnitude
    {
      get
      {
        return _magnitude;
      }
    }

    public Complex Center
    {
      get
      {
        return _center;
      }
    }

    public int MaxIterationCount
    {
      get
      {
        return _maxIterationCount;
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

    public override string ToString()
    {
      return $"  - Zoom magnitude: x {Magnitude:E5}\n"
        + $"  - Center : {Center}\n"
        + $"  - Maximum iteration count : {MaxIterationCount}\n"
        + $"  - Horizontal radius : {HorizontalRadius}\n"
        + $"  - Vertical radius : {VerticalRadius}";
    }
  }
}
