using System;
using System.Windows;

namespace XMusic.Src.Utils;

public static class GlobalHelper
{
    public static void Close()
    {
        App.MainWindowClose();
        Application.Current.Shutdown();
        Environment.Exit(0);
    }

    public static void Open()
    {
        App.Window.WindowState = System.Windows.WindowState.Normal;
        App.Window.Visibility = System.Windows.Visibility.Visible;
        App.Window.Activate();
    }
}