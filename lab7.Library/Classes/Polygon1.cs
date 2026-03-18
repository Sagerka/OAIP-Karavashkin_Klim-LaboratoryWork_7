using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;

namespace lab7.Library.Classes
{
    public class Polygon1 : Figure
    {
        public List<Point> RelativePoints { get; set; } = new List<Point>();
        public double Scale { get; set; } = 1.0;
        public int VertexCount { get; set; }
        public double Size { get; set; }

        public Polygon1(double x, double y, int vertexCount, double size) : base(x, y)
        {
            VertexCount = vertexCount;
            Size = size;
            RelativePoints = CreateRegularPolygon(vertexCount, size);
        }

        public Polygon1(double x, double y, List<Point> points) : base(x, y)
        {
            VertexCount = points.Count;
            RelativePoints = points;
            Size = 100;
        }

        private List<Point> CreateRegularPolygon(int vertices, double radius)
        {
            var points = new List<Point>();
            double angleStep = 2 * Math.PI / vertices;

            for (int i = 0; i < vertices; i++)
            {
                double angle = i * angleStep - Math.PI / 2;
                double px = radius * Math.Cos(angle);
                double py = radius * Math.Sin(angle);
                points.Add(new Point(px, py));
            }

            
            double minX = points.Min(p => p.X);
            double minY = points.Min(p => p.Y);

            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Point(points[i].X - minX, points[i].Y - minY);
            }

            return points;
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

        public void SetScale(double newScale)
        {
            if (newScale > 0) Scale = newScale;
        }

        public double GetWidth()
        {
            if (RelativePoints.Count == 0) return 0;

            double minX = RelativePoints.Min(p => p.X);
            double maxX = RelativePoints.Max(p => p.X);

            return (maxX - minX) * Scale;
        }

        public double GetHeight()
        {
            if (RelativePoints.Count == 0) return 0;

            double minY = RelativePoints.Min(p => p.Y);
            double maxY = RelativePoints.Max(p => p.Y);

            return (maxY - minY) * Scale;
        }
    }
}