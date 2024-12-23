
namespace XMusic;

public partial class App
{
    public static WPF.Xlog.Logger.Impl.ILogService? Logger => WPF.Xlog.Logger.Service.LogService.Instance;
}