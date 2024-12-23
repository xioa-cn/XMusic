using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Hardcodet.Wpf.TaskbarNotification;
using XMusic.Src.NotifyView.ViewModel;

namespace XMusic;

/// <summary>
/// @author Xioa
/// @date  2024年12月23日
/// </summary>
public partial class App
{
    private static TaskbarIcon? _notifyIcon;
    public static NotifyViewModel NotifyViewModel { get; set; } = new NotifyViewModel();

    private void NotifyIconInitialize()
    {
        Binding binding = new Binding();
        binding.Source = NotifyViewModel;
        binding.Path = new PropertyPath("Title");
        binding.Mode = BindingMode.TwoWay;
        _notifyIcon = new TaskbarIcon
        {
            DataContext = NotifyViewModel,
            Icon = new System.Drawing.Icon("Assets/logo/logo_32x32.ico"),
            ContextMenu = new ContextMenu()
            {
                Style = (Style)FindResource("Notify")
            },
        };
        _notifyIcon.SetBinding(TaskbarIcon.ToolTipTextProperty, binding);
    }
}