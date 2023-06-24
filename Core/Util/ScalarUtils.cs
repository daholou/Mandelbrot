namespace Core.Util
{
  public class ScalarUtils
  {
    private ScalarUtils()
    {
    }

    public static double Clamp(double value, double min, double max)
    {
      return Math.Min(max, Math.Max(min, value));
    }

    public static int Clamp(int value, int min, int max)
    {
      return Math.Min(max, Math.Max(min, value));
    }
  }
}
