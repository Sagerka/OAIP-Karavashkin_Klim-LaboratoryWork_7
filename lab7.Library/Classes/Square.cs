using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Square : Figure
    {
        public double Side { get; set; }

        public Square(double x, double y, double side) : base(x, y)
        {
            Side = side;
        }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawRectangle(Color, new Pen(Brushes.Black, 2), new Rect(x, y, Side, Side));
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public override bool IsWithinBounds(double minX, double minY, double maxX, double maxY)
        {
            return x >= minX && y >= minY && x + Side <= maxX && y + Side <= maxY;
        }
    }
}