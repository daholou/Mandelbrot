﻿using Core;
using Core.Util;
using MandelbrotExample.PainterFactory;
using MandelbrotExample.Util;
using System.Drawing;
using System.Runtime.Versioning;

namespace MandelbrotExample
{
  [SupportedOSPlatform("windows")]
  public class BitmapRunner : MandelbrotRunner
  {
    private static BitmapPainterFactory BuildPainterFactory(
      int width,
      int height,
      int paletteSize
    )
    {
      PositionColor[] breakpoints = new PositionColor[] {
        new PositionColor(0.0, Color.FromArgb(0, 7, 100)),
        new PositionColor(0.16, Color.FromArgb(32, 107, 203)),
        new PositionColor(0.42, Color.FromArgb(237, 255, 255)),
        new PositionColor(0.6425, Color.FromArgb(255, 170, 0)),
        new PositionColor(0.8575, Color.FromArgb(0, 2, 0)),
        new PositionColor(1.0, Color.FromArgb(0, 0, 0))
      };
      Bitmap bitmap = new(width, height);
      ColorInterpolator colorInterpolator = new(breakpoints, paletteSize);
      return new BitmapPainterFactory(bitmap, colorInterpolator);
    }

    public BitmapRunner(
      int width,
      int height,
      int paletteSize
    ) : base(width, height, BuildPainterFactory(width, height, paletteSize))
    {
    }

    public override void Execute(
      ZoomConfiguration zoomConfiguration,
      string folderPath
    )
    {
      for (int k = 0; k < zoomConfiguration.Size(); ++k)
      {
        Configuration configuration = zoomConfiguration.ConfigurationAt(k);
        FrameData frame = zoomConfiguration.FrameAt(k);
        Console.WriteLine($"  Working on frame {frame.FrameIndex} with {frame.MaxIterationCount} iterations...");
        _stopwatch.Restart();
        _mandelbrotGrid.UpdateConfiguration(configuration);
        string filename = $"bitmap_frame_{frame.GetFormattedIndex()}.png";
        _painterFactory.Save(FileUtils.GetFilePath(folderPath, filename));
        _stopwatch.Stop();
        Console.WriteLine($"  - bitmap created in : {_stopwatch.ElapsedMilliseconds} ms");
      }
    }

    public override string GetFramesDirectory()
    {
      return "bitmap-frames";
    }
  }
}
