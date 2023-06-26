using System.Drawing;

namespace MandelbrotExample.Util
{
  public class PositionColor
  {
    private double _position;
    private Color _color;

    public PositionColor(double position, Color color)
    {
      _position = position;
      _color = color;
    }

    public Color Color
    {
      get
      {
        return _color;
      }
      set
      {
        _color = value;
      }
    }

    public double Position
    {
      get
      {
        return _position;
      }
      set
      {
        _position = value;
      }
    }
  }
}
