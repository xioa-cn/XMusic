using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.ComponentModel;

namespace XMusic.Src.MainView.Components
{
    /// <summary>
    /// BottomContentRight.xaml 的交互逻辑
    /// </summary>
    public partial class BottomContentRight : UserControl
    {
        private ObservableCollection<LyricItem> _lyrics;
        private bool _isUserScrolling = false;
        private System.Timers.Timer _scrollTimer;

        public BottomContentRight()
        {
            InitializeComponent();
            _lyrics = new ObservableCollection<LyricItem>();
            LyricList.ItemsSource = _lyrics;

            _scrollTimer = new System.Timers.Timer(1500);
            _scrollTimer.Elapsed += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    _isUserScrolling = false;
                    _scrollTimer.Stop();
                });
            };

            // 添加一些测试歌词
            var lyrics = new List<LyricItem>
            {
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
                new LyricItem { Text = "第1句歌词", TimeStamp = TimeSpan.FromSeconds(0) },
                new LyricItem { Text = "第2句歌词", TimeStamp = TimeSpan.FromSeconds(3) },
                new LyricItem { Text = "第3句歌词", TimeStamp = TimeSpan.FromSeconds(9) },
                new LyricItem { Text = "第4句歌词", TimeStamp = TimeSpan.FromSeconds(12) },
                new LyricItem { Text = "第5句歌词", TimeStamp = TimeSpan.FromSeconds(15) },
                new LyricItem { Text = "第6句歌词", TimeStamp = TimeSpan.FromSeconds(18) },
                new LyricItem { Text = "第7句歌词", TimeStamp = TimeSpan.FromSeconds(21) },
                new LyricItem { Text = "第8句歌词", TimeStamp = TimeSpan.FromSeconds(24) },
                new LyricItem { Text = "第9句歌词", TimeStamp = TimeSpan.FromSeconds(27) },
                new LyricItem { Text = "第10句歌词", TimeStamp = TimeSpan.FromSeconds(30) },
                new LyricItem { Text = "第11句歌词", TimeStamp = TimeSpan.FromSeconds(33) },
            };
            this.UpdateLyrics(lyrics);

            // 启动测试
            TestLyricScroll();
        }

       

        private void LyricScroller_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            _isUserScrolling = true;
            _scrollTimer.Stop();
            _scrollTimer.Start();
        }

        private void Update()
        {
            // 设置当前播放的歌词（比如根据播放时间）
            this.SetActiveLyric(1); // 激活第二句歌词
        }

        // 更新歌词方法
        public void UpdateLyrics(List<LyricItem> lyrics)
        {
            _lyrics.Clear();
            foreach (var lyric in lyrics)
            {
                _lyrics.Add(lyric);
            }
        }

        // 设置当前活动歌词
        public void SetActiveLyric(int index)
        {
            if (index < 0 || index >= _lyrics.Count)
                return;

            // 更新活跃状态
            for (int i = 0; i < _lyrics.Count; i++)
            {
                _lyrics[i].IsActive = (i == index);
            }

            // 只有在用户没有手动滚动时才自动滚动到当前歌词
            if (!_isUserScrolling)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    // 计算目标滚动位置
                    double itemHeight = 40; // 每个歌词项的高度
                    double paddingTop = 80; // StackPanel 的上边距
                    
                    // 计算目标位置（考虑上边距）
                    double targetOffset = (index * itemHeight) + paddingTop;
                    
                    // 调整位置使当前歌词位于中间
                    double viewportCenter = LyricScroller.ViewportHeight / 2;
                    double scrollOffset = targetOffset - viewportCenter;
                    
                    // 使用平滑滚动
                    SmoothScrollTo(Math.Max(0, scrollOffset));
                    
                }), System.Windows.Threading.DispatcherPriority.Loaded);
            }
        }

        // 添加平滑滚动方法
        private void SmoothScrollTo(double targetOffset)
        {
            const int duration = 300; // 滚动持续时间（毫秒）
            const int frames = 30;    // 动画帧数
            double startOffset = LyricScroller.VerticalOffset;
            double distance = targetOffset - startOffset;
            double step = distance / frames;
            
            var timer = new System.Windows.Threading.DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(duration / frames)
            };

            int frame = 0;
            timer.Tick += (s, e) =>
            {
                frame++;
                if (frame >= frames)
                {
                    LyricScroller.ScrollToVerticalOffset(targetOffset);
                    timer.Stop();
                }
                else
                {
                    double progress = (double)frame / frames;
                    double easedProgress = 1 - Math.Pow(1 - progress, 3); // 缓出效果
                    double currentOffset = startOffset + (distance * easedProgress);
                    LyricScroller.ScrollToVerticalOffset(currentOffset);
                }
            };

            timer.Start();
        }

        // 添加一个测试方法，用于模拟歌词滚动
        private async void TestLyricScroll()
        {
            for (int i = 0; i < _lyrics.Count; i++)
            {
                await Task.Delay(2000); // 每2秒切换一次歌词
                SetActiveLyric(i);
            }
        }
    }

    // 歌词项数据类
    public partial class LyricItem : ObservableObject
    {
        [ObservableProperty]
        private string _text;

        [ObservableProperty]
        private bool _isActive;

        [ObservableProperty]
        private TimeSpan _timeStamp;
    }
}
