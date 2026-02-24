using System.Collections.Generic;

namespace lab7.Library.Classes
{
    public static class ShapeContainer
    {
        public static List<Figure> FigureList { get; } = new List<Figure>();

        public static void AddFigure(Figure figure) => FigureList.Add(figure);
        public static void RemoveFigure(Figure figure) => FigureList.Remove(figure);
        public static void Clear() => FigureList.Clear();
    }
}