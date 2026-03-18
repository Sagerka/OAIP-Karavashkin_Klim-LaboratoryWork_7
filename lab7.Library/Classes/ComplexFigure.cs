using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace lab7.Library.Classes
{
    public class ComplexFigure : Figure
    {
        public List<Figure> Parts { get; set; } = new List<Figure>();
        public double Width { get; set; } = 180;
        public double Height { get; set; } = 150;

        public ComplexFigure(double x, double y, double width, double height) : base(x, y)
        {
            Width = width;
            Height = height;
            CreateTrainFigure();
        }

        private void CreateTrainFigure()
        {
            Parts.Clear();

            double scaleX = Width / 180.0;
            double scaleY = Height / 150.0;

            
            var body = new Rectangle1(0 * scaleX, 50 * scaleY, 120 * scaleX, 60 * scaleY);
            body.Color = Brushes.SteelBlue;
            Parts.Add(body);

            
            var cabin = new Rectangle1(120 * scaleX, 50 * scaleY, 60 * scaleX, 60 * scaleY);
            cabin.Color = Brushes.IndianRed;
            Parts.Add(cabin);

            
            Parts.Add(new InternalTriangle(
                10 * scaleX, 0 * scaleY,
                new Point(0, 50 * scaleY),      
                new Point(20 * scaleX, 0),       
                new Point(40 * scaleX, 50 * scaleY) 
            ));

            Parts.Add(new InternalTriangle(
                60 * scaleX, 0 * scaleY,
                new Point(0, 50 * scaleY),      
                new Point(20 * scaleX, 0),      
                new Point(40 * scaleX, 50 * scaleY) 
            ));

           
            Parts.Add(new Circle(20 * scaleX, 120 * scaleY, 15 * Math.Min(scaleX, scaleY)));
            Parts.Add(new Circle(70 * scaleX, 120 * scaleY, 15 * Math.Min(scaleX, scaleY)));
        }

        public override void Draw(DrawingContext dc)
        {
            foreach (var part in Parts)
            {
                double origX = part.x;
                double origY = part.y;

                part.x = x + origX;
                part.y = y + origY;

                part.Draw(dc);

                part.x = origX;
                part.y = origY;
            }
        }

        public override void Move(double dx, double dy)
        {
            x += dx;
            y += dy;
        }
    }
}