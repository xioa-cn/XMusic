using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace XMusic.Src.MainView.Components
{
    /// <summary>
    /// BottomContentBg.xaml 的交互逻辑
    /// </summary>
    public partial class BottomContentBg : UserControl
    {
        public BottomContentBg()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// nuget套件 MahApps.Metro.IconPacks
        /// 三种雪花样式，下降加旋转动画
        /// </summary>
        /// <param name="panel"></param>
        void StartSnowing(Canvas panel)
        {
            Random random = new Random();
            Task.Factory.StartNew(new Action(() =>
            {
                for (int j = 0; j < 25; j++)
                {
                    Thread.Sleep(j * 100);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        int snowCount = random.Next(0, 10);
                        for (int i = 0; i < snowCount; i++)
                        {
                            int width = random.Next(10, 20);
                            PackIconBase pack = null;
                            int snowType = random.Next(3);                   //三种雪花
                            switch (snowType)
                            {
                                case 0: pack = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.SnowflakeRegular }; break;
                                case 1: pack = new PackIconMaterial() { Kind = PackIconMaterialKind.Snowflake }; break;
                                case 2: pack = new PackIconModern() { Kind = PackIconModernKind.Snowflake }; break;
                                default:
                                    break;
                            }
                            pack.Width = width;
                            pack.Height = width;
                            pack.Foreground = Brushes.White;              //白色
                            pack.BorderThickness = new Thickness(0);
                            pack.RenderTransform = new RotateTransform();

                            int left = random.Next(0, (int)panel.ActualWidth);
                            Canvas.SetLeft(pack, left);
                            panel.Children.Add(pack);
                            int seconds = random.Next(20, 30);
                            DoubleAnimationUsingPath doubleAnimation = new DoubleAnimationUsingPath()        //下降动画
                            {
                                Duration = new Duration(new TimeSpan(0, 0, seconds)),
                                RepeatBehavior = RepeatBehavior.Forever,
                                PathGeometry = new PathGeometry(new List<PathFigure>() { new PathFigure(new Point(left, 0), new List<PathSegment>() { new LineSegment(new Point(left, panel.ActualHeight), false) }, false) }),
                                Source = PathAnimationSource.Y
                            };
                            pack.BeginAnimation(Canvas.TopProperty, doubleAnimation);
                            DoubleAnimation doubleAnimation1 = new DoubleAnimation(360, new Duration(new TimeSpan(0, 0, 10)))              //旋转动画
                            {
                                RepeatBehavior = RepeatBehavior.Forever,

                            };
                            pack.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, doubleAnimation1);
                        }
                    }));
                }
            }));
        }
    }
}
