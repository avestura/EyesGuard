using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EyesGuard.Resources.Controls
{
    public partial class WaveCircle : UserControl
    {

        public TimeSpan SignalDuration
        {
            get { return (TimeSpan)GetValue(SignalDurationProperty); }
            set { SetValue(SignalDurationProperty, value); }
        }

        public static readonly DependencyProperty SignalDurationProperty =
            DependencyProperty.Register("SignalDuration", typeof(TimeSpan), typeof(WaveCircle));

        public double CircleArea
        {
            get { return (double)GetValue(CircleAreaProperty); }
            set { SetValue(CircleAreaProperty, value); }
        }
        public static readonly DependencyProperty CircleAreaProperty =
            DependencyProperty.Register("CircleArea", typeof(double), typeof(WaveCircle));

        public double MaximumSignalArea
        {
            get { return (double)GetValue(MaximumSignalAreaProperty); }
            set { SetValue(MaximumSignalAreaProperty, value); }
        }
        public static readonly DependencyProperty MaximumSignalAreaProperty =
            DependencyProperty.Register("MaximumSignalArea", typeof(double), typeof(WaveCircle));

        public System.Windows.Media.Brush WaveStroke
        {
            get { return (System.Windows.Media.Brush)GetValue(WaveStrokeProperty); }
            set { SetValue(WaveStrokeProperty, value); }
        }

        public static readonly DependencyProperty WaveStrokeProperty =
            DependencyProperty.Register("WaveStroke", typeof(System.Windows.Media.Brush), typeof(WaveCircle));

        public Ellipse Ellipse { get { return MainEllipse; } }

        public bool IsSignaling
        {
            get { return (bool)GetValue(IsSignalingProperty); }
            set
            {
                SetValue(IsSignalingProperty, value);

                try
                {
                    if (value)
                    {
                        StartSignal();
                    }
                    else
                    {
                        StopSignal();
                    }
                }
                catch { }
            }
        }

        public static readonly DependencyProperty IsSignalingProperty =
            DependencyProperty.Register("IsSignaling", typeof(bool), typeof(WaveCircle));

        DoubleAnimation widthAnim;

        DoubleAnimation heightAnim;

        DoubleAnimation fadeAnim;

        Storyboard story = new Storyboard();

        public WaveCircle()
        {
            InitializeComponent();

            if (SignalDuration.Milliseconds == 0)
                SignalDuration = TimeSpan.FromSeconds(2);

        }

        private void SetAnimations()
        {
            widthAnim = new DoubleAnimation()
            {
                From = CircleArea,
                To = MaximumSignalArea,
                Duration = SignalDuration,
                EasingFunction = new QuadraticEase(),
                RepeatBehavior = RepeatBehavior.Forever
            };

            heightAnim = new DoubleAnimation()
            {
                From = CircleArea,
                To = MaximumSignalArea,
                Duration = SignalDuration,
                EasingFunction = new QuadraticEase(),
                RepeatBehavior = RepeatBehavior.Forever
            };

            fadeAnim = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = SignalDuration,
                EasingFunction = new QuadraticEase(),
                RepeatBehavior = RepeatBehavior.Forever
            };
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MainEllipse_Loaded(object sender, RoutedEventArgs e)
        {

            SetAnimations();

            Storyboard.SetTargetProperty(widthAnim, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(widthAnim, MainEllipse);

            Storyboard.SetTargetProperty(heightAnim, new PropertyPath(Ellipse.HeightProperty));
            Storyboard.SetTarget(heightAnim, MainEllipse);

            Storyboard.SetTargetProperty(fadeAnim, new PropertyPath(Ellipse.OpacityProperty));
            Storyboard.SetTarget(fadeAnim, MainEllipse);

            story.Children.Add(widthAnim);
            story.Children.Add(heightAnim);
            story.Children.Add(fadeAnim);

            if (IsSignaling)
                StartSignal();
        }

        private void StartSignal()
        {
            MainEllipse.BeginStoryboard(story, HandoffBehavior.SnapshotAndReplace, false);
        }
        private void StopSignal()
        {
            MainEllipse.BeginStoryboard(null, HandoffBehavior.SnapshotAndReplace);
        }
    }
}
