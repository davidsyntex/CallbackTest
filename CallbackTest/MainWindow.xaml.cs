using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CallbackTest
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartSpam(object sender, RoutedEventArgs e)
        {
            var progress = new Progress<string>(s => TextBoxSpam.AppendText(s));
            await Task.Factory.StartNew(() => LongWork(progress));

            TextBoxSpam.AppendText(Environment.NewLine + "Complete");
        }

        private void LongWork(IProgress<string> progress)
        {
            for (var i = 1; i < 1001; i++)
            {
                //Task.Delay(500).Wait();
                SleepAsync(1000).Wait();
                progress.Report(i + Environment.NewLine);
            }
        }

        private Task<bool> SleepAsync(int i)
        {
            TaskCompletionSource<bool> tcs = null;
            var t = new Timer(delegate { tcs.TrySetResult(true); }, null, -1, -1);
            tcs = new TaskCompletionSource<bool>(t);
            t.Change(i, -1);
            return tcs.Task;
        }

        private void ScrollDown(object sender, TextChangedEventArgs e)
        {
            TextBoxSpam.ScrollToEnd();
        }

        private AsyncCallback sAsyncCallback;


    }
}