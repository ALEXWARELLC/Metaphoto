using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Metaphoto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer RefreshTimer;

        public MainWindow()
        {
            Altex.Core.Initialize();
            InitializeComponent();
            RefreshTimer = new Timer() { Interval = 1000, AutoReset = true };
            EnableEvents();
            RefreshTimer.Enabled = true;
            Title = Altex.UI.UIWindowHelper.Instance.SetTitle(Altex.Licensing.ProductManager.Instance.GetProduct(Altex.Licensing.ProductID.Metaphoto));
            LoadImage();
        }

        private void EnableEvents()
        {
            RefreshTimer.Elapsed += OnTimerHit;
        }

        private void OPEN_IMAGE_FILE(object sender, RoutedEventArgs e) => LoadImage();

        private void OnTimerHit(object? sender, ElapsedEventArgs e)
        {
            //this.Dispatcher.Invoke(new Action(() =>
            //{
            //    LABEL_STATUSBAR_CPUUSAGE.Content = $"CPU: {Task.Run(Altex.Diagnostics.CPUManager.Instance.GetCpuUsageAsync).Result}";
            //}));
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    LABEL_STATUSBAR_MEMORYUSAGE.Content = $"Memory: {Altex.Diagnostics.MemoryManager.Instance.CalculateMemoryUsage(Process.GetCurrentProcess().PrivateMemorySize64)}";
                }));
            }
            catch
            {
                return;
            }
        }

        private void LoadImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Title = "Open an Image File | Metaphoto", Filter = "Image Files|*.png;*.jpg;*.jpeg", FilterIndex = 0 };
            try
            {
                if (openFileDialog.ShowDialog() == true) { CONTENT_VIEWPORT.Source = new BitmapImage(new Uri(openFileDialog.FileName)); Title = Altex.UI.UIWindowHelper.Instance.SetTitle(Altex.Licensing.ProductManager.Instance.GetProduct(Altex.Licensing.ProductID.Metaphoto), openFileDialog.SafeFileName); Settings.IsImageInteractable = true; } else { return; }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void ALLOW_IMAGE_CONTROL_EVENTS(object sender, System.Windows.Input.MouseEventArgs e) { Settings.IsImageInteractable = true; }
    }
}
