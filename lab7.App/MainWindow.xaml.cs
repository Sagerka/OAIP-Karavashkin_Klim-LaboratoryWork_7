using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using lab7.Library.Classes;

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

            _renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            DrawingImage.Source = _renderTarget;
            Redraw();
        }

        private void Redraw()
        {
            if (_renderTarget == null) return;
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
                foreach (var fig in ShapeContainer.FigureList) fig.Draw(dc);
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
                LstFigures.Items.Add($"#{i + 1} {fig.GetType().Name} ({fig.x}, {fig.y})");
            }
        }

        private double GetVal(TextBox t)
        {
            try { return double.Parse(t.Text.Replace('.', ',')); }
            catch { return 0; }
        }

        private void BtnAddRectangle_Click(object s, RoutedEventArgs e)
        { ShapeContainer.AddFigure(new Rectangle1(GetVal(TxtX), GetVal(TxtY), GetVal(TxtSize), GetVal(TxtHeight))); Redraw(); }

        private void BtnAddSquare_Click(object s, RoutedEventArgs e)
        { ShapeContainer.AddFigure(new Square(GetVal(TxtX), GetVal(TxtY), GetVal(TxtSize))); Redraw(); }

        private void BtnAddEllipse_Click(object s, RoutedEventArgs e)
        { ShapeContainer.AddFigure(new Ellipse1(GetVal(TxtX), GetVal(TxtY), GetVal(TxtSize), GetVal(TxtHeight))); Redraw(); }

        private void BtnAddCircle_Click(object s, RoutedEventArgs e)
        { ShapeContainer.AddFigure(new Circle(GetVal(TxtX), GetVal(TxtY), GetVal(TxtSize))); Redraw(); }

        private void BtnDelete_Click(object s, RoutedEventArgs e)
        {
            if (LstFigures.SelectedIndex >= 0 && LstFigures.SelectedIndex < ShapeContainer.FigureList.Count)
            { ShapeContainer.RemoveFigure(ShapeContainer.FigureList[LstFigures.SelectedIndex]); Redraw(); }
        }

        private void BtnChangeRadius_Click(object s, RoutedEventArgs e)
        {
            if (TxtNewRadius.Text == "Новый радиус" || string.IsNullOrWhiteSpace(TxtNewRadius.Text))
            {
                MessageBox.Show("Введите новый радиус", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (LstFigures.SelectedIndex >= 0 && ShapeContainer.FigureList[LstFigures.SelectedIndex] is Circle c)
            { c.ChangeRadius(GetVal(TxtNewRadius)); Redraw(); }
        }

        private void BtnClearAll_Click(object s, RoutedEventArgs e)
        { ShapeContainer.Clear(); Redraw(); }

        private void BtnHelp_Click(object s, RoutedEventArgs e)
        {
            MessageBox.Show(
                "ИНСТРУКЦИЯ:\n\n" +
                "1. Введите координаты X и Y\n" +
                "2. Введите размеры (ширина/радиус/высота)\n" +
                "3. Нажмите кнопку фигуры для добавления\n" +
                "4. Нажмите на фигуру в списке и нажмите 'Удалить'\n" +
                "5. Для окружности: сотрите надпись и введите новый радиус и нажмите 'Изменить радиус'\n" +
                "6. 'Очистить всё' удаляет все фигуры\n\n" +
                "Можно использовать дробные числа используя '.' или ','",
                "Справка",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}