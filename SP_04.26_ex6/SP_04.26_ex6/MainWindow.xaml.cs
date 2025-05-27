using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SP_04._26_ex6
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void InputTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InputTextBox.Text == "Введіть текст...")
            {
                InputTextBox.Text = string.Empty;
                InputTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void InputTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputTextBox.Text))
            {
                InputTextBox.Text = "Введіть текст...";
                InputTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private async void AnalyzeButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                ResultTextBlock.Text = "Будь ласка, введіть текст.";
                return;
            }

            ResultTextBlock.Text = "Аналізую...";

            var result = await Task.Run(() => AnalyzeText(inputText));

            ResultTextBlock.Text = $"Голосні: {result.Vowels}, Приголосні: {result.Consonants}, Символи: {result.Characters}";
        }

        private (int Vowels, int Consonants, int Characters) AnalyzeText(string text)
        {
            int vowels = 0, consonants = 0, characters = text.Length;
            string vowelsList = "аеєиіїоуюяaeiou";

            foreach (char c in text.ToLower())
            {
                if (char.IsLetter(c))
                {
                    if (vowelsList.Contains(c))
                        vowels++;
                    else
                        consonants++;
                }
            }

            return (vowels, consonants, characters);
        }
    }
}