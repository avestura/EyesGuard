using EyesGuard.AppManagers;
using EyesGuard.Configurations;
using EyesGuard.Views.Pages;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using static EyesGuard.Data.LanguageLoader;

namespace EyesGuard
{
    public partial class App
    {
        public static double SystemDpiFactor { get; set; }

        public static IdleDetector SystemIdleDetector { get; set; }

        public static MainPage CurrentMainPage { get; set; }

        public static TaskbarIcon TaskbarIcon { get; set; }

        public static bool ShortBreakShownOnce = false;

        public static Configuration Configuration { get; set; } = new Configuration();

        public static LocalizedEnvironment LocalizedEnvironment { get; set; }

        public static CultureInfo CurrentLocale { get; private set; }

        public static bool LaunchMinimized { get; set; } = true;

        public static bool IsProtectionPaused
        {
            get { return _isProtectionPaused; }
            set
            {
                _isProtectionPaused = value;
                UIViewModels
                    .ShortLongBreakTimeRemaining
                    .IsProtectionPaused = value;
                UIViewModels.NotifyIcon.PausedVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                SystemIdleDetector.EnableRaisingEvents = !value;
            }
        }

        public static bool AppIsInIdleState =>
            Configuration.SystemIdleDetectionEnabled
            && CurrentMainPage?.ProtectionState == GuardStates.Protecting
            && SystemIdleDetector.IsSystemIdle();

        public static bool TimersAreEligibleToCountdown => !(IsProtectionPaused || AppIsInIdleState);

        public static TimeSpan PauseProtectionSpan { get; set; } = TimeSpan.FromSeconds(0);
        public static TimeSpan NextShortBreak { get; set; } = App.Configuration.ShortBreakGap;
        public static TimeSpan NextLongBreak { get; set; } = App.Configuration.LongBreakGap;
        public static TimeSpan ShortBreakVisibleTime { get; set; } = App.Configuration.ShortBreakDuration;
        public static TimeSpan LongBreakVisibleTime { get; set; } = App.Configuration.LongBreakDuration;

        public static DispatcherTimer ShortBreakHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer LongBreakHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer PauseHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public static DispatcherTimer ShortDurationCounter { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer LongDurationCounter { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public static Window CurrentShortBreakWindow { get; set; } = null;
        public static Window CurrentLongBreakWindow { get; set; } = null;

        public static UIViewModels UIViewModels { get; } = new UIViewModels();

        public bool BasePrequirementsLoaded { get; private set; } = false;
    }
}
