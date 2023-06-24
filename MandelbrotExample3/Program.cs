using Core;
using MandelbrotExample3.PainterFactory;
using MandelbrotExample3.Util;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.Versioning;

namespace MandelbrotExample3
{
  internal class Program
  {
    private static readonly int WIDTH = 142; // 1280; // 3840; // 1920; //2560; // 
    private static readonly int HEIGHT = 79; // 720; // 2160; // 1080; //1440; // 
    private static readonly double RATIO = HEIGHT / (double)WIDTH;
    private static readonly Configuration INITIAL_CONFIGURATION = new(
      new Complex(
        -0.743643887037151, //-0.7746806106269039, //0.3602404434376144, //  
         0.131825904205330 //-0.1374168856037867 //-0.64131306106480317 // 
      ), 3.6, RATIO, 4000);
    private static readonly double ZOOM_FACTOR = 1.015;

    private static readonly int W_RES = 1280; // 2560; // 3840; // 1920; //
    private static readonly int H_RES = 720; // 1440; // 2160; // 1080; //

    [SupportedOSPlatform("windows")]
    public static void Main(string[] args)
    {
      StringGrid stringGrid = new(WIDTH, HEIGHT);
      AsciiPainterFactory painterFactory = new(stringGrid);
      MandelbrotPixelGrid mandelbrotGrid = new(painterFactory, WIDTH, HEIGHT);
      Configuration configuration = INITIAL_CONFIGURATION;

      Stopwatch watch = new();
      for (int frame = 1; frame <= 4000; ++frame)
      {
        double magnitude = Math.Pow(ZOOM_FACTOR, frame - 1);
        Console.WriteLine($"  Working on frame {frame} - magnitude x {magnitude} ...");
        watch.Restart();
        mandelbrotGrid.UpdateConfiguration(configuration);
        string formattedFrame = "" + frame;
        while (formattedFrame.Length < 8)
        {
          formattedFrame = "0" + formattedFrame;
        }
        string filename = $"ascii_frame_{formattedFrame}.png";
        stringGrid.Save(W_RES, H_RES, filename, ImageFormat.Png);
        watch.Stop();
        configuration = configuration.Zoom(ZOOM_FACTOR);
        Console.WriteLine($"  - created in : {watch.ElapsedMilliseconds} ms");
      }
    }
  }
}
