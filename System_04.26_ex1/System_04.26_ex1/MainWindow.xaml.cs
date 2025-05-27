using System;
using System.Text;
using System.Threading;
using System.Windows;

namespace System_04._26_ex1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartNumbersThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(GenerateNumbers);
            thread.Priority = GetSelectedPriority(NumbersPriority);
            thread.Start();
        }

        private void StartLettersThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(GenerateLetters);
            thread.Priority = GetSelectedPriority(LettersPriority);
            thread.Start();
        }

        private void StartSymbolsThread(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(GenerateSymbols);
            thread.Priority = GetSelectedPriority(SymbolsPriority);
            thread.Start();
        }

        private ThreadPriority GetSelectedPriority(System.Windows.Controls.ComboBox comboBox)
        {
            string selected = ((System.Windows.Controls.ComboBoxItem)comboBox.SelectedItem).Content.ToString();
            return selected switch
            {
                "Низький" => ThreadPriority.Lowest,
                "Нормальний" => ThreadPriority.Normal,
                "Високий" => ThreadPriority.Highest,
                _ => ThreadPriority.Normal
            };
        }

        private void GenerateNumbers()
        {
            for (int i = 0; i < 100; i++)
            {
                AppendText($"Число: {i}");
                Thread.Sleep(100);
            }
        }

        private void GenerateLetters()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                AppendText($"Літера: {c}");
                Thread.Sleep(150);
            }
        }

        private void GenerateSymbols()
        {
            string symbols = "!@#$%^&*()";
            foreach (char symbol in symbols)
            {
                AppendText($"Символ: {symbol}");
                Thread.Sleep(200);
            }
        }

        private void AppendText(string text)
        {
            Dispatcher.Invoke(() =>
            {
                OutputBox.AppendText(text + Environment.NewLine);
                OutputBox.ScrollToEnd();
            });
        }
    }
}