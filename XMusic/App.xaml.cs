using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;

namespace XMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NotifyIconInitialize();
            base.OnStartup(e);
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon?.Dispose();
            base.OnExit(e);
        }
    }
}