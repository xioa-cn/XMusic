using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using CommunityToolkit.Mvvm.Messaging;

namespace XMusic.Src.MainView.Views
{
    /// <summary>
    /// MusicLabPage.xaml 的交互逻辑
    /// </summary>
    public partial class MusicLabPage : System.Windows.Controls.Page
    {


        public MusicLabPage()
        {
            InitializeComponent();

        }

        private void Player_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var content = button.Tag as MusicFile;
                if (content != null)
                    WeakReferenceMessenger.Default.Send(content);
            }
        }
    }

    // Class to represent a music file
    public class MusicFile
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsPlayer { get; set; } = false;
    }
}
