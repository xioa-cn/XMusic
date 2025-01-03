using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using XMusic.Src.MainView.Model;
using XMusic.Src.MainView.Views;

namespace XMusic.Src.MainView.ViewModel
{
    public partial class MainViewModel
    {
        [ObservableProperty] private string _playerInfo;
        private IWavePlayer? wavePlayer;
        private WaveStream? audioStream;

        private DispatcherTimer timer;
        private bool _isLooping;

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            if (ManualStop)
                return;
            if (_isLooping && audioStream != null)
            {
                audioStream.Position = 0;
                wavePlayer.Play();
            }
            else if (!_isLooping && audioStream != null)
            {
                Next();
            }
        }

        public int PlayIndex { get; set; } = -1;

        private void PlayMusic(object recipient, MusicFile message)
        {
            if (message.IsPlayer) return;
            var r =
                MusicFiles.FirstOrDefault(e => e.IsPlayer);
            if (r is not null)
                r.IsPlayer = false;
            var r1 =
               MusicFiles.FirstOrDefault(e => e == message);
            if (r1 is null) return;
            PlayIndex = Array.IndexOf(MusicFiles.ToArray(), message);
            r1.IsPlayer = true;
            Load(message.FilePath);
            PlayerInfo = message.FileName.Replace(".mp3", "");
            WeakReferenceMessenger.Default.Send(new Messenger<ChangeTitle>() { data = new ChangeTitle { Title = PlayerInfo } });
            this.Play();
        }

        [RelayCommand]
        public void Next()
        {
            if (PlayIndex == -1)
            {
                if (MusicFiles.Count > 0)
                {
                    PlayMusic(null, MusicFiles[0]);
                }
                return;
            }

            if (MusicFiles.Count > 0 && MusicFiles.Count == PlayIndex + 1)
            {
                PlayMusic(null, MusicFiles[0]);
                return;
            }
            else if (MusicFiles.Count > 0 && MusicFiles.Count > PlayIndex + 1)
            {
                PlayMusic(null, MusicFiles[PlayIndex + 1]);
                return;
            }
        }

        [RelayCommand]
        public void Before()
        {
            if (PlayIndex == -1)
            {
                if (MusicFiles.Count > 0)
                {
                    PlayMusic(null, MusicFiles[MusicFiles.Count - 1]);
                }
                return;
            }

            if (MusicFiles.Count > 0 && PlayIndex - 1 < 0)
            {
                PlayMusic(null, MusicFiles[MusicFiles.Count - 1]);
                return;
            }
            else if (MusicFiles.Count > 0 && MusicFiles.Count > PlayIndex - 1 && PlayIndex - 1 > 0)
            {
                PlayMusic(null, MusicFiles[PlayIndex - 1]);
                return;
            }
        }

        public bool IsLooping
        {
            get => _isLooping;
            set => SetProperty(ref _isLooping, value);
        }

        public void Load(string fileName)
        {
            Stop();
            //wavePlayer = new DirectSoundOut();
            //wavePlayer.PlaybackStopped += OnPlaybackStopped;

            Thread.Sleep(500);
            audioStream = new AudioFileReader(fileName);
            wavePlayer?.Init(audioStream);
            OnPropertyChanged(nameof(TotalTime));
            TotalTimeString = audioStream?.TotalTime.ToString(@"hh\:mm\:ss");
        }

        public void Play()
        {
            ManualStop = false;
            if (wavePlayer != null && audioStream != null)
            {
                wavePlayer.Play();
                timer.Start();
            }
        }
        private void StringPlay(object recipient, Messenger<string> message)
        {
            if (message.data == "NEXT")
            {
                Next();
            }
            else if (message.data == "BEFORE")
            {
                Before();
            }
            else if (message.data == "STOP")
            {
                Stop();
            }
            else if (message.data == "START")
            {
                Play();
            }
        }

        private bool ManualStop = false;
        public void Stop()
        {
            ManualStop = true;
            wavePlayer?.Stop();
            //wavePlayer?.Dispose();
            timer.Stop();
        }

        public void Dispose()
        {
            wavePlayer?.Dispose();
            audioStream?.Dispose();
        }

        private double _currentPosition;

        public double CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                if (_currentPosition != value)
                {
                    if (audioStream is not null)
                        audioStream.CurrentTime = TimeSpan.FromSeconds(value);
                    _currentPosition = value;
                    OnPropertyChanged();
                }
            }
        }

        [ObservableProperty] private string? _currentTime;
        [ObservableProperty] private string? _totalTimeString;

        public double TotalTime => audioStream?.TotalTime.TotalSeconds ?? 0;

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (audioStream != null)
            {
                CurrentPosition = audioStream.CurrentTime.TotalSeconds;
                CurrentTime = audioStream.CurrentTime.ToString(@"hh\:mm\:ss");
            }
        }
    }
}
