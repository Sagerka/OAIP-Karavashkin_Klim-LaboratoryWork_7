using System.Collections.Generic;
using System.Linq;

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

           
            GetFigureBoundsAndOffsets(fig, out double width, out double height, out double offsetX, out double offsetY);

            double newX = fig.x + dx;
            double newY = fig.y + dy;

            
            if (newX + offsetX < 0)
                newX = -offsetX;

            
            if (newY + offsetY < 0)
                newY = -offsetY;

            
            if (newX + offsetX + width > CanvasWidth)
                newX = CanvasWidth - offsetX - width;

            
            if (newY + offsetY + height > CanvasHeight)
                newY = CanvasHeight - offsetY - height;

           
            newX = Math.Max(-offsetX, Math.Min(newX, CanvasWidth - offsetX - width));
            newY = Math.Max(-offsetY, Math.Min(newY, CanvasHeight - offsetY - height));

            fig.x = newX;
            fig.y = newY;
        }

        private static void GetFigureBoundsAndOffsets(Figure fig, out double width, out double height, out double offsetX, out double offsetY)
        {
            offsetX = 0;
            offsetY = 0;

            switch (fig)
            {
                case Rectangle1 r:
                    width = r.Width;
                    height = r.Height;
                    offsetX = 0;
                    offsetY = 0;
                    break;
                case Square s:
                    width = s.Side;
                    height = s.Side;
                    offsetX = 0;
                    offsetY = 0;
                    break;
                case Ellipse1 e:
                    width = e.Width;
                    height = e.Height;
                    offsetX = 0;
                    offsetY = 0;
                    break;
                case Circle c:
                    width = c.Radius * 2;
                    height = c.Radius * 2;
                    offsetX = 0;
                    offsetY = 0;
                    break;
                case Triangle t:
                    width = t.GetWidth();
                    height = t.GetHeight();
                    
                    if (t.RelativePoints.Count > 0)
                    {
                        offsetX = t.RelativePoints.Min(p => p.X);
                        offsetY = t.RelativePoints.Min(p => p.Y);
                    }
                    break;
                case Polygon1 p:
                    width = p.GetWidth();
                    height = p.GetHeight();
                    if (p.RelativePoints.Count > 0)
                    {
                        offsetX = p.RelativePoints.Min(p => p.X);
                        offsetY = p.RelativePoints.Min(p => p.Y);
                    }
                    break;
                case ComplexFigure cf:
                    width = cf.Width;
                    height = cf.Height;
                    offsetX = 0;
                    offsetY = 0;
                    break;
                default:
                    width = 100;
                    height = 100;
                    offsetX = 0;
                    offsetY = 0;
                    break;
            }
        }

        public static void GetFigureBounds(Figure fig, out double width, out double height)
        {
            GetFigureBoundsAndOffsets(fig, out width, out height, out _, out _);
        }

        public static Figure? GetFigure(int index)
        {
            if (index >= 0 && index < FigureList.Count)
                return FigureList[index];
            return null;
        }
    }
}