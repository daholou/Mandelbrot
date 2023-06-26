using Core;
using Core.Util;
using System.Numerics;
using System.Runtime.Versioning;

namespace MandelbrotExample
{
  internal class Program
  {
    private static readonly Complex CENTER = new(
      -0.743643887037151, //-0.7746806106269039, //0.3602404434376144, //  
      0.131825904205330 //-0.1374168856037867 //-0.64131306106480317 // 
    );
    private static readonly double INITIAL_HORIZONTAL_DIAMETER = 3.6;
    private static readonly double ZOOM_FACTOR = 1.015;

    private static readonly FrameData[] ALL_FRAME_DATA = new FrameData[] {
      new (1, 100),
      new (3, 100),
      //new (100, 200),
      //new (200, 300),
      //new (300, 600),
      //new (400, 800),
      //new (500, 1000),
      //new (600, 1300),
      //new (700, 1600),
      //new (800, 1900),
      //new (900, 7000),
      //new (1000, 8000),
      //new (1100, 10000),
      //new (1200, 12000),
      //new (1300, 9000),
      //new (1400, 6000),
      //new (1500, 4000),
      //new (1600, 10000),
      //new (1700, 16000),
      //new (1800, 10000),
      //new (1900, 10000),
      //new (2000, 10000),
      //new (2100, 10000),
      //new (2200, 10000),
      //new (2300, 10000),
      //new (2400, 10000),
    };

    [SupportedOSPlatform("windows")]
    public static void Main(string[] args)
    {
      // 1280, // 1920, // 2560, // 3840, //
      // 720, // 1080, // 1440, // 2160, // 
      MandelbrotRunner runner = new AsciiRunner(142, 79, 2560, 1440);
      //MandelbrotRunner runner = new BitmapRunner(1920, 1080, 2048);
      for (int i = 0; i < ALL_FRAME_DATA.Length - 1; i++)
      {
        FrameData firstFrameData = ALL_FRAME_DATA[i];
        FrameData lastFrameData = ALL_FRAME_DATA[i + 1];
        ZoomConfiguration zoomConfiguration = new(
          firstFrameData,
          lastFrameData,
          CENTER,
          INITIAL_HORIZONTAL_DIAMETER,
          runner.Ratio,
          ZOOM_FACTOR
        );
        int a = firstFrameData.FrameIndex;
        int b = lastFrameData.FrameIndex;
        string folderPath = Path.Combine(
          FileUtils.GetCurrentFolder(),
          "generated-images",
          runner.GetFramesDirectory(),
          $"frames_{a}_{b}"
        );
        runner.Execute(zoomConfiguration, folderPath);
      }
    }
  }
}
