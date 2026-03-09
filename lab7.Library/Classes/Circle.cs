using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Circle : Figure
    {
        public double Radius { get; set; }

        public Circle(double x, double y, double radius) : base(x, y)
        {
            Radius = radius;
        }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawEllipse(Color, new Pen(Brushes.Black, 2),
                new Point(x + Radius, y + Radius), Radius, Radius);
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public override bool IsWithinBounds(double minX, double minY, double maxX, double maxY)
        {
            return x >= minX && y >= minY && x + Radius * 2 <= maxX && y + Radius * 2 <= maxY;
        }

        public void ChangeRadius(double newRadius)
        {
            if (newRadius > 0) Radius = newRadius;
        }
    }
}