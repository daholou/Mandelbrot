using Core.PixelPainter;
using System.Numerics;
using System;

namespace Core
{
  /// <summary>
  /// Represents a single pixel that covers a point (complex coordinates)
  /// </summary>
  public class MandelbrotPixel
  {
    private readonly IPixelPainter _pixelPainter;

    /// <summary>
    /// Complex point covered by this pixel
    /// </summary>
    private Complex _centerCoordinates;

    /// <summary>
    /// Ratio of the smoothed number of iterations needed for the complex
    /// to escape, divided by the maximum number of iterations.
    /// Computing the divergence rate is a costly operation, thus lazy loading 
    /// is used to avoid doing it too often.
    /// </summary>
    private double _divergenceRate;

    /// <summary>
    /// Maximum number of iterations used for computing the divergence rate.
    /// </summary>
    private int _maxIterationCount;

    /// <summary>
    /// true iff the divergence rate must be recomputed (lazy loading)
    /// </summary>
    private bool _mustRecomputeDivergenceRate;

    public MandelbrotPixel(
      IPixelPainter pixelPainter,
      Complex centerCoordinates
    )
    {
      _pixelPainter = pixelPainter;
      _centerCoordinates = centerCoordinates;
      _mustRecomputeDivergenceRate = true;
      _divergenceRate = 0.0;
      _maxIterationCount = 0;
    }

    public Complex CenterCoordinates
    {
      get
      {
        return _centerCoordinates;
      }
      set
      {
        _centerCoordinates = value;
      }
    }

    public void Update(Complex centerCoordinates, int maxIterationCount)
    {
      _centerCoordinates = centerCoordinates;
      _maxIterationCount = maxIterationCount;
      _mustRecomputeDivergenceRate = true;
    }

    public void Repaint()
    {
      _pixelPainter.Repaint(DivergenceRate);
    }

    private double ComputeDivergenceRate(int maxIterationCount)
    {
      Complex z = 0;
      double magnitude = 0;
      int k = 0;
      for (; k < maxIterationCount; k++)
      {
        magnitude = z.Magnitude;
        if (magnitude > 2)
        {
          break;
        }
        z = z * z + _centerCoordinates;
      }
      //double smoothedIterationCount = k;
      double smoothedIterationCount = (k == maxIterationCount)
        ? maxIterationCount
        : k + 1 - Math.Log2(Math.Log2(magnitude));
      return smoothedIterationCount / maxIterationCount;
    }

    public double DivergenceRate
    {
      get
      {
        if (_mustRecomputeDivergenceRate)
        {
          _divergenceRate = ComputeDivergenceRate(_maxIterationCount);
          _mustRecomputeDivergenceRate = false;
        }
        return _divergenceRate;
      }
    }
  }
}
