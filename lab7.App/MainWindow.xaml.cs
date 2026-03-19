using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using lab7.Library.Classes;
using System.Collections.Generic;
using System.Linq;

namespace lab7.App
{
    public partial class MainWindow : Window
    {
        private RenderTargetBitmap? _renderTarget;

        public MainWindow() => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init.DrawingImage = DrawingImage;
            int width = (int)DrawingImage.ActualWidth;
            int height = (int)DrawingImage.ActualHeight;
            if (width <= 0) width = 800;
            if (height <= 0) height = 400;

            ShapeContainer.CanvasWidth = width;
            ShapeContainer.CanvasHeight = height;

            _renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            DrawingImage.Source = _renderTarget;
            Redraw();
        }

        private void Redraw()
        {
            if (_renderTarget == null) return;
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(null, new Pen(Brushes.Red, 1),
                    new Rect(0, 0, ShapeContainer.CanvasWidth, ShapeContainer.CanvasHeight));

                foreach (var fig in ShapeContainer.FigureList)
                    fig.Draw(dc);
            }
            _renderTarget.Clear();
            _renderTarget.Render(visual);
            DrawingImage.Source = _renderTarget;
            UpdateList();
        }

        private void UpdateList()
        {
            LstFigures.Items.Clear();
            for (int i = 0; i < ShapeContainer.FigureList.Count; i++)
            {
                var fig = ShapeContainer.FigureList[i];
                LstFigures.Items.Add($"#{i + 1} {fig.GetType().Name} ({fig.x:F1}, {fig.y:F1})");
            }
        }

        private double GetVal(TextBox t)
        {
            try { return double.Parse(t.Text.Replace('.', ',')); }
            catch { return 0; }
        }

        private void TxtX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,.-]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtPolyPoints_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,. -]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnAddRectangle_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double w = Math.Max(10, GetVal(TxtSize));
            double h = Math.Max(10, GetVal(TxtHeight));

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + w > ShapeContainer.CanvasWidth || y + h > ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(x, ShapeContainer.CanvasWidth - w);
            y = Math.Min(y, ShapeContainer.CanvasHeight - h);

            ShapeContainer.AddFigure(new Rectangle1(x, y, w, h));
            Redraw();
        }

        private void BtnAddSquare_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double side = Math.Max(10, GetVal(TxtSize));

            if (side > ShapeContainer.CanvasWidth || side > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + side > ShapeContainer.CanvasWidth || y + side > ShapeContainer.CanvasHeight ||
                x + side <= 0 || y + side <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            side = Math.Min(side, Math.Min(ShapeContainer.CanvasWidth - x, ShapeContainer.CanvasHeight - y));

            ShapeContainer.AddFigure(new Square(x, y, side));
            Redraw();
        }

        private void BtnAddEllipse_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double w = Math.Max(10, GetVal(TxtSize));
            double h = Math.Max(10, GetVal(TxtHeight));

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + w > ShapeContainer.CanvasWidth || y + h > ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            w = Math.Min(w, ShapeContainer.CanvasWidth - x);
            h = Math.Min(h, ShapeContainer.CanvasHeight - y);

            ShapeContainer.AddFigure(new Ellipse1(x, y, w, h));
            Redraw();
        }

        private void BtnAddCircle_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double r = Math.Max(5, GetVal(TxtSize));

            if (r * 2 > ShapeContainer.CanvasWidth || r * 2 > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + r * 2 > ShapeContainer.CanvasWidth || y + r * 2 > ShapeContainer.CanvasHeight ||
                x + r * 2 <= 0 || y + r * 2 <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            double maxRadius = Math.Min(ShapeContainer.CanvasWidth - x, ShapeContainer.CanvasHeight - y) / 2;
            r = Math.Min(r, maxRadius);

            ShapeContainer.AddFigure(new Circle(x, y, r));
            Redraw();
        }

        private void BtnAddTriangle_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);

            Point p1 = new Point(GetVal(TxtT1X), GetVal(TxtT1Y));
            Point p2 = new Point(GetVal(TxtT2X), GetVal(TxtT2Y));
            Point p3 = new Point(GetVal(TxtT3X), GetVal(TxtT3Y));

            var points = new List<Point> { p1, p2, p3 };

            double minX = points.Min(p => p.X);
            double maxX = points.Max(p => p.X);
            double minY = points.Min(p => p.Y);
            double maxY = points.Max(p => p.Y);

            double triWidth = maxX - minX;
            double triHeight = maxY - minY;

            if (triWidth > ShapeContainer.CanvasWidth || triHeight > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Треугольник слишком большой! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + maxX > ShapeContainer.CanvasWidth || y + maxY > ShapeContainer.CanvasHeight ||
                x + minX < 0 || y + minY < 0)
            {
                MessageBox.Show("Треугольник не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(-minX, x);
            y = Math.Max(-minY, y);
            x = Math.Min(x, ShapeContainer.CanvasWidth - maxX);
            y = Math.Min(y, ShapeContainer.CanvasHeight - maxY);

            ShapeContainer.AddFigure(new Triangle(x, y, points));
            Redraw();
        }

        private void BtnAddPolygon_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);

            var points = ParsePoints(TxtPolyPoints.Text);

            if (points.Count < 3)
            {
                MessageBox.Show("Введите минимум 3 вершины! Формат: x1,y1 x2,y2 x3,y3...",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (points.Count > 50)
            {
                MessageBox.Show($"Слишком много вершин! Максимум 50. Введено: {points.Count}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double minX = points.Min(p => p.X);
            double maxX = points.Max(p => p.X);
            double minY = points.Min(p => p.Y);
            double maxY = points.Max(p => p.Y);

            double polyWidth = maxX - minX;
            double polyHeight = maxY - minY;

            if (polyWidth > ShapeContainer.CanvasWidth || polyHeight > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Многоугольник слишком большой! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + maxX > ShapeContainer.CanvasWidth || y + maxY > ShapeContainer.CanvasHeight ||
                x + minX < 0 || y + minY < 0)
            {
                MessageBox.Show("Многоугольник не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(-minX, x);
            y = Math.Max(-minY, y);
            x = Math.Min(x, ShapeContainer.CanvasWidth - maxX);
            y = Math.Min(y, ShapeContainer.CanvasHeight - maxY);

            ShapeContainer.AddFigure(new Polygon1(x, y, points));
            Redraw();
        }

        private void BtnAddComplex_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double w = Math.Max(50, GetVal(TxtSize));
            double h = Math.Max(50, GetVal(TxtHeight));

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая! Максимум: {ShapeContainer.CanvasWidth}x{ShapeContainer.CanvasHeight}",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (x + w > ShapeContainer.CanvasWidth || y + h > ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте в указанной позиции!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            w = Math.Min(w, ShapeContainer.CanvasWidth - x);
            h = Math.Min(h, ShapeContainer.CanvasHeight - y);

            ShapeContainer.AddFigure(new ComplexFigure(x, y, w, h));
            Redraw();
        }

        private void BtnDelete_Click(object s, RoutedEventArgs e)
        {
            if (LstFigures.SelectedIndex >= 0 && LstFigures.SelectedIndex < ShapeContainer.FigureList.Count)
            { ShapeContainer.RemoveFigure(ShapeContainer.FigureList[LstFigures.SelectedIndex]); Redraw(); }
        }

        private void BtnChangeRadius_Click(object s, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNewRadius.Text))
            {
                MessageBox.Show("Введите новый радиус", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (LstFigures.SelectedIndex >= 0 && ShapeContainer.FigureList[LstFigures.SelectedIndex] is Circle c)
            {
                double newRadius = GetVal(TxtNewRadius);

                double maxRadiusByWidth = (ShapeContainer.CanvasWidth - c.x) / 2;
                double maxRadiusByHeight = (ShapeContainer.CanvasHeight - c.y) / 2;

                double maxRadius = Math.Min(maxRadiusByWidth, maxRadiusByHeight);

                newRadius = Math.Max(1, Math.Min(newRadius, maxRadius));

                c.ChangeRadius(newRadius);
                Redraw();
            }
            else
                MessageBox.Show("Выберите окружность", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnResizeRectangle_Click(object s, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNewWidth.Text) || string.IsNullOrWhiteSpace(TxtNewHeight.Text))
            {
                MessageBox.Show("Введите ширину и высоту", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (LstFigures.SelectedIndex >= 0 && ShapeContainer.FigureList[LstFigures.SelectedIndex] is Rectangle1 r)
            {
                double newWidth = Math.Max(10, GetVal(TxtNewWidth));
                double newHeight = Math.Max(10, GetVal(TxtNewHeight));

                if (newWidth > ShapeContainer.CanvasWidth || newHeight > ShapeContainer.CanvasHeight)
                {
                    MessageBox.Show("Размер превышает холст!", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double maxWidth = ShapeContainer.CanvasWidth - r.x;
                double maxHeight = ShapeContainer.CanvasHeight - r.y;

                newWidth = Math.Max(10, Math.Min(newWidth, maxWidth));
                newHeight = Math.Max(10, Math.Min(newHeight, maxHeight));

                r.Resize(newWidth, newHeight);
                Redraw();
            }
            else
                MessageBox.Show("Выберите прямоугольник", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnMoveFigure_Click(object s, RoutedEventArgs e)
        {
            if (LstFigures.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите фигуру для перемещения", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double dx = GetVal(TxtDx);
            double dy = GetVal(TxtDy);
            ShapeContainer.MoveSelectedFigure(LstFigures.SelectedIndex, dx, dy);
            Redraw();
        }

        private void BtnClearAll_Click(object s, RoutedEventArgs e)
        { ShapeContainer.Clear(); Redraw(); }

        private void BtnHelp_Click(object s, RoutedEventArgs e)
        {
            MessageBox.Show(
                "ИНСТРУКЦИЯ:\n\n" +
                "1. Введите координаты X, Y и размеры\n" +
                "2. Нажмите кнопку фигуры для добавления\n" +
                "3. Выделите фигуру в списке для управления\n" +
                "4. 'Удалить' - удаляет выбранную фигуру\n" +
                "5. 'Изменить радиус' - для окружности\n" +
                "6. 'Изменить размер' - для прямоугольника\n" +
                "7. 'Переместить' - сдвиг на dx, dy\n" +
                "8. 'Сложная фигура' - машина из нескольких частей\n" +
                "9. 'Очистить всё' удалит все фигуры\n\n" +
                "Дробные числа: через ',' или '.'",
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private List<Point> ParsePoints(string text)
        {
            var points = new List<Point>();

            string[] vertexStrings = text.Split(new char[] { ' ', '\t', '\n', '\r' },
                                                StringSplitOptions.RemoveEmptyEntries);

            foreach (var vertexStr in vertexStrings)
            {
                string[] coords = vertexStr.Split(new char[] { ',', '.' },
                                                   StringSplitOptions.RemoveEmptyEntries);

                if (coords.Length >= 2)
                {
                    try
                    {
                        double x = double.Parse(coords[0].Replace('.', ','));
                        double y = double.Parse(coords[1].Replace('.', ','));
                        points.Add(new Point(x, y));
                    }
                    catch { }
                }
            }

            return points;
        }
    }
}