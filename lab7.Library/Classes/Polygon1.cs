using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace lab7.Library.Classes
{
    public class Polygon1 : Figure
    {
        public List<Point> RelativePoints { get; set; } = new List<Point>();
        public double Scale { get; set; } = 1.0;

        public Polygon1(double x, double y, List<Point> points) : base(x, y)
        {
            RelativePoints = points;
        }

        public override void Draw(DrawingContext dc)
        {
            var geometry = new StreamGeometry();
            using (var ctx = geometry.Open())
            {
                bool isFirst = true;
                Point start = new Point(0, 0);

                foreach (var p in RelativePoints)
                {
                    var point = new Point(x + p.X * Scale, y + p.Y * Scale);
                    if (isFirst)
                    {
                        ctx.BeginFigure(point, false, true);
                        start = point;
                        isFirst = false;
                    }
                    else
                    {
                        ctx.LineTo(point, true, false);
                    }
                }
                ctx.LineTo(start, true, false);
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
            foreach (var p in RelativePoints)
            {
                double px = x + p.X * Scale;
                double py = y + p.Y * Scale;
                if (px < minX || py < minY || px > maxX || py > maxY)
                    return false;
            }
            return true;
        }

        public void SetScale(double newScale)
        {
            if (newScale > 0) Scale = newScale;
        }
    }
}