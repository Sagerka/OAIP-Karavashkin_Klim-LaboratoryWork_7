using System.Collections.Generic;

namespace lab7.Library.Classes
{
    public static class ShapeContainer
    {
        public static List<Figure> FigureList { get; } = new List<Figure>();
        public static double CanvasWidth { get; set; } = 800;
        public static double CanvasHeight { get; set; } = 400;

        public static void AddFigure(Figure figure) => FigureList.Add(figure);
        public static void RemoveFigure(Figure figure) => FigureList.Remove(figure);
        public static void Clear() => FigureList.Clear();

        public static void MoveSelectedFigure(int index, double dx, double dy)
        {
            if (index < 0 || index >= FigureList.Count) return;

            var fig = FigureList[index];

            
            GetFigureBounds(fig, out double width, out double height);

            
            double newX = fig.x + dx;
            double newY = fig.y + dy;

            
            if (newX < 0) newX = 0;
            if (newY < 0) newY = 0;

            
            if (newX + width > CanvasWidth)
                newX = CanvasWidth - width;
            if (newY + height > CanvasHeight)
                newY = CanvasHeight - height;

            
            fig.x = newX;
            fig.y = newY;
        }

        private static void GetFigureBounds(Figure fig, out double width, out double height)
        {
            switch (fig)
            {
                case Rectangle1 r:
                    width = r.Width;
                    height = r.Height;
                    break;
                case Square s:
                    width = s.Side;
                    height = s.Side;
                    break;
                case Ellipse1 e:
                    width = e.Width;
                    height = e.Height;
                    break;
                case Circle c:
                    width = c.Radius * 2;
                    height = c.Radius * 2;
                    break;
                case Triangle t:
                    width = t.Width;
                    height = t.Height;
                    break;
                case ComplexFigure cf:
                    width = 180;
                    height = 150;
                    break;
                default:
                    width = 100;
                    height = 100;
                    break;
            }
        }

        public static Figure? GetFigure(int index)
        {
            if (index >= 0 && index < FigureList.Count)
                return FigureList[index];
            return null;
        }
    }
}