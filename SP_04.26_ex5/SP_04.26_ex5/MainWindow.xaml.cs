using System;
using System.Threading.Tasks;
using System.Windows;

namespace SP_04._26_ex5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ResultText.Text = "Обчислення...";
            try
            {
                double number = double.Parse(NumberInput.Text);
                int power = int.Parse(PowerInput.Text);

                double result = await Task.Run(() => CalculatePower(number, power));
                ResultText.Text = $"Результат: {result}";
            }
            catch (FormatException)
            {
                ResultText.Text = "Помилка: введіть коректні числа.";
            }
            catch (Exception ex)
            {
                ResultText.Text = $"Помилка: {ex.Message}";
            }
        }

        private double CalculatePower(double number, int power)
        {
            return Math.Pow(number, power);
        }
    }
}