using Core;
using MandelbrotExample2.PainterFactory;
using MandelbrotExample2.Util;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.Versioning;

namespace MandelbrotExample2
{
  internal class Program
  {
    private static readonly int WIDTH = 1280; // 3840; // 1920; //2560; // 
    private static readonly int HEIGHT = 720; // 2160; // 1080; //1440; // 
    private static readonly double RATIO = HEIGHT / (double)WIDTH;
    private static readonly int PALETTE_SIZE = 2048;
    private static readonly PositionColor[] BREAKPOINTS = new PositionColor[] {
        new PositionColor(0.0, Color.FromArgb(0, 7, 100)),
        new PositionColor(0.16, Color.FromArgb(32, 107, 203)),
        new PositionColor(0.42, Color.FromArgb(237, 255, 255)),
        new PositionColor(0.6425, Color.FromArgb(255, 170, 0)),
        new PositionColor(0.8575, Color.FromArgb(0, 2, 0)),
        new PositionColor(1.0, Color.FromArgb(0, 0, 0))
    };
    private static readonly Complex CENTER = new(
      -0.743643887037151, //-0.7746806106269039, //0.3602404434376144, //  
      0.131825904205330 //-0.1374168856037867 //-0.64131306106480317 // 
    );
    private static readonly double INITIAL_HORIZONTAL_DIAMETER = 3.6;
    private static readonly double ZOOM_FACTOR = 1.015;

    private static readonly FrameData[] ALL_FRAME_DATA = new FrameData[] {
      new (1, 100),
      new (100, 200),
      new (200, 300),
      new (300, 600),
      new (400, 800),
      new (500, 1000),
      new (600, 1300),
      new (700, 1600),
      new (800, 1900),
      new (900, 7000),
      new (1000, 8000),
      new (1100, 10000),
      new (1200, 12000),
      new (1300, 9000),
      new (1400, 6000),
      new (1500, 4000),
      new (1600, 10000),
      new (1700, 16000),
    };
    private static readonly int FIRST = 0;
    private static readonly int LAST = FIRST + 1;
    private static readonly ZoomConfiguration ZOOM_CONFIGURATION = new(
      ALL_FRAME_DATA[FIRST],
      ALL_FRAME_DATA[LAST],
      CENTER,
      INITIAL_HORIZONTAL_DIAMETER,
      RATIO,
      ZOOM_FACTOR
    );

    private static string GetFilePath(string filename)
    {
      int a = ALL_FRAME_DATA[FIRST].FrameIndex;
      int b = ALL_FRAME_DATA[LAST].FrameIndex;
      string folderPath = $"C:\\Users\\hetsu\\OneDrive\\Bureau\\test\\bitmap\\frames_{a}_{b}";
      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }
      return Path.Combine(folderPath, filename);
    }

    [SupportedOSPlatform("windows")]
    public static void Main(string[] args)
    {
      Bitmap bitmap = new(WIDTH, HEIGHT);
      ColorInterpolator colorInterpolator = new(BREAKPOINTS, PALETTE_SIZE);
      BitmapPainterFactory painterFactory = new(bitmap, colorInterpolator);
      MandelbrotPixelGrid mandelbrotGrid = new(painterFactory, WIDTH, HEIGHT);

      Stopwatch watch = new();
      for (int k = 0; k < ZOOM_CONFIGURATION.Size(); ++k)
      {
        Configuration configuration = ZOOM_CONFIGURATION.ConfigurationAt(k);
        FrameData frame = ZOOM_CONFIGURATION.FrameAt(k);
        Console.WriteLine($"  Working on frame {frame.FrameIndex} with {frame.MaxIterationCount} iterations...");
        watch.Restart();
        mandelbrotGrid.UpdateConfiguration(configuration);
        string filename = $"bitmap_frame_{frame.GetFormattedIndex()}.png";
        bitmap.Save(GetFilePath(filename), ImageFormat.Png);
        watch.Stop();
        Console.WriteLine($"  - bitmap created in : {watch.ElapsedMilliseconds} ms");
      }
    }
  }
}
