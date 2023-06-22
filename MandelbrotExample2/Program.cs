using Core;
using MandelbrotExample2.PainterFactory;
using MandelbrotExample2.Util;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace MandelbrotExample2
{
  internal class Program
  {
    private static readonly int WIDTH =1280; // 3840; // 1920; //2560; // 
    private static readonly int HEIGHT =720; // 2160; // 1080; //1440; // 
    private static readonly double RATIO = HEIGHT / (double)WIDTH;
    private static readonly int PALETTE_SIZE = 2048;
    private static readonly PositionColor[] BREAKPOINTS = new PositionColor[] {
        new PositionColor(0.0, Color.FromArgb(0, 7, 100)),
        new PositionColor(0.16, Color.FromArgb(32, 107, 203)),
        new PositionColor(0.42, Color.FromArgb(237, 255, 255)),
        new PositionColor(0.6425, Color.FromArgb(255, 170, 0)),
        new PositionColor(0.8575, Color.FromArgb(0, 2, 0)),
        new PositionColor(1.0, Color.FromArgb(0, 7, 100))
    };
    private static readonly Configuration[] CONFIGURATIONS = new Configuration[] {
      new Configuration(new Complex(-0.7, 0), 3.6, RATIO, 100),
      new Configuration(new Complex(-0.87591, 0.17164), 0.53184, RATIO, 150),
      new Configuration(new Complex(-0.759856, 0.125547), 0.051579, RATIO, 500),
      new Configuration(new Complex(-0.743030, 0.126433), 0.016110, RATIO, 2000),
      new Configuration(new Complex(-0.7435669, 0.1314023), 0.0022878, RATIO, 1000),
      new Configuration(new Complex(-0.74364990, 0.13188204), 0.00073801, RATIO, 1000),
      new Configuration(new Complex(-0.74364085, 0.13182733), 0.00012068, RATIO, 1000),
      new Configuration(new Complex(-0.743643135, 0.131825963), 0.000014628, RATIO, 2000),
      new Configuration(new Complex(-0.7436447860, 0.1318252536), 0.0000029336, RATIO, 8000),
      new Configuration(new Complex(-0.74364409961, 0.13182604688), 0.00000066208, RATIO, 8000),
      new Configuration(new Complex(-0.74364386269, 0.13182590271), 0.00000013526, RATIO, 12000),
      new Configuration(new Complex(-0.743643900055, 0.131825890901), 0.000000049304, RATIO, 12000),
      new Configuration(new Complex(-0.7436438885706, 0.1318259043124), 0.0000000041493, RATIO, 5000),
      new Configuration(new Complex(-0.74364388717342, 0.13182590425182), 0.00000000059849, RATIO, 4000),
      new Configuration(new Complex(-0.743643887037151, 0.131825904205330), 0.000000000051299, RATIO, 16000),
    };

    public static void Main(string[] args)
    {
      if (!OperatingSystem.IsWindows())
      {
        Console.WriteLine("ERROR: this program can only run on Windows");
        return;
      }

      ColorInterpolator colorInterpolator = new(BREAKPOINTS, PALETTE_SIZE);
      Bitmap bitmap = new(WIDTH, HEIGHT);
      BitmapPainterFactory painterFactory = new(bitmap, colorInterpolator);
      MandelbrotPixelGrid mandelbrotGrid = new(painterFactory, WIDTH, HEIGHT, PALETTE_SIZE);
      Stopwatch watch = new();
      for (int k = 0; k < CONFIGURATIONS.Length; k++)
      {
        Console.WriteLine($"  Working on configuration {k} ...");
        watch.Restart();
        mandelbrotGrid.UpdateConfiguration(CONFIGURATIONS[k]);
        string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        string fileName = $"output_{k}_{CONFIGURATIONS[k].MaxIterationCount}_{timeStamp}.png";
        bitmap.Save(fileName, ImageFormat.Png);
        watch.Stop();
        Console.WriteLine($"  - max iteration count === {CONFIGURATIONS[k].MaxIterationCount}");
        Console.WriteLine($"  - created in : {watch.ElapsedMilliseconds} ms");
      }
    }
  }
}
