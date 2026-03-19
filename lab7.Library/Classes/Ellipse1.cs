using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Ellipse1 : Figure
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Ellipse1(double x, double y, double width, double height) : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawEllipse(Color, new Pen(Brushes.Black, 2),
                new Point(x + Width / 2, y + Height / 2), Width / 2, Height / 2);
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public override double GetWidth()
        {
            return Width;
        }

        public override double GetHeight()
        {
            return Height;
        }
    }
}