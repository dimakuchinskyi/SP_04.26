using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace System_04._26_ex2
{
    public partial class MainWindow : Window
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isPaused;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectSourceFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                SourcePath.Text = openFileDialog.FileName;
                SourcePath.Foreground = Brushes.Black;
            }
        }

        private void SelectDestinationFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                DestinationPath.Text = saveFileDialog.FileName;
                DestinationPath.Foreground = Brushes.Black;
            }
        }

        private void RemovePlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void AddPlaceholder(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "SourcePath")
                    textBox.Text = "Шлях до початкового файлу";
                else if (textBox.Name == "DestinationPath")
                    textBox.Text = "Шлях для копіювання";

                textBox.Foreground = Brushes.Gray;
            }
        }

        private async void StartCopy_Click(object sender, RoutedEventArgs e)
        {
            string source = SourcePath.Text;
            string destination = DestinationPath.Text;
            if (!File.Exists(source))
            {
                MessageBox.Show("Початковий файл не знайдено.");
                return;
            }

            if (!int.TryParse(ThreadCount.Text, out int threadCount) || threadCount <= 0)
            {
                MessageBox.Show("Введіть коректну кількість потоків.");
                return;
            }

            _cancellationTokenSource = new CancellationTokenSource();
            _isPaused = false;

            try
            {
                ProgressBar.Value = 0;
                await CopyFileAsync(source, destination, threadCount, _cancellationTokenSource.Token);
                MessageBox.Show("Копіювання завершено!");
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Копіювання зупинено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private async Task CopyFileAsync(string source, string destination, int threadCount, CancellationToken cancellationToken)
        {
            long fileSize = new FileInfo(source).Length;
            long chunkSize = fileSize / threadCount;
            ProgressBar.Maximum = fileSize;

            Task[] tasks = new Task[threadCount];
            object progressLock = new object();

            for (int i = 0; i < threadCount; i++)
            {
                long start = i * chunkSize;
                long end = (i == threadCount - 1) ? fileSize : start + chunkSize;

                tasks[i] = Task.Run(() =>
                {
                    using (FileStream sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read))
                    using (FileStream destinationStream = new FileStream(destination, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        byte[] buffer = new byte[8192];
                        long position = start;
                        sourceStream.Seek(position, SeekOrigin.Begin);
                        destinationStream.Seek(position, SeekOrigin.Begin);

                        while (position < end)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            lock (this)
                            {
                                while (_isPaused)
                                {
                                    Monitor.Wait(this);
                                }
                            }

                            int bytesToRead = (int)Math.Min(buffer.Length, end - position);
                            int bytesRead = sourceStream.Read(buffer, 0, bytesToRead);
                            if (bytesRead == 0) break;

                            destinationStream.Write(buffer, 0, bytesRead);
                            position += bytesRead;

                            lock (progressLock)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    ProgressBar.Value += bytesRead;
                                });
                            }
                        }
                    }
                }, cancellationToken);
            }

            await Task.WhenAll(tasks);
        }

        private void PauseCopy_Click(object sender, RoutedEventArgs e)
        {
            lock (this)
            {
                _isPaused = !_isPaused;
                if (!_isPaused)
                {
                    Monitor.PulseAll(this);
                }
            }
        }

        private void StopCopy_Click(object sender, RoutedEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _isPaused = false;

            // Очищення полів для вибору нових шляхів
            SourcePath.Text = "Шлях до початкового файлу";
            SourcePath.Foreground = Brushes.Gray;
            DestinationPath.Text = "Шлях для копіювання";
            DestinationPath.Foreground = Brushes.Gray;
            ProgressBar.Value = 0;
        }
    }
}