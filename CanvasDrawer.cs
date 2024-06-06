using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;

namespace CalculatorOnWPF
{

    static class PointExtentions
    {
        public static Point ToMathCoordinates(this Point point, Canvas canvas, double zoom)
        {
            return new Point(
                (point.X - canvas.ActualWidth / 2) / zoom,
                (canvas.ActualHeight / 2 - point.Y) / zoom
                );
        }

        public static Point ToUiCoordinates(this Point point, Canvas canvas, double zoom) 
        {
            return new Point(
                point.X * zoom + canvas.ActualWidth / 2,
                canvas.ActualHeight / 2 - point.Y * zoom
                );
        }
    }

    internal class CanvasDrawer
    {
        private readonly Canvas _canvas;
        Point xAxisStart, xAxisEnd, yAxisStart, yAxisEnd;
        private readonly double _xStart, _xEnd, _step, _zoom;
        private readonly int _scaleLength;
        private readonly Brush _brush = Brushes.Black;

        public CanvasDrawer( Canvas canvas, double xStart, double xEnd, double step, double zoom)
        {
            _canvas = canvas;
            _xStart = xStart;
            _xEnd = xEnd;
            _step = step;
            _zoom = zoom;

            xAxisStart = new Point(_canvas.ActualWidth / 2, 0);
            xAxisEnd = new Point(_canvas.ActualWidth / 2, _canvas.ActualHeight);



            yAxisStart = new Point(0, (int)_canvas.ActualHeight / 2);
            yAxisEnd = new Point((int)_canvas.ActualWidth, (int)_canvas.ActualHeight / 2);
        }

        public void DrawLine(Point point1, Point point2, Brush brush)
        {
            Line line = new Line();
            line.Visibility = Visibility.Visible;
            line.StrokeThickness = 1;
            line.Stroke = brush;
            line.X1 = point1.X;
            line.X2 = point2.X;
            line.Y1 = point1.Y;
            line.Y2 = point2.Y;
            _canvas.Children.Add(line);
        }

        public void DrawAxis()
        {
            DrawLine(xAxisStart, xAxisEnd, _brush);
            DrawLine(yAxisStart, yAxisEnd, _brush);

            DrawScale();
        }

        public static void DrawPoint(Canvas canvas, Point point, Brush brush)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 5;
            ellipse.Height = 5;
            ellipse.Fill = brush;
            
            Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
            Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);
            canvas.Children.Add(ellipse);
        }

        private void DrawScale()
        {

            for (double i = _xStart; i <= _xEnd; i += _step)
            {
                Point point1 = new Point(i, _zoom/150).ToUiCoordinates(_canvas, _zoom);
                Point point2 = new Point(i, -_zoom/150).ToUiCoordinates(_canvas, _zoom);
                point1.Y -= _scaleLength;
                point2.Y += _scaleLength;

                DrawLine(point1, point2, _brush);
            }


        }

        public void PlotGraph(List<Point> points)
        {
            
            for (int i = 1; i < points.Count; i++)
            {
                DrawPoint(_canvas, points[i].ToUiCoordinates(_canvas, _zoom), Brushes.Red);
                DrawLine(points[i - 1].ToUiCoordinates(_canvas, _zoom), points[i].ToUiCoordinates(_canvas, _zoom), Brushes.Red);
            }
        }

    }
}
