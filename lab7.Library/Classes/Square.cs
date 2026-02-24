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
            dc.DrawRectangle(Color, new Pen(Brushes.Black, 1),
                new Rect(x, y, Side, Side));
        }
    }
}