using System.Drawing;

namespace LinearInterpolationExample
{
  internal class ColorInterpolator
  {
    delegate byte ComponentSelector(Color color);
    private static readonly ComponentSelector _redSelector = color => color.R;
    private static readonly ComponentSelector _greenSelector = color => color.G;
    private static readonly ComponentSelector _blueSelector = color => color.B;

    private ColorInterpolator() {}

    private static byte InterpolateRGBComponent(
      Color colorA,
      Color colorB,
      double lambda,
      ComponentSelector selector
    )
    {
      double safeLambda = Math.Min(1.0, Math.Max(lambda, 0.0));
      double interpolatedValue = selector(colorA)
        + (selector(colorB) - selector(colorA)) * safeLambda;
      return (byte)interpolatedValue;
    }

    public static Color Interpolate(
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
      ;
    }
  }
}
