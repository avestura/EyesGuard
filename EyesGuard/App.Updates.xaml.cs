using EyesGuard.AppManagers;
using FormatWith;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyesGuard
{
    public partial class App
    {
        public static void UpdateLongTimeString()
        {
            if (NextLongBreak.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.LongBreak.Seconds.FormatWith(new
                    {
                        Seconds = (int)NextLongBreak.TotalSeconds
                    });
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.LongBreak.Minutes.FormatWith(new
                    {
                        Minutes = (int)NextLongBreak.TotalMinutes
                    });
            }
            UIViewModels.NotifyIcon.NextLongBreak =
                $"{NextLongBreak.Hours}:{NextLongBreak.Minutes}:{NextLongBreak.Seconds}";
        }

        public static void UpdateShortTimeString()
        {
            if (NextShortBreak.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.ShortBreak.Seconds.FormatWith(new
                    {
                        Seconds = (int)NextShortBreak.TotalSeconds
                    });
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.ShortBreak.Minutes.FormatWith(new
                    {
                        Minutes = (int)NextShortBreak.TotalMinutes
                    });
            }
            UIViewModels.NotifyIcon.NextShortBreak =
                $"{NextShortBreak.Hours}:{NextShortBreak.Minutes}:{NextShortBreak.Seconds}";
        }

        public static void UpdatePauseTimeString()
        {
            if (PauseProtectionSpan.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.PauseTime =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.PauseTime.Seconds.FormatWith(new
                    {
                        Seconds = (int)PauseProtectionSpan.TotalSeconds
                    });
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.PauseTime =
                    LocalizedEnvironment.Translation.EyesGuard.TimeRemaining.PauseTime.Minutes.FormatWith(new
                    {
                        Minutes = (int)PauseProtectionSpan.TotalMinutes
                    });
            }
            UIViewModels.NotifyIcon.PauseRemaining =
                $"{PauseProtectionSpan.Hours}:{PauseProtectionSpan.Minutes}:{PauseProtectionSpan.Seconds}";
        }

        public static void UpdateTimeHandlers()
        {
            if (Configuration.ProtectionState == GuardStates.Protecting)
            {
                if (!(Configuration.OnlyOneShortBreak && ShortBreakShownOnce))
                    ShortBreakHandler.Start();

                LongBreakHandler.Start();
            }
            else if (Configuration.ProtectionState == GuardStates.NotProtecting)
            {
                ShortBreakHandler.Stop();
                LongBreakHandler.Stop();
            }
        }

        public static void UpdateKeyTimeVisible()
        {
            if (Configuration.KeyTimesVisible)
            {
                UIViewModels.ShortLongBreakTimeRemaining.TimeRemainingVisibility = Visibility.Visible;
                UIViewModels.HeaderMenu.IsTimeItemChecked = true;
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.TimeRemainingVisibility = Visibility.Collapsed;
                UIViewModels.HeaderMenu.IsTimeItemChecked = false;
            }
        }

        public static void UpdateLongShortVisibility()
        {
            if (Configuration.ProtectionState == GuardStates.Protecting)
                UIViewModels.ShortLongBreakTimeRemaining.LongShortVisibility = Visibility.Visible;
            else
                UIViewModels.ShortLongBreakTimeRemaining.LongShortVisibility = Visibility.Collapsed;
        }

        public static void UpdateTaskbarIcon()
        {
            TaskbarIconManager.UpdateTaskbarIcon();
        }

        public static void UpdateIntruptOfStats(GuardStates state)
        {
            if (state == GuardStates.PausedProtecting)
            {
                Configuration.PauseCount++;
                Configuration.SaveSettingsToFile();
                UIViewModels.Stats.PauseCount = Configuration.PauseCount;
            }
            else if (state == GuardStates.NotProtecting)
            {
                Configuration.StopCount++;
                Configuration.SaveSettingsToFile();
                UIViewModels.Stats.StopCount = Configuration.StopCount;
            }
        }

        public static void UpdateStats()
        {
            Configuration.SaveSettingsToFile();
            UIViewModels.Stats.ShortCount = Configuration.ShortBreaksCompleted;
            UIViewModels.Stats.LongCompletedCount = Configuration.LongBreaksCompleted;
            UIViewModels.Stats.LongFailedCount = Configuration.LongBreaksFailed;
            UIViewModels.Stats.PauseCount = Configuration.PauseCount;
            UIViewModels.Stats.StopCount = Configuration.StopCount;
        }
    }
}
