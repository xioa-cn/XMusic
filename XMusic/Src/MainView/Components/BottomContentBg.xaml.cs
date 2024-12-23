using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Effects;

namespace XMusic.Src.MainView.Components
{
    /// <summary>
    /// 底部背景雪花动画控件
    /// </summary>
    public partial class BottomContentBg : UserControl
    {
        // 随机数生成器，用于生成雪花的随机属性
        private readonly Random _random = new Random();
        
        // 存储雪花及其运动参数的列表
        // snowflake: 雪花图形对象
        // speed: 下落速度
        // amplitude: 摆动幅度
        // frequency: 摆动频率
        // canRotate: 是否可以旋转
        // rotateSpeed: 旋转速度
        private readonly List<(Ellipse snowflake, double speed, double amplitude, double frequency, bool canRotate, double rotateSpeed)> _snowflakes 
            = new List<(Ellipse, double, double, double, bool, double)>();
        
        // 定时器，用于控制动画帧更新
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        
        // 最大雪花数量
        private const int MaxSnowflakes = 70;

        // 添加一个标志来跟踪动画状态
        private bool _isAnimating = false;

        /// <summary>
        /// 获取动画是否正在运行
        /// </summary>
        public bool IsAnimating
        {
            get => _isAnimating;
            private set
            {
                if (_isAnimating != value)
                {
                    _isAnimating = value;
                    IsAnimatingChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// 动画状态改变事件
        /// </summary>
        public event EventHandler<bool> IsAnimatingChanged;

        /// <summary>
        /// 雪花动画参数配置
        /// </summary>
        public class SnowflakeSettings
        {
            /// <summary>
            /// 最大雪花数量
            /// </summary>
            public int MaxSnowflakes { get; set; } = 70;

            /// <summary>
            /// 雪花大小范围（最小值）
            /// </summary>
            public int MinSize { get; set; } = 2;

            /// <summary>
            /// 雪花大小范围（最大值）
            /// </summary>
            public int MaxSize { get; set; } = 8;

            /// <summary>
            /// 雪花透明度范围（最小值）
            /// </summary>
            public int MinOpacity { get; set; } = 30;

            /// <summary>
            /// 雪花透明度范围（最大值）
            /// </summary>
            public int MaxOpacity { get; set; } = 95;

            /// <summary>
            /// 发光效果范围（最小值）
            /// </summary>
            public int MinGlowRadius { get; set; } = 5;

            /// <summary>
            /// 发光效果范围（最大值）
            /// </summary>
            public int MaxGlowRadius { get; set; } = 15;

            /// <summary>
            /// 发光效果透明度范围（最小值）
            /// </summary>
            public double MinGlowOpacity { get; set; } = 0.4;

            /// <summary>
            /// 发光效果透明度范围（最大值）
            /// </summary>
            public double MaxGlowOpacity { get; set; } = 0.8;

            /// <summary>
            /// 下落速度范围（最小值）
            /// </summary>
            public double MinSpeed { get; set; } = 0.5;

            /// <summary>
            /// 下落速度范围（最大值）
            /// </summary>
            public double MaxSpeed { get; set; } = 3.8;

            /// <summary>
            /// 整体速度系数
            /// </summary>
            public double SpeedFactor { get; set; } = 0.5;

            /// <summary>
            /// 摆动幅度范围（最小值）
            /// </summary>
            public double MinAmplitude { get; set; } = 0.3;

            /// <summary>
            /// 摆动幅度范围（最大值）
            /// </summary>
            public double MaxAmplitude { get; set; } = 2.3;

            /// <summary>
            /// 摆动频率范围（最小值）
            /// </summary>
            public double MinFrequency { get; set; } = 0.01;

            /// <summary>
            /// 摆动频率范围（最大值）
            /// </summary>
            public double MaxFrequency { get; set; } = 0.06;

            /// <summary>
            /// 旋转概率（0-1之间）
            /// </summary>
            public double RotationProbability { get; set; } = 0.5;

            /// <summary>
            /// 旋转速度范围（最小值）
            /// </summary>
            public double MinRotationSpeed { get; set; } = -3;

            /// <summary>
            /// 旋转速度范围（最大值）
            /// </summary>
            public double MaxRotationSpeed { get; set; } = 3;
        }

        /// <summary>
        /// 动画参数设置
        /// </summary>
        public SnowflakeSettings Settings { get; set; } = new SnowflakeSettings();

        /// <summary>
        /// 构造函数
        /// </summary>
        public BottomContentBg()
        {
            InitializeComponent();
            // 注册控件生命周期事件
            Loaded += UserControl_Loaded;
            Unloaded += UserControl_Unloaded;
            SizeChanged += UserControl_SizeChanged;
        }

        /// <summary>
        /// 控件加载完成时触发
        /// </summary>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // 默认启动动画
            StartAnimation();
        }

        /// <summary>
        /// 控件卸载时触发，清理资源
        /// </summary>
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            StopAnimation();
        }

        /// <summary>
        /// 控件大小改变时触发，重置雪花
        /// </summary>
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 清理所有现有的雪花，防止雪花位置错乱
            ClearSnowflakes();
        }

        /// <summary>
        /// 启动动画
        /// </summary>
        public void StartAnimation()
        {
            if (IsAnimating) return;

            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            IsAnimating = true;
        }

        /// <summary>
        /// 停止动画
        /// </summary>
        public void StopAnimation()
        {
            if (!IsAnimating) return;

            _timer.Stop();
            _timer.Tick -= Timer_Tick;
            ClearSnowflakes();
            IsAnimating = false;
        }

        /// <summary>
        /// 切换动画状态
        /// </summary>
        public void ToggleAnimation()
        {
            if (IsAnimating)
            {
                StopAnimation();
            }
            else
            {
                StartAnimation();
            }
        }

        /// <summary>
        /// 清理所有雪花
        /// </summary>
        private void ClearSnowflakes()
        {
            // 从画布中移除所有雪花并清空列表
            foreach (var (snowflake, _, _, _, _, _) in _snowflakes)
            {
                MyCanvas.Children.Remove(snowflake);
            }
            _snowflakes.Clear();
        }

        /// <summary>
        /// 定时器触发事件，更新动画帧
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // 如果雪花数量未达到最大值，创建新雪花
            if (_snowflakes.Count < Settings.MaxSnowflakes)
            {
                CreateSnowflake();
            }

            // 更新所有雪花的位置
            AnimateSnowflakes();
        }

        /// <summary>
        /// 创建一个新的雪花
        /// </summary>
        private void CreateSnowflake()
        {
            var snowflake = new Ellipse
            {
                Width = _random.Next(Settings.MinSize, Settings.MaxSize),
                Height = _random.Next(Settings.MinSize, Settings.MaxSize),
                Fill = new SolidColorBrush(Colors.White) 
                { 
                    Opacity = _random.Next(Settings.MinOpacity, Settings.MaxOpacity) / 100.0 
                }
            };

            snowflake.Effect = new DropShadowEffect
            {
                Color = Colors.White,
                Direction = 0,
                ShadowDepth = 0,
                BlurRadius = _random.Next(Settings.MinGlowRadius, Settings.MaxGlowRadius),
                Opacity = _random.NextDouble() * 
                    (Settings.MaxGlowOpacity - Settings.MinGlowOpacity) + Settings.MinGlowOpacity
            };

            snowflake.RenderTransformOrigin = new Point(0.5, 0.5);
            snowflake.RenderTransform = new RotateTransform();

            Canvas.SetLeft(snowflake, _random.Next(0, (int)MyCanvas.ActualWidth));
            Canvas.SetTop(snowflake, -10);

            var speed = _random.NextDouble() * 
                (Settings.MaxSpeed - Settings.MinSpeed) + Settings.MinSpeed;
            var amplitude = _random.NextDouble() * 
                (Settings.MaxAmplitude - Settings.MinAmplitude) + Settings.MinAmplitude;
            var frequency = _random.NextDouble() * 
                (Settings.MaxFrequency - Settings.MinFrequency) + Settings.MinFrequency;

            var canRotate = _random.NextDouble() < Settings.RotationProbability;
            var rotateSpeed = _random.NextDouble() * 
                (Settings.MaxRotationSpeed - Settings.MinRotationSpeed) + Settings.MinRotationSpeed;

            _snowflakes.Add((snowflake, speed, amplitude, frequency, canRotate, rotateSpeed));
            MyCanvas.Children.Add(snowflake);
        }

        /// <summary>
        /// 更新所有雪花的位置
        /// </summary>
        private void AnimateSnowflakes()
        {
            // 从后向前遍历，方便删除元素
            for (int i = _snowflakes.Count - 1; i >= 0; i--)
            {
                var (snowflake, speed, amplitude, frequency, canRotate, rotateSpeed) = _snowflakes[i];
                var top = Canvas.GetTop(snowflake);
                var left = Canvas.GetLeft(snowflake);

                // 如果雪花超出底部，则移除
                if (top > MyCanvas.ActualHeight)
                {
                    MyCanvas.Children.Remove(snowflake);
                    _snowflakes.RemoveAt(i);
                    continue;
                }

                // 使用更平滑的正弦函数计算水平位置
                var time = DateTime.Now.Ticks / 10000000.0; // 转换为秒
                // 使用初始位置作为基准点，避免突然的位置变化
                var baseX = Canvas.GetLeft(snowflake);
                var offset = amplitude * Math.Sin(frequency * time);
                var newLeft = baseX + offset * 0.1; // 减小位移量，使运动更加柔和

                // 确保雪花不会超出画布边界
                if (newLeft < 0) 
                {
                    newLeft = 0;
                }
                else if (newLeft > MyCanvas.ActualWidth - snowflake.Width)
                {
                    newLeft = MyCanvas.ActualWidth - snowflake.Width;
                }

                // 平滑过渡到新位置
                var currentLeft = Canvas.GetLeft(snowflake);
                var smoothedLeft = currentLeft + (newLeft - currentLeft) * 0.1; // 使用插值让运动更平滑

                // 更新雪花位置
                Canvas.SetLeft(snowflake, smoothedLeft);
                Canvas.SetTop(snowflake, top + speed * Settings.SpeedFactor);

                // 更新旋转
                if (canRotate)
                {
                    var transform = (RotateTransform)snowflake.RenderTransform;
                    transform.Angle = (transform.Angle + rotateSpeed) % 360;
                }
            }
        }
    }
}
