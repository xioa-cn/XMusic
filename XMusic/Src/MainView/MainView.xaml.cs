using System.Windows.Controls;
using System.Windows.Media;
using XMusic.Src.MainView.Page;

namespace XMusic.Src.MainView;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    private BasePage BasePage { get; set; } = new BasePage();
    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        this.frame.Navigate(BasePage);
    }
}