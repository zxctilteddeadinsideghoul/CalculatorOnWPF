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
            double xVariable;
            if (tbInputX.Text == "")
            {
                lblOutput.Content = "Результат: " + new RPNcalculator(tbInputExpression.Text).Calculate().ToString();
            }
            else if (double.TryParse(tbInputX.Text, out xVariable))
            {
                lblOutput.Content = "Результат: " + new RPNcalculator(tbInputExpression.Text).Calculate(xVariable).ToString();
            }
        }
    }
}