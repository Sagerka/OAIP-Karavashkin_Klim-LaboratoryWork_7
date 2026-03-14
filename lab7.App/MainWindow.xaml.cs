using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using lab7.Library.Classes;
using System.Collections.Generic;

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

        private void BtnAddRectangle_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double w = GetVal(TxtSize);
            double h = GetVal(TxtHeight);

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            w = Math.Min(w, ShapeContainer.CanvasWidth - x);
            h = Math.Min(h, ShapeContainer.CanvasHeight - y);

            ShapeContainer.AddFigure(new Rectangle1(x, y, w, h));
            Redraw();
        }

        private void BtnAddSquare_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double side = GetVal(TxtSize);

            if (side > ShapeContainer.CanvasWidth || side > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + side <= 0 || y + side <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
            double w = GetVal(TxtSize);
            double h = GetVal(TxtHeight);

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
            double r = GetVal(TxtSize);

            if (r * 2 > ShapeContainer.CanvasWidth || r * 2 > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + r * 2 <= 0 || y + r * 2 <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
            double w = GetVal(TxtSize);
            double h = GetVal(TxtHeight);

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);
            w = Math.Min(w, ShapeContainer.CanvasWidth - x);
            h = Math.Min(h, ShapeContainer.CanvasHeight - y);

            ShapeContainer.AddFigure(new Triangle(x, y, w, h));
            Redraw();
        }

        private void BtnAddPolygon_Click(object s, RoutedEventArgs e)
        {
            var points = new List<Point>
            {
                new Point(0, 50),
                new Point(30, 0),
                new Point(80, 0),
                new Point(100, 50),
                new Point(50, 100)
            };

            double x = GetVal(TxtX);
            double y = GetVal(TxtY);

            if (100 > ShapeContainer.CanvasWidth || 100 > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + 100 <= 0 || y + 100 <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, x);
            y = Math.Max(0, y);

            ShapeContainer.AddFigure(new Polygon1(x, y, points));
            Redraw();
        }

        private void BtnAddComplex_Click(object s, RoutedEventArgs e)
        {
            double x = GetVal(TxtX);
            double y = GetVal(TxtY);
            double w = GetVal(TxtSize);
            double h = GetVal(TxtHeight);

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight ||
                x >= ShapeContainer.CanvasWidth || y >= ShapeContainer.CanvasHeight ||
                x + w <= 0 || y + h <= 0)
            {
                MessageBox.Show("Фигура не помещается на холсте!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
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
                double newWidth = GetVal(TxtNewWidth);
                double newHeight = GetVal(TxtNewHeight);

                if (newWidth > ShapeContainer.CanvasWidth || newHeight > ShapeContainer.CanvasHeight)
                {
                    MessageBox.Show("Размер превышает холст!", "Внимание",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                double maxWidth = ShapeContainer.CanvasWidth - r.x;
                double maxHeight = ShapeContainer.CanvasHeight - r.y;

                newWidth = Math.Max(1, Math.Min(newWidth, maxWidth));
                newHeight = Math.Max(1, Math.Min(newHeight, maxHeight));

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
                "8. 'Сложная фигура' - машина\n" +
                "9. 'Очистить всё' удалит все фигуры\n\n" +
                "9. 'Очистить всё' удалит все фигуры\n\n" +
                "Дробные числа: ',' или '.'",
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}