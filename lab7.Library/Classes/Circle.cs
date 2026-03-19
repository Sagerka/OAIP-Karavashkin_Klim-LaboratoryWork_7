using System.Windows;
using System.Windows.Media;

namespace lab7.Library.Classes
{
    public class Circle : Ellipse1
    {
        public double Radius
        {
            get => Width / 2;
            set
            {
                Width = value * 2;
                Height = value * 2;
            }
        }

        public Circle(double x, double y, double radius)
            : base(x, y, radius * 2, radius * 2)
        {
        }

        public void ChangeRadius(double newRadius)
        {
            if (newRadius > 0)
            {
                Radius = newRadius;
            }
        }
    }
}