using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using FontAwesome.WPF;


namespace EyesGuard
{
    public static class Animation
    {

        public static void MoveTo(this ImageAwesome image, double from, double to, TimeSpan? duration = null)
        {
            if (duration == null) duration = new TimeSpan(0, 0, 1);
            var anim = new DoubleAnimation()
            {
                Duration = duration.Value,
                From = from,
                To = to,
                EasingFunction = new QuadraticEase(),
                By = (from + to) / 2
            };
            image.BeginAnimation(ImageAwesome.SpinDurationProperty, anim);
           
        }
        
        public static void MarginFadeInAnimation(this FrameworkElement element, Thickness from, Thickness to, TimeSpan? duration = null, bool useFade = true, bool makeVisible = true)
        {
            if (duration == null) duration = new TimeSpan(0, 0, 1);
            var storyboard = new Storyboard();

            var thicknessAnim = new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = duration.Value,
                EasingFunction = new QuadraticEase()
            };

            Storyboard.SetTarget(thicknessAnim, element);
            Storyboard.SetTargetProperty(thicknessAnim, new PropertyPath(FrameworkElement.MarginProperty));

            storyboard.Children.Add(thicknessAnim);
            
            if (useFade)
            {
                var fadeAnim = new DoubleAnimation()
                {
                    From = 0,
                    To = 1,
                    Duration = duration.Value,
                    EasingFunction = new QuadraticEase()
                };

                Storyboard.SetTarget(fadeAnim, element);
                Storyboard.SetTargetProperty(fadeAnim, new PropertyPath(UIElement.OpacityProperty));

                storyboard.Children.Add(fadeAnim);
            }

            if (useFade)
            {
                element.Opacity = 0;
                element.Visibility = Visibility.Visible;
            }
            element.BeginStoryboard(storyboard);

        }

        public static void MarginFadeOutAnimation(this FrameworkElement element, Thickness from, Thickness to, TimeSpan? duration = null, bool useFade = true, bool collapse = true)
        {
            if (duration == null) duration = new TimeSpan(0, 0, 1);
            var storyboard = new Storyboard();
            storyboard.Completed += (sender, ev) =>
            {
                if (useFade) element.Visibility = Visibility.Collapsed;
            };
            var thicknessAnim = new ThicknessAnimation()
            {
                From = from,
                To = to,
                Duration = duration.Value,
                EasingFunction = new QuadraticEase()
            };

            Storyboard.SetTarget(thicknessAnim, element);
            Storyboard.SetTargetProperty(thicknessAnim, new PropertyPath(FrameworkElement.MarginProperty));

            storyboard.Children.Add(thicknessAnim);

            if (useFade)
            {
                var fadeAnim = new DoubleAnimation()
                {
                    From = 1,
                    To = 0,
                    Duration = duration.Value,
                    EasingFunction = new QuadraticEase()
                };
                

                Storyboard.SetTarget(fadeAnim, element);
                Storyboard.SetTargetProperty(fadeAnim, new PropertyPath(UIElement.OpacityProperty));

                storyboard.Children.Add(fadeAnim);
            }

            element.BeginStoryboard(storyboard);

        }

        #region Animation :: Show And Fade
        public static void HideUsingLinearAnimation(this UIElement element, int milliSeconds = 500)
        {
            if (element == null) return;
            var anim = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = new TimeSpan(0, 0, 0, 0, milliSeconds)
            };

            anim.Completed += new EventHandler((sender, e) =>
            {
                element.Visibility = Visibility.Collapsed;
            });
            element.Opacity = 1;
            element.Visibility = Visibility.Visible;
            element.BeginAnimation(UIElement.OpacityProperty, anim);



        }

        public static void ShowUsingLinearAnimation(this UIElement element, int milliSeconds = 500)
        {
            if (element == null) return;
            var anim = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = new TimeSpan(0, 0, 0, 0, milliSeconds)
            };

            element.Opacity = 0;
            element.Visibility = Visibility.Visible;
            element.BeginAnimation(UIElement.OpacityProperty, anim);

        }

        public static Task HideUsingLinearAnimationAsync(this UIElement element, int milliSeconds = 500)
        {
            return Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() =>
                {
                    HideUsingLinearAnimation(element, milliSeconds);
                });
                await Task.Delay(milliSeconds);
            });
        }

        public static Task ShowUsingLinearAnimationAsync(this UIElement element, int milliSeconds = 500)
        {
            return Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() =>
                {
                    ShowUsingLinearAnimation(element, milliSeconds);
                });
                await Task.Delay(milliSeconds);
            });
        }
        #endregion

        #region Animation :: Blink (Easing and Linear)
        public static void BlinkLinear(this UIElement element, TimeSpan duration, double opacityStart = 0, double opacityEnd = 1)
        {
            var animation = new DoubleAnimationUsingKeyFrames() { Duration = duration, RepeatBehavior = RepeatBehavior.Forever };
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0), Value = opacityStart });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0.5), Value = opacityEnd });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(1), Value = opacityStart });

            element.BeginAnimation(UIElement.OpacityProperty, animation, HandoffBehavior.SnapshotAndReplace);
        }

        public static void BlinkLinear(this UIElement element, int milliSecondDuration = 1000, double opacityStart = 0, double opacityEnd = 1)
        {
            element.BlinkLinear(TimeSpan.FromMilliseconds(milliSecondDuration), opacityStart, opacityEnd);
        }

        public static void BlinkEasing(this UIElement element, TimeSpan duration, double opacityStart = 0, double opacityEnd = 1)
        {
            var animation = new DoubleAnimationUsingKeyFrames() { Duration = duration, RepeatBehavior = RepeatBehavior.Forever };
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0), Value = opacityStart });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0.5), Value = opacityEnd });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(1), Value = opacityStart });

            element.BeginAnimation(UIElement.OpacityProperty, animation, HandoffBehavior.SnapshotAndReplace);
        }

        public static void BlinkEasing(this UIElement element, int milliSecondDuration = 1000, double opacityStart = 0, double opacityEnd = 1)
        {
            element.BlinkEasing(TimeSpan.FromMilliseconds(milliSecondDuration), opacityStart, opacityEnd);
        }
        #endregion



    }
}
