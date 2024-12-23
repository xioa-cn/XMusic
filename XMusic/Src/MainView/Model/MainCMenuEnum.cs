using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using XMusic.Src.Utils;

namespace XMusic.Src.MainView.Model
{
    public enum MainCMenuEnum
    {
        Mini,
        Max,
        Normal,
        Notify,
        Close,
    }

    public static class MainMenuHelper
    {
        private static Rect? rcnormal;
        public static void Run(this MainCMenuEnum cMenuenum)
        {
            rcnormal ??= new Rect(App.Window.Left, App.Window.Top, App.Window.Width, App.Window.Height);

            if (cMenuenum == MainCMenuEnum.Mini)
            {
                App.Window.WindowState = System.Windows.WindowState.Minimized;
            }

            else if (cMenuenum == MainCMenuEnum.Notify)
            {
                App.Window.Visibility = System.Windows.Visibility.Collapsed;
            }

            else if (cMenuenum == MainCMenuEnum.Max)
            {
                App.Window.Left = 0;
                App.Window.Top = 0;
                Rect rc = SystemParameters.WorkArea;
                App.Window.Width = rc.Width;
                App.Window.Height = rc.Height;
            }

            else if (cMenuenum == MainCMenuEnum.Normal)
            {
                App.Window.Left = rcnormal.Value.Left;
                App.Window.Top = rcnormal.Value.Top;
                App.Window.Width = rcnormal.Value.Width;
                App.Window.Height = rcnormal.Value.Height;
            }

            else if (cMenuenum == MainCMenuEnum.Close)
            {
                GlobalHelper.Close();
            }
        }
    }
}
