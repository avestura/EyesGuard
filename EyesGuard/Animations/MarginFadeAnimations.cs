using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EyesGuard.Animations
{
    public static class MarginFadeAnimations
    {
        public static void MarginFadeInAnimation(
            this FrameworkElement element,
            Thickness from,
            Thickness to,
            TimeSpan? duration = null,
            bool useFade = true,
            bool makeVisible = false)
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

        public static async Task MarginFadeInAnimationAsync(
            this FrameworkElement element,
            Thickness from,
            Thickness to,
            TimeSpan? duration = null,
            bool useFade = true,
            bool makeVisible = true)
        {
            if (duration == null) duration = new TimeSpan(0, 0, 1);
            await Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() => element.MarginFadeInAnimation(from, to, duration, useFade, makeVisible));
                await Task.Delay(duration.Value);
            });
        }

        public static async Task MarginFadeOutAnimationAsync(
            this FrameworkElement element,
            Thickness from,
            Thickness to,
            TimeSpan? duration = null,
            bool useFade = true,
            bool makeVisible = true)
        {
            if (duration == null) duration = new TimeSpan(0, 0, 1);
            await Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() => element.MarginFadeOutAnimation(from, to, duration, useFade, makeVisible));
                await Task.Delay(duration.Value);
            });
        }

        public static void MarginFadeOutAnimation(
            this FrameworkElement element,
            Thickness from,
            Thickness to,
            TimeSpan?
            duration = null,
            bool useFade = true,
            bool collapse = true)
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
    }
}
