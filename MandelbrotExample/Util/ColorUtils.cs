using Core.Util;
using System.Drawing;

namespace MandelbrotExample.Util
{
  internal class ColorUtils
  {
    delegate byte ComponentSelector(Color color);
    private static readonly ComponentSelector _redSelector = color => color.R;
    private static readonly ComponentSelector _greenSelector = color => color.G;
    private static readonly ComponentSelector _blueSelector = color => color.B;

    private ColorUtils()
    {
    }

    private static byte InterpolateRGBComponent(
      Color colorA,
      Color colorB,
      double lambda,
      ComponentSelector selector
    )
    {
      double safeLambda = ScalarUtils.Clamp(lambda, 0.0, 1.0);
      double interpolatedValue = selector(colorA)
        + (selector(colorB) - selector(colorA)) * safeLambda;
      return (byte)interpolatedValue;
    }

    public static Color InterpolateBetween(
        Color colorA,
        Color colorB,
        double lambda
    )
    {
      return Color.FromArgb(
        InterpolateRGBComponent(colorA, colorB, lambda, _redSelector),
        InterpolateRGBComponent(colorA, colorB, lambda, _greenSelector),
        InterpolateRGBComponent(colorA, colorB, lambda, _blueSelector)
      );
    }
  }
}
