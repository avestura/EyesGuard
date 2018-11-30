using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EyesGuard.Views.Animations
{
    public static class ShowAndFadeAnimations
    {
        public static void HideUsingLinearAnimation(
            this UIElement element,
            int milliSeconds = 500,
            IEasingFunction easingFunction = null)
        {
            if (element == null) return;
            var anim = new DoubleAnimation()
            {
                From = 1,
                To = 0,
                Duration = new TimeSpan(0, 0, 0, 0, milliSeconds),
            };
            if (easingFunction != null) anim.EasingFunction = easingFunction;

            anim.Completed += new EventHandler((sender, e) => element.Visibility = Visibility.Collapsed);
            element.Opacity = 1;
            element.Visibility = Visibility.Visible;
            element.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        public static Task HideUsingLinearAnimationAsync(
            this UIElement element,
            int milliSeconds = 500,
            IEasingFunction easingFunction = null)
        {
            return Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() => HideUsingLinearAnimation(element, milliSeconds, easingFunction));
                await Task.Delay(milliSeconds);
            });
        }

        public static void ShowUsingLinearAnimation(
            this UIElement element,
            int milliSeconds = 500,
            IEasingFunction easingFunction = null)
        {
            if (element == null) return;
            var anim = new DoubleAnimation()
            {
                From = 0,
                To = 1,
                Duration = new TimeSpan(0, 0, 0, 0, milliSeconds)
            };
            if (easingFunction != null) anim.EasingFunction = easingFunction;

            element.Opacity = 0;
            element.Visibility = Visibility.Visible;
            element.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        public static Task ShowUsingLinearAnimationAsync(
            this UIElement element,
            int milliSeconds = 500,
            IEasingFunction easingFunction = null)
        {
            return Task.Run(async () =>
            {
                if (element == null) return;
                element.Dispatcher.Invoke(() => ShowUsingLinearAnimation(element, milliSeconds, easingFunction));
                await Task.Delay(milliSeconds);
            });
        }
    }
}
