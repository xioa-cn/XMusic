using System.Windows.Controls;

namespace XMusic.Src.NotifyView;

public partial class NotifyView : UserControl
{
    public NotifyView()
    {
        this.DataContext = App.NotifyViewModel;
        InitializeComponent();
    }
}