using Core.Util;
using System.Drawing;

namespace MandelbrotExample2.Util
{
  internal class ColorInterpolator
  {
    /// <summary>
    /// Spectrum of colors
    /// </summary>
    private readonly PositionColor[] _breakpoints;

    /// <summary>
    /// Color palette (pre-computed on construction).
    /// </summary>
    private readonly Color[] _palette;

    private static PositionColor[] Sanitize(PositionColor[] positionColors)
    {
      PositionColor[] sanitized = new PositionColor[positionColors.Length];
      Array.Copy(positionColors, sanitized, positionColors.Length);
      Array.Sort(sanitized, (x, y) => x.Position.CompareTo(y.Position));
      double min = sanitized[0].Position;
      double max = sanitized[^1].Position;
      for (int k = 0; k < sanitized.Length; ++k)
      {
        sanitized[k].Position = (sanitized[k].Position - min) / (max - min);
      }
      return sanitized;
    }

    public ColorInterpolator(
      PositionColor[] breakpoints,
      int paletteSize = 10
    )
    {
      _breakpoints = Sanitize(breakpoints);
      _palette = BuildPalette(paletteSize);
    }

    private Color[] BuildPalette(int numberOfColors)
    {
      Color[] result = new Color[numberOfColors];

      for (int k = 0; k < numberOfColors; ++k)
      {
        double lambda = k / (double)numberOfColors;
        int foundIndex = 0;
        for (; foundIndex < _breakpoints.Length - 1; ++foundIndex)
        {
          if (_breakpoints[foundIndex].Position <= lambda
            && lambda <= _breakpoints[foundIndex + 1].Position
          )
          {
            break;
          }
        }

        double ratio = (lambda - _breakpoints[foundIndex].Position)
          / (_breakpoints[foundIndex + 1].Position - _breakpoints[foundIndex].Position);

        result[k] = ColorUtils.InterpolateBetween(
          _breakpoints[foundIndex].Color,
          _breakpoints[foundIndex + 1].Color,
          ratio
        );
      }
      return result;
    }

    public Color GetColor(double rate)
    {
      int paletteIndex = (int)Math.Floor(rate * (_palette.Length - 1));
      int safeIndex = ScalarUtils.Clamp(paletteIndex, 0, _palette.Length - 1);
      return _palette[safeIndex];
    }
  }
}
