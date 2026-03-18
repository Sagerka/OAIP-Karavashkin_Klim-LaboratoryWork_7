using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Rectangle1 : Figure
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle1(double x, double y, double width, double height) : base(x, y)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawRectangle(Color, new Pen(Brushes.Black, 2), new Rect(x, y, Width, Height));
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }

        public void Resize(double newWidth, double newHeight)
        {
            if (newWidth > 0) Width = newWidth;
            if (newHeight > 0) Height = newHeight;
        }
    }
}