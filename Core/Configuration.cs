using System.Numerics;
using System;

namespace Core
{
  public class Configuration
  {
    private readonly Complex _center;
    private readonly double _horizontalRadius;
    private readonly double _verticalRadius;
    private readonly int _maxIterationCount;

    public Configuration(Complex center, double horizontalDiameter, double ratio, int maxIterationCount)
    {
      _center = center;
      _horizontalRadius = horizontalDiameter / 2;
      _verticalRadius = _horizontalRadius * ratio;
      _maxIterationCount = maxIterationCount;
    }

    public Configuration(Complex center, double horizontalDiameter, double ratio)
    {
      _center = center;
      _horizontalRadius = horizontalDiameter / 2;
      _verticalRadius = _horizontalRadius * ratio;
      _maxIterationCount = (int)Math.Floor(50 + Math.Pow(Math.Log10(4 / horizontalDiameter), 5));
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
