using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace EyesGuard.Animations
{
    public static class BlinkAnimations
    {
        public static void BlinkLinear(
            this UIElement element,
            TimeSpan duration,
            double opacityStart = 0,
            double opacityEnd = 1)
        {
            var animation = new DoubleAnimationUsingKeyFrames() { Duration = duration, RepeatBehavior = RepeatBehavior.Forever };
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0), Value = opacityStart });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0.5), Value = opacityEnd });
            animation.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(1), Value = opacityStart });

            element.BeginAnimation(UIElement.OpacityProperty, animation, HandoffBehavior.SnapshotAndReplace);
        }

        public static void BlinkLinear(
            this UIElement element,
            int milliSecondDuration = 1000,
            double opacityStart = 0,
            double opacityEnd = 1)
        {
            element.BlinkLinear(TimeSpan.FromMilliseconds(milliSecondDuration), opacityStart, opacityEnd);
        }

        public static void BlinkEasing(
            this UIElement element,
            TimeSpan duration,
            double opacityStart = 0,
            double opacityEnd = 1)
        {
            var animation = new DoubleAnimationUsingKeyFrames() { Duration = duration, RepeatBehavior = RepeatBehavior.Forever };
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0), Value = opacityStart });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(0.5), Value = opacityEnd });
            animation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(1), Value = opacityStart });

            element.BeginAnimation(UIElement.OpacityProperty, animation, HandoffBehavior.SnapshotAndReplace);
        }

        public static void BlinkEasing(
            this UIElement element,
            int milliSecondDuration = 1000,
            double opacityStart = 0,
            double opacityEnd = 1)
        {
            element.BlinkEasing(TimeSpan.FromMilliseconds(milliSecondDuration), opacityStart, opacityEnd);
        }
    }
}
