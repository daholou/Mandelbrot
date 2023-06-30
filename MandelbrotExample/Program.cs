using Core.Frame;
using Core.Runner;
using Core.Util;
using MandelbrotExample.Runner;
using System.Numerics;
using System.Runtime.Versioning;


/*
* things to try
*  - https://www.youtube.com/watch?v=ghIqbvtRU5E
*  - https://www.youtube.com/watch?v=1Ta4BQpGhY4
*/

namespace MandelbrotExample
{
  internal class Program
  {
    private static readonly Complex[] ALL_CENTERS = new Complex[] {
      // https://www.youtube.com/watch?v=pCpLWbHVNhk
      new (0.3602404434376144, 0.64131306106480317),
      // https://en.wikipedia.org/wiki/Mandelbrot_set#/media/File:Mandel_zoom_14_satellite_julia_island.jpg
      new (-0.743643887037151, 0.131825904205330),
      // https://www.youtube.com/watch?v=CDxmQNkCclU
      new (-0.7746806106269039, -0.1374168856037867),
      // https://www.youtube.com/watch?v=ZiuZeBwamJ8
      new (-1.74699155837435203049881264860737, 0.00307900937843247525527155131469168),
      // https://www.youtube.com/watch?v=1Ta4BQpGhY4
      new (-1.99977406013629035931268075596025, -0.00000000329004032147943505349697867592668),
      // https://en.wikipedia.org/wiki/Mandelbrot_set#/media/File:Mandelbrot_sequence_new.gif
      new (-0.743643887037158704752191506114774, 0.131825904205311970493132056385139),
      new (-0.5, 0),
    };
    private static readonly Complex CENTER = ALL_CENTERS[2];
    private static readonly double INITIAL_HORIZONTAL_RADIUS = 1.8;
    private static readonly double[] ALL_ZOOM_FACTORS = new double[] { 1, 1.015 };
    private static readonly double ZOOM_FACTOR = ALL_ZOOM_FACTORS[1];
    private static readonly FrameData[] ALL_FRAME_DATA = new FrameData[] {
      new (1, 100),
      new (500, 500),
      new (1000, 1500),
      new (1500, 3000),
      new (2000, 8000),
      new (2250, 8000),

      //new (1, 100),
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
      //new (2120, 10000), // warning - precision loss past ~2120
    };

    private static void GenerateFrameZoomSequence(MandelbrotRunner runner)
    {
      for (int i = 0; i < ALL_FRAME_DATA.Length - 1; i++)
      {
        FrameData firstFrameData = ALL_FRAME_DATA[i];
        FrameData lastFrameData = ALL_FRAME_DATA[i + 1];
        FrameZoomSequence frameZoomSequence = new(
          firstFrameData,
          lastFrameData,
          CENTER,
          INITIAL_HORIZONTAL_RADIUS,
          INITIAL_HORIZONTAL_RADIUS * runner.Ratio,
          ZOOM_FACTOR
        );
        string folderPath = Path.Combine(
          FileUtils.GetCurrentFolder(),
          "generated-images",
          runner.GetFramesDirectory(),
          $"frames_{firstFrameData.FrameIndex}_{lastFrameData.FrameIndex - 1}"
        );
        runner.SaveZoomSequence(frameZoomSequence, folderPath);
      }
    }

    // 1280, // 1920, // 2560, // 3840, //
    // 720, // 1080, // 1440, // 2160, // 
    private static void GenerateSingleFrame(
      MandelbrotRunner runner,
      int maxIterationCount,
      int zoomCount
    )
    {
      FrameConfiguration frameConfiguration = new(
        CENTER,
        INITIAL_HORIZONTAL_RADIUS,
        INITIAL_HORIZONTAL_RADIUS * runner.Ratio,
        maxIterationCount
      );
      double magnitude = Math.Pow(ZOOM_FACTOR, zoomCount);
      frameConfiguration = frameConfiguration.Zoom(magnitude);
      string folderPath = Path.Combine(
        FileUtils.GetCurrentFolder(),
        "generated-images",
        runner.GetFramesDirectory(),
        $"single-frames"
      );
      runner.SaveSingleFrame(frameConfiguration, folderPath);
    }

    [SupportedOSPlatform("windows")]
    public static void Main(string[] args)
    {
      // UNCOMMENT THE DESIRED RUNNER
      // 1280, // 1920, // 2560, // 3840, //
      // 720, // 1080, // 1440, // 2160, // 
      MandelbrotRunner runner = new AsciiRunner(142, 79, 2560, 1440);
      //MandelbrotRunner runner = new BitmapRunner(1920, 1080, 2048);
      //GenerateSingleFrame(runner, 500, 500); // 500
      //GenerateSingleFrame(runner, 1500, 1000); // 4000
      //GenerateSingleFrame(runner, 3000, 1500); // 4000
      //GenerateSingleFrame(runner, 8000, 2000); // 10000
      //GenerateSingleFrame(runner, 8000, 2250); // 6000
      //// 2300 is too much
      GenerateFrameZoomSequence(runner);
    }
  }
}
