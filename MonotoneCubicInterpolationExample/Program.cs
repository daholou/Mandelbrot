//Position = 0.0     Color = (0, 7, 100)
//Position = 0.16    Color = (32, 107, 203)
//Position = 0.42    Color = (237, 255, 255)
//Position = 0.6425  Color = (255, 170, 0)
//Position = 0.8575  Color = (0, 2, 0)
using System.Drawing;

namespace LinearInterpolationExample
{
  public class Program
  {
    public static void Main()
    {
      List<double> xValues = new() { // X coordinates of control points
        0.0,
        0.16, 
        0.42, 
        0.6425, 
        0.8575,
        1.0
      };
      List<Color> yValues = new() { // Y coordinates of control points
        Color.FromArgb(0, 7, 100),
        Color.FromArgb(32, 107, 203),
        Color.FromArgb(237, 255, 255),
        Color.FromArgb(255, 170, 0),
        Color.FromArgb(0, 2, 0),
        Color.FromArgb(0, 7, 100),
      };
      int numPoints = 20;
      double stepSize = (xValues[^1] - xValues[0]) / (numPoints - 1);

      List<Color> interpolatedColors = new List<Color>();
      for (int i = 0; i < stepSize - 1; i++)
      {
        double interpolatedX = xValues[0] + i * stepSize;
        Console.WriteLine("({0}, {1})", interpolatedX[i], interpolatedY[i]);
      }
    }

    // Generate interpolated points between the control points
    public static double[] GenerateInterpolatedPoints(
      int numInterpolatedPoints, // must be >= 2
      double[] x // must be of Length >= 2
    )
    {
      double[] interpolatedX = new double[numInterpolatedPoints];
      double stepSize = (x[^1] - x[0]) / (numInterpolatedPoints - 1);
      for (int i = 0; i < numInterpolatedPoints; i++)
      {
        interpolatedX[i] = x[0] + i * stepSize;
      }
      return interpolatedX;
    }

    // Perform linear interpolation
    public static Color LinearInterpolation(
      List<double[] xValues,
      Color[] yValues,
      double target
    )
    {
      int k = 0; 
      xValues
    }
  }
}
