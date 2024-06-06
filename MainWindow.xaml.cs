using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPN.Logic;


namespace CalculatorOnWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            UpdateCanvas();
        }

        private void coordinates_MouseMove(object sender, MouseEventArgs e)
        {
            Point uiPoint = Mouse.GetPosition(canvasOutput);
            lblUiCoordinates.Content = $"{uiPoint.X:.0}; {uiPoint.Y:.0}";
            double zoom = double.Parse(tbInputScale.Text);
            Point mathPoint = Mouse.GetPosition(canvasOutput).ToMathCoordinates(canvasOutput, zoom);
            lblMathCoordinates.Content = $"{mathPoint.X:.0}; {mathPoint.Y:.0}";
        }

        private void UpdateCanvas()
        {
            canvasOutput.Children.Clear();
            double start = double.Parse(tbInputStart.Text);
            double end = double.Parse(tbInputEnd.Text);
            double step = double.Parse(tbInputStep.Text);
            double zoom = double.Parse(tbInputScale.Text);

            CanvasDrawer canvasDrawer = new CanvasDrawer(
                canvasOutput,
                start,
                end,
                step,
                zoom
                );
            canvasDrawer.DrawAxis();
            //canvasDrawer.DrawLine(new Point(230, 230), new Point(400, 400));

            RPNcalculator calculator = new RPNcalculator(tbInputExpression.Text);
            List<Point> points = new List<Point>();
            for (double x = start; x <= end; x += step)
            {
                double y = calculator.Calculate(x);
                points.Add(new Point(x, y));
            }
            canvasDrawer.PlotGraph(points);
        }
    }
}