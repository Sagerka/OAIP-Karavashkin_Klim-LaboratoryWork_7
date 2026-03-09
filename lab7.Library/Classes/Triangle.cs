using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Triangle : Figure
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Triangle(double x, double y, double width, double height) : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(DrawingContext dc)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(new Point(x, y + Height), false, false);
                ctx.LineTo(new Point(x + Width / 2, y), true, false);
                ctx.LineTo(new Point(x + Width, y + Height), true, false);
                ctx.LineTo(new Point(x, y + Height), true, false);
            }
            dc.DrawGeometry(Color, new Pen(Brushes.Black, 2), geometry);
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public override bool IsWithinBounds(double minX, double minY, double maxX, double maxY)
        {
            return x >= minX && y >= minY && x + Width <= maxX && y + Height <= maxY;
        }
    }
}