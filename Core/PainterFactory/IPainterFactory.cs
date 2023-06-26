using Core.PixelPainter;

namespace Core.PainterFactory
{
  public interface IPainterFactory
  {
    public IPixelPainter MakePixelPainter(int row, int col);

    public void Save(string filePath);
  }
}
