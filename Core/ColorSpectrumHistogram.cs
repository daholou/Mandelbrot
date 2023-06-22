using Core.Util;
using System;

namespace Core
{
  public class ColorSpectrumHistogram
  {
    /// <summary>
    /// Number of colors used in the palette (sampled spectrum)
    /// </summary>
    private readonly int _paletteSize;

    /// <summary>
    /// Cumulative histogram of the color repartition. There are as many bins
    /// as there are colors in the palette.
    /// _histogram[i] is the rate of pixels whose divergence rate is below 
    /// the threshold "i / _palette.Length".
    /// </summary>
    private readonly double[] _histogram;

    public ColorSpectrumHistogram(int paletteSize)
    {
      _paletteSize = paletteSize;
      _histogram = new double[paletteSize];
    }

    public double GetHistogramRate(double divergenceRate)
    {
      int histogramIndex = (int)Math.Floor(divergenceRate * (_paletteSize - 1));
      int safeHistogramIndex = ScalarUtils.Clamp(histogramIndex, 0, _paletteSize - 1);
      return _histogram[safeHistogramIndex];
    }

    public void ResetHistogram()
    {
      for (int i = 0; i < _histogram.Length; ++i)
      {
        _histogram[i] = 0.0;
      }
    }

    public void IncrementHistogram(double divergenceRate)
    {
      int index = (int)Math.Floor(divergenceRate * (_histogram.Length - 1));
      int safeIndex = ScalarUtils.Clamp(index, 0, _histogram.Length - 1);
      _histogram[safeIndex] += 1.0;
    }

    public void BuildCumulativeHistogram()
    {
      for (int i = 1; i < _histogram.Length; ++i)
      {
        _histogram[i] += _histogram[i - 1];
      }
    }

    public void NormalizeHistogram(int total)
    {
      for (int i = 0; i < _histogram.Length; ++i)
      {
        _histogram[i] /= total;
      }
    }
  }
}
