using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace XMusic
{
    public partial class App
    {
        public static Window Window { get; private set; } = new MainWindow();

        public static void MainWindowClose()
        {
            App.Window.Close();
            App._notifyIcon?.Dispose();
        }
    }
}
