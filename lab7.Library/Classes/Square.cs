using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Square : Rectangle1
    {
        public Square(double x, double y, double side)
            : base(x, y, side, side)
        {
        }

        public override void Resize(double newWidth, double newHeight)
        {
            double side = System.Math.Max(newWidth, newHeight);
            if (side > 0)
            {
                Width = side;
                Height = side;
            }
        }
    }
}