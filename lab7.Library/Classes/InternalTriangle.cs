using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class InternalTriangle : Figure
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public Point Point3 { get; set; }

        public InternalTriangle(double x, double y, Point p1, Point p2, Point p3) : base(x, y)
        {
            Point1 = p1;
            Point2 = p2;
            Point3 = p3;
        }

        public override void Draw(DrawingContext dc)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(new Point(x + Point1.X, y + Point1.Y), false, true);
                ctx.LineTo(new Point(x + Point2.X, y + Point2.Y), true, false);
                ctx.LineTo(new Point(x + Point3.X, y + Point3.Y), true, false);
                ctx.LineTo(new Point(x + Point1.X, y + Point1.Y), true, false);
            }
            dc.DrawGeometry(Color, new Pen(Brushes.Black, 2), geometry);
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public override double GetWidth()
        {
            double minX = System.Math.Min(Point1.X, System.Math.Min(Point2.X, Point3.X));
            double maxX = System.Math.Max(Point1.X, System.Math.Max(Point2.X, Point3.X));
            return maxX - minX;
        }

        public override double GetHeight()
        {
            double minY = System.Math.Min(Point1.Y, System.Math.Min(Point2.Y, Point3.Y));
            double maxY = System.Math.Max(Point1.Y, System.Math.Max(Point2.Y, Point3.Y));
            return maxY - minY;
        }
    }
}