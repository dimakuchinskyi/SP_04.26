using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace System_04._26_ex3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectSourcePath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Выберите папку",
                ValidateNames = false
            };

            if (dialog.ShowDialog() == true)
            {
                SourcePathTextBox.Text = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private void SelectDestinationPath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Выберите папку",
                ValidateNames = false
            };

            if (dialog.ShowDialog() == true)
            {
                DestinationPathTextBox.Text = Path.GetDirectoryName(dialog.FileName);
            }
        }

        private async void StartCopy_Click(object sender, RoutedEventArgs e)
        {
            string sourcePath = SourcePathTextBox.Text;
            string destinationPath = DestinationPathTextBox.Text;

            if (!int.TryParse(ThreadCountTextBox.Text, out int threadCount) || threadCount <= 0)
            {
                MessageBox.Show("Введіть коректну кількість потоків.");
                return;
            }

            if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destinationPath))
            {
                MessageBox.Show("Виберіть шляхи для копіювання.");
                return;
            }

            try
            {
                CopyProgressBar.Value = 0;
                await Task.Run(() => CopyDirectory(sourcePath, destinationPath, threadCount));
                MessageBox.Show("Копіювання завершено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void CopyDirectory(string sourceDir, string destDir, int threadCount)
        {
            var files = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories);
            int totalFiles = files.Length;
            int copiedFiles = 0;

            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
            {
                string relativePath = Path.GetRelativePath(sourceDir, file);
                string destFile = Path.Combine(destDir, relativePath);

                Directory.CreateDirectory(Path.GetDirectoryName(destFile)!);
                File.Copy(file, destFile, true);

                Interlocked.Increment(ref copiedFiles);
                Dispatcher.Invoke(() =>
                {
                    CopyProgressBar.Value = (double)copiedFiles / totalFiles * 100;
                });
            });
        }
    }
}