using Core.PixelPainter;

namespace Core.PainterFactory
{
  public interface IPainterFactory
  {
    public IPixelPainter MakePixelPainter(
      int row,
      int col,
      int? maxRow,
      int? maxCol
    );
  }
}
