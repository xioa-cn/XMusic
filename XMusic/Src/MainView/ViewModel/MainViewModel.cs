using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using XMusic.Src.MainView.Model;
using XMusic.Src.MainView.Views;

namespace XMusic.Src.MainView.ViewModel;

public partial class MainViewModel : ObservableObject
{
    #region ContentVisibility

    [ObservableProperty] private Visibility _contentVisibility = Visibility.Collapsed;
    [RelayCommand]
    private void ShowContent()
    {
        ContentVisibility = Visibility.Visible;
    }
    [RelayCommand]
    private void NoShowContent()
    {
        ContentVisibility = Visibility.Collapsed;
        MainCMenuEnum.Normal.Run();
    }

    #endregion

    public MainViewModel()
    {
        wavePlayer = new DirectSoundOut();
        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromMilliseconds(500);
        timer.Tick += Timer_Tick;
        wavePlayer.PlaybackStopped += OnPlaybackStopped;
        _musicLabPage = new MusicLabPage() { DataContext = this, };
        WeakReferenceMessenger.Default.Register<MusicFile>(this, PlayMusic);
        WeakReferenceMessenger.Default.Register<Messenger<string>>(this, StringPlay);
    }

    

    private MusicLabPage _musicLabPage;
    [RelayCommand]
    public void GoToViewMusicLab(Frame frame)
    {
        frame.Navigate(_musicLabPage);
    }

    #region ContentMenu

    private bool currentStatusIsMax = false;
    [RelayCommand]
    private void WindowMethod(string mType)
    {
        var value = Enum.Parse<MainCMenuEnum>(mType);
        if (value == MainCMenuEnum.Max && currentStatusIsMax)
        {
            MainCMenuEnum.Normal.Run();
            currentStatusIsMax = false;
            return;
        }
        else
        {
            currentStatusIsMax = true;
        }
        value.Run();
    }

    #endregion

    public ObservableCollection<MusicFile> MusicFiles { get; } = new ObservableCollection<MusicFile>();

    [RelayCommand]
    private void OnSelectFolderClicked()
    {

        var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
        if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            string folderPath = folderDialog.SelectedPath;
            LoadMusicFiles(folderPath);
        }
    }
    private void LoadMusicFiles(string folderPath)
    {
        MusicFiles.Clear();
        var mp3Files = Directory.GetFiles(folderPath, "*.mp3", SearchOption.AllDirectories);
        foreach (var file in mp3Files)
        {
            MusicFiles.Add(new MusicFile
            {
                FileName = Path.GetFileName(file),
                FilePath = file
            });
        }
    }
}