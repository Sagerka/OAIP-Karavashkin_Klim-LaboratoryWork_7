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

            double width = fig.GetWidth();
            double height = fig.GetHeight();

            double offsetX = 0;
            double offsetY = 0;

            if (fig is Polygon1 p && p.RelativePoints.Count > 0)
            {
                offsetX = p.RelativePoints.Min(pt => pt.X);
                offsetY = p.RelativePoints.Min(pt => pt.Y);
            }

            double newX = fig.x + dx;
            double newY = fig.y + dy;

            newX = System.Math.Max(-offsetX, newX);
            newY = System.Math.Max(-offsetY, newY);
            newX = System.Math.Min(newX, CanvasWidth - offsetX - width);
            newY = System.Math.Min(newY, CanvasHeight - offsetY - height);

            fig.Move(newX - fig.x, newY - fig.y);
        }

        public static Figure? GetFigure(int index)
        {
            if (index >= 0 && index < FigureList.Count)
                return FigureList[index];
            return null;
        }
    }
}