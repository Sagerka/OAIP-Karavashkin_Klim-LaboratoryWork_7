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
            double w = Math.Max(10, GetVal(TxtSize));
            double h = Math.Max(10, GetVal(TxtHeight));

            if (w > ShapeContainer.CanvasWidth || h > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Фигура слишком большая",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, Math.Min(x, ShapeContainer.CanvasWidth - w));
            y = Math.Max(0, Math.Min(y, ShapeContainer.CanvasHeight - h));

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
                MessageBox.Show($"Фигура слишком большая",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, Math.Min(x, ShapeContainer.CanvasWidth - side));
            y = Math.Max(0, Math.Min(y, ShapeContainer.CanvasHeight - side));

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
                MessageBox.Show($"Фигура слишком большая",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, Math.Min(x, ShapeContainer.CanvasWidth - w));
            y = Math.Max(0, Math.Min(y, ShapeContainer.CanvasHeight - h));

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
                MessageBox.Show($"Фигура слишком большая",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, Math.Min(x, ShapeContainer.CanvasWidth - r * 2));
            y = Math.Max(0, Math.Min(y, ShapeContainer.CanvasHeight - r * 2));

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

            // размеры треугольника
            double minX = points.Min(p => p.X);
            double maxX = points.Max(p => p.X);
            double minY = points.Min(p => p.Y);
            double maxY = points.Max(p => p.Y);

            double triWidth = maxX - minX;
            double triHeight = maxY - minY;

            
            if (triWidth > ShapeContainer.CanvasWidth || triHeight > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Треугольник слишком большой",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            if (x + maxX > ShapeContainer.CanvasWidth || y + maxY > ShapeContainer.CanvasHeight ||
                x + minX < 0 || y + minY < 0)
            {
                MessageBox.Show("Треугольник не помещается в указанной позиции",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // ограничение позиции 
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
            double size = Math.Max(10, GetVal(TxtSize));
            int vertices = (int)Math.Max(3, Math.Min(GetVal(TxtVertices), 20));

            
            double polyWidth = size * 2;
            double polyHeight = size * 2;

            
            if (polyWidth > ShapeContainer.CanvasWidth || polyHeight > ShapeContainer.CanvasHeight)
            {
                MessageBox.Show($"Многоугольник слишком большой",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            
            if (x + polyWidth > ShapeContainer.CanvasWidth || y + polyHeight > ShapeContainer.CanvasHeight ||
                x + polyWidth <= 0 || y + polyHeight <= 0)
            {
                MessageBox.Show("Многоугольник не помещаетс в указанной позиции",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // ограничение позиции
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(x, ShapeContainer.CanvasWidth - polyWidth);
            y = Math.Min(y, ShapeContainer.CanvasHeight - polyHeight);

            ShapeContainer.AddFigure(new Polygon1(x, y, vertices, size));
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
                MessageBox.Show($"Фигура слишком большая",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            x = Math.Max(0, Math.Min(x, ShapeContainer.CanvasWidth - w));
            y = Math.Max(0, Math.Min(y, ShapeContainer.CanvasHeight - h));

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
           // else
               // MessageBox.Show("Выберите окружность", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("Размер превышает холст", "Внимание",
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
            //else
               // MessageBox.Show("Выберите прямоугольник", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            // доп проверка после перемещения
            var fig = ShapeContainer.FigureList[LstFigures.SelectedIndex];
            ShapeContainer.GetFigureBounds(fig, out double w, out double h);

            if (fig.x < 0 || fig.y < 0 || fig.x + w > ShapeContainer.CanvasWidth || fig.y + h > ShapeContainer.CanvasHeight)
            {
                fig.x = Math.Max(0, Math.Min(fig.x, ShapeContainer.CanvasWidth - w));
                fig.y = Math.Max(0, Math.Min(fig.y, ShapeContainer.CanvasHeight - h));
            }

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
    }
}