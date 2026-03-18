using System.Windows.Media;

namespace lab7.Library.Classes
{
    public abstract class Figure
    {
        public double x { get; set; }
        public double y { get; set; }
        public Brush Color { get; set; } = Brushes.Black;
        public double Opacity { get; set; } = 1.0;

        public Figure(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract void Draw(DrawingContext dc);

        public abstract void Move(double dx, double dy);
    }
}