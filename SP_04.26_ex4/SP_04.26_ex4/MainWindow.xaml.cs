using System;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;

namespace SP_04._26_ex4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void InputTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrEmpty(InputTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private async void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = "Обчислення...";
            if (int.TryParse(InputTextBox.Text, out int number) && number >= 0)
            {
                try
                {
                    BigInteger result = await Task.Run(() => CalculateFactorial(number));
                    ResultTextBlock.Text = $"Факторіал {number} = {result}";
                }
                catch (Exception ex)
                {
                    ResultTextBlock.Text = $"Помилка: {ex.Message}";
                }
            }
            else
            {
                ResultTextBlock.Text = "Будь ласка, введіть коректне невід'ємне число.";
            }
        }

        private BigInteger CalculateFactorial(int number)
        {
            BigInteger factorial = 1;
            for (int i = 2; i <= number; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}