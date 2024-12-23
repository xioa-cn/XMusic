using System.Windows;

namespace XMusic.Src.NotifyView.Attached;

public static class BtnIsCheckedAttached
{
    public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached(
        "IsChecked",
        typeof(bool),
        typeof(BtnIsCheckedAttached),
        new PropertyMetadata(true));

    public static bool GetIsChecked(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsCheckedProperty);
    }

    public static void SetIsChecked(DependencyObject obj, bool value)
    {
        obj.SetValue(IsCheckedProperty, value);
    }
}