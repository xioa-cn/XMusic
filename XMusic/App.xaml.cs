using System;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;
using Microsoft.VisualBasic.Logging;
using WPF.Xlog.Logger.Service;

namespace XMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LogService.CreateLoggerInstance();
            // 设置全局异常处理
            AppDomain.CurrentDomain.UnhandledException += (s, args) =>
            {
                Logger?.LogFatal("Unhandled Exception", args.ExceptionObject as Exception);
            };

            Current.DispatcherUnhandledException += (s, args) =>
            {
                Logger?.LogError("Dispatcher Unhandled Exception", args.Exception);
                args.Handled = true;
            };
            Logger?.LogInfo("startup");
            NotifyIconInitialize();
            Window.Show();
            base.OnStartup(e);
        }



        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon?.Dispose();
            base.OnExit(e);
        }
    }
}