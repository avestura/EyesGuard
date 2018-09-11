using EyesGuard.AppManagers;
using EyesGuard.Configurations;
using EyesGuard.Extensions;
using EyesGuard.Pages;
using EyesGuard.Resources.Menus;
using EyesGuard.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace EyesGuard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Application :: Fields :: Private Fields
        private static bool _isProtectionPaused = false;
        private const int EyesGuardIdleDetectionThreshold = 80;

        /// <summary>
        /// Used in <see cref="App.GetShortWindowMessage()"/> to get appropriate message from string resources
        /// </summary>
        private static int ShortMessageIteration = 0;

        /// <summary>
        /// Number of application process. Used to run only one instance
        /// </summary>
        private static int programInstancesCount =
            Process.GetProcessesByName(
                System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)
                ).Length;

        #endregion Application Fields :: Private Fields

        #region Application :: Fields :: Public Fields
        public static double SystemDpiFactor { get; set; }
        public static ScalingType UserScalingType {
            get
            {
                // Section :: Scaling Init
                if (SystemDpiFactor >= 1 && SystemDpiFactor <= 2.5 && SystemDpiFactor % 0.25 == 0)
                    return Configuration.DpiScalingType;
                else
                    return ScalingType.UseWindowsDPIScaling;
            }

        }
        public static ScalingSize UserScalingFactor { get; set; }

        public static IdleDetector SystemIdleDetector { get; set; }

        public static MainPage CurrentMainPage { get; set; }
        public static TaskbarIcon TaskbarIcon { get; set; }
        public static bool ShortBreakShownOnce = false;
        public static Configuration Configuration { get; set; } = new Configuration();

        public static bool LaunchMinimized { get; set; } = true;

        public static bool IsProtectionPaused {
            get { return _isProtectionPaused; }
            set {
                _isProtectionPaused = value;
                UIViewModels.ShortLongBreakTimeRemaining.IsProtectionPaused = value;
                UIViewModels.NotifyIcon.PausedVisibility = (value) ? Visibility.Visible : Visibility.Collapsed;
                SystemIdleDetector.EnableRaisingEvents = !value;
            }
        }

        public static bool AppIsInIdleState =>
            Configuration.SystemIdleDetectionEnabled
            && CurrentMainPage.ProtectionState == GuardStates.Protecting
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
        #endregion Application Fields :: Public Fields

        #region Application :: Fields :: Public Fields :: ViewModels
        public static UIViewModels UIViewModels { get; } = new UIViewModels();
        #endregion

        #region Application :: Enums

        public enum GuardStates
        {
            PausedProtecting, Protecting, NotProtecting
        }
        public enum ScalingType
        {
            UseWindowsDPIScaling, UseCutomScaling
        }
        public enum ScalingSize
        {
            X100, X125, X150, X175, X200, Unset
        }

        #endregion Application Enums

        #region Application :: Sensitive Get

        public static App GetApp()
        {
            return ((App)Current);
        }

        public static MainWindow GetMainWindow()
        {
            return ((MainWindow)App.Current.MainWindow);
        }
        #endregion

        #region Application :: Initialization

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Check if application is running by startup
            if (e.Args.Length > 0 && e.Args[0] == "/auto")
            {
                LaunchMinimized = true;
            }

            if (programInstancesCount > 1)
            {
                MessageBox.Show("Strings.EyesGuard.Alerts.DoNotRunMultipleInstances".Translate());
                Shutdown();
            }

            Configuration.InitializeLocalFolder();
            Configuration.LoadSettingsFromFile();

            InitializeIdleDetector(Configuration.SystemIdleDetectionEnabled);

            ToolTipService.ShowDurationProperty.OverrideMetadata(
                typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));

            UserScalingFactor = Configuration.DpiScalingFactor;
            // End Section

            // Ignore paused protecting state
            if (Configuration.ProtectionState == GuardStates.PausedProtecting)
                Configuration.ProtectionState = GuardStates.Protecting;

            if ((int)Configuration.ShortBreakGap.TotalMinutes < 1)
                Configuration.ShortBreakGap = new TimeSpan(0, 1, 0);

            if ((int)Configuration.LongBreakGap.TotalMinutes < 5)
                Configuration.LongBreakGap = new TimeSpan(0, 5, 0);

            if ((int)Configuration.ShortBreakDuration.TotalSeconds < 2)
                Configuration.ShortBreakDuration = new TimeSpan(0, 0, 2);

            if ((int)Configuration.LongBreakDuration.TotalSeconds < 5)
                Configuration.LongBreakDuration = new TimeSpan(0, 0, 5);

            Configuration.SaveSettingsToFile();

            NextShortBreak  = App.Configuration.ShortBreakGap;
            NextLongBreak  = App.Configuration.LongBreakGap;
            ShortBreakVisibleTime  = App.Configuration.ShortBreakDuration;
            LongBreakVisibleTime  = App.Configuration.LongBreakDuration;

            if(App.Configuration.ProtectionState == GuardStates.Protecting)
            {
                ShortBreakHandler.Start();
                LongBreakHandler.Start();
            }

            UpdateShortTimeString();
            UpdateLongTimeString();
            UpdateKeyTimeVisible();
            UpdateStats();

            ShortBreakHandler.Tick += ShortBreakHandler_Tick;
            LongBreakHandler .Tick += LongBreakHandler_Tick;
            PauseHandler     .Tick += PauseHandler_Tick;

            ShortDurationCounter.Tick += ShortDurationCounter_Tick;
            LongDurationCounter.Tick += LongDurationCounter_Tick;

            TaskbarIcon = "App.GlobalTaskbarIcon".Translate<TaskbarIcon>();
            TaskbarIcon.DataContext = UIViewModels.NotifyIcon;

            UpdateTaskbarIcon();

            if (App.TaskbarIcon != null && !App.Configuration.TrayNotificationSaidBefore)
            {
                App.TaskbarIcon.ShowBalloonTip(
                    "Strings.EyesGuard.Alerts.RunBackground.Title".Translate(),
                    "Strings.EyesGuard.Alerts.RunBackground.Message".Translate(),
                    Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);

                App.Configuration.TrayNotificationSaidBefore = true;
                App.Configuration.SaveSettingsToFile();
            }

        }

        private void InitializeIdleDetector(bool initialStart)
        {
            try
            {
                SystemIdleDetector = new IdleDetector()
                {
                    IdleThreshold = EyesGuardIdleDetectionThreshold,
                    DeferUpdate = false,
                    EnableRaisingEvents = initialStart
                };
                SystemIdleDetector.IdleStateChanged += SystemIdleDetector_IdleStateChanged;

                if(initialStart && SystemIdleDetector.State == IdleDetectorState.Stopped)
                    _ = SystemIdleDetector.RequestStart();
            }
            catch { }
        }

        private void SystemIdleDetector_IdleStateChanged(object sender, IdleStateChangedEventArgs e)
        {
            UpdateIdleActions();
        }

        public static void UpdateIdleActions()
        {
            if (App.CheckIfResting(showWarning: false)) return;

            UIViewModels.ShortLongBreakTimeRemaining.IdleVisibility =
                (AppIsInIdleState) ? Visibility.Visible : Visibility.Collapsed;

        }

        #endregion

        #region Application :: Timing and Control :: Common

        /// <summary>
        /// This method prevents user to change protection status in resting mode
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfResting(bool showWarning = true)
        {
            if (App.CurrentShortBreakWindow != null
                || App.CurrentLongBreakWindow != null)
            {
                if(showWarning)
                    App.ShowWarning("Strings.EyesGuard.WaitUnitlEndOfBreak".Translate(), WarningPage.PageStates.Warning);
                return true;
            }
            return false;
        }

        #endregion

        #region Application :: Timing and Control :: Handlers

        private void PauseHandler_Tick(object sender, EventArgs e)
        {
            PauseProtectionSpan = PauseProtectionSpan.Subtract(TimeSpan.FromSeconds(1));
            UpdatePauseTimeString();

            if ((int)PauseProtectionSpan.TotalMilliseconds == 0)
            {
                ResumeProtection();
            }
        }

        private void ShortBreakHandler_Tick(object sender, EventArgs e)
        {
            if (TimersAreEligibleToCountdown)
            {
                NextShortBreak = NextShortBreak.Subtract(TimeSpan.FromSeconds(1));
                UpdateShortTimeString();

                if ((int)NextShortBreak.TotalSeconds == 0)
                {
                    StartShortBreak();
                }
            }

        }

        public async void StartShortBreak()
        {
            ShortBreakHandler.Stop();
            LongBreakHandler.Stop();

            UIViewModels.HeaderMenu.ManualBreakEnabled = false;
            UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak = "Strings.EyesGuard.Resting".Translate();
            UIViewModels.NotifyIcon.NextShortBreak = "Strings.EyesGuard.Resting".Translate();

            NextShortBreak = App.Configuration.ShortBreakGap;
            ShortBreakShownOnce = true;
            var shortWindow = new ShortBreakWindow()
            {
                DataContext = UIViewModels.ShortBreak
            };
            ShortBreakVisibleTime = App.Configuration.ShortBreakDuration;
            UIViewModels.ShortBreak.TimeRemaining = ((int)ShortBreakVisibleTime.TotalSeconds).ToString();
            UIViewModels.ShortBreak.ShortMessage = GetShortWindowMessage();
            try
            {
                await shortWindow.ShowUsingLinearAnimationAsync();
                shortWindow.Show();
                shortWindow.BringIntoView();
                shortWindow.Focus();
            }
            catch { }

            ShortDurationCounter.Start();

        }

        private void LongBreakHandler_Tick(object sender, EventArgs e)
        {
            if (TimersAreEligibleToCountdown)
            {
                NextLongBreak = NextLongBreak.Subtract(TimeSpan.FromSeconds(1));
                UpdateLongTimeString();

                if(App.Configuration.AlertBeforeLongBreak && (int)NextLongBreak.TotalSeconds == 60)
                {
                    App.TaskbarIcon.ShowBalloonTip(
                        "Strings.EyesGuard.LongBreakAlert.Title".Translate(),
                        "Strings.EyesGuard.LongBreakAlert.Message".Translate(),
                        BalloonIcon.Info);
                }

                if ((int)NextLongBreak.TotalSeconds == 0)
                {

                    StartLongBreak();
                }
            }

        }

        public async void StartLongBreak()
        {

            ShortBreakHandler.Stop();
            LongBreakHandler.Stop();
            UIViewModels.HeaderMenu.ManualBreakEnabled = false;
            UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak = "Strings.EyesGuard.Resting".Translate();
            UIViewModels.NotifyIcon.NextLongBreak = "Strings.EyesGuard.Resting".Translate();

            NextShortBreak = App.Configuration.ShortBreakGap;
            NextLongBreak = App.Configuration.LongBreakGap;

            var longWindow = new LongBreakWindow()
            {
                DataContext = UIViewModels.LongBreak
            };
            LongBreakVisibleTime = App.Configuration.LongBreakDuration;
            UIViewModels.LongBreak.TimeRemaining = string.Format(
                "Strings.EyesGuard.LongBreakTimeRemaining".Translate(),
                LongBreakVisibleTime.Hours,
                LongBreakVisibleTime.Minutes,
                LongBreakVisibleTime.Seconds);

            UIViewModels.LongBreak.CanCancel = (Configuration.ForceUserToBreak) ? Visibility.Collapsed : Visibility.Visible;

            if (CurrentShortBreakWindow != null)
            {
                ((ShortBreakWindow)CurrentShortBreakWindow).LetItClose = true;
                CurrentShortBreakWindow.Close();
                CurrentShortBreakWindow = null;
            }
            ShortDurationCounter.Stop();
            await longWindow.ShowUsingLinearAnimationAsync();
            longWindow.Show();
            longWindow.BringIntoView();
            longWindow.Focus();

            LongDurationCounter.Start();

        }

        #endregion

        #region Application :: Timing and Control :: During Rest

        private void ShortDurationCounter_Tick(object sender, EventArgs e)
        {
            ShortBreakVisibleTime = ShortBreakVisibleTime.Subtract(TimeSpan.FromSeconds(1));
            UIViewModels.ShortBreak.TimeRemaining = ((int)ShortBreakVisibleTime.TotalSeconds).ToString();
            if ((int)ShortBreakVisibleTime.TotalSeconds == 0)
            {
                EndShortBreak();
            }
        }

        private async void EndShortBreak()
        {

            if (Configuration.SaveStats)
            {
                Configuration.ShortBreaksCompleted++;
                UpdateStats();
            }

            UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak = "Strings.EyesGuard.Waiting".Translate();
            UIViewModels.NotifyIcon.NextShortBreak = "Strings.EyesGuard.Waiting".Translate();

            await CurrentShortBreakWindow.HideUsingLinearAnimationAsync();
            if (CurrentShortBreakWindow != null)
            {
                ((ShortBreakWindow)CurrentShortBreakWindow).LetItClose = true;
                CurrentShortBreakWindow.Close();
                CurrentShortBreakWindow = null;
            }
            if (!App.Configuration.OnlyOneShortBreak && Configuration.ProtectionState == GuardStates.Protecting)
            {
                ShortBreakHandler.Start();

            }
            LongBreakHandler.Start();
            ShortDurationCounter.Stop();

            UIViewModels.HeaderMenu.ManualBreakEnabled = true;
        }

        private async void LongDurationCounter_Tick(object sender, EventArgs e)
        {
            LongBreakVisibleTime = LongBreakVisibleTime.Subtract(TimeSpan.FromSeconds(1));
            UIViewModels.LongBreak.TimeRemaining = string.Format(
                         "Strings.EyesGuard.LongBreakTimeRemaining".Translate(),
                         LongBreakVisibleTime.Hours,
                         LongBreakVisibleTime.Minutes,
                         LongBreakVisibleTime.Seconds);

            if ((int)LongBreakVisibleTime.TotalSeconds == 0)
            {
                await EndLongBreak();
            }
        }

        private async Task EndLongBreak()
        {

            ((LongBreakWindow)CurrentLongBreakWindow).LetItClose = true;
            if (Configuration.SaveStats)
            {
                Configuration.LongBreaksCompleted++;
                UpdateStats();
            }
            await CurrentLongBreakWindow.HideUsingLinearAnimationAsync();
            CurrentLongBreakWindow.Close();
            CurrentLongBreakWindow = null;
            ShortBreakShownOnce = false;
            if (Configuration.ProtectionState == GuardStates.Protecting)
            {
                ShortBreakHandler.Start();
                LongBreakHandler.Start();
            }
            LongDurationCounter.Stop();

            UIViewModels.HeaderMenu.ManualBreakEnabled = true;
        }

        #endregion

        #region Application :: Updates

        public static void UpdateLongTimeString()
        {
            if (NextLongBreak.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak =
                    $"{(int)NextLongBreak.TotalSeconds} {"Strings.EyesGuard.TimeRemaining.LongBreak.Seconds".Translate()}";
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak =
                    $"{(int)NextLongBreak.TotalMinutes} {"Strings.EyesGuard.TimeRemaining.LongBreak.Minutes".Translate()}";

            }
            UIViewModels.NotifyIcon.NextLongBreak =
                $"{NextLongBreak.Hours}:{NextLongBreak.Minutes}:{NextLongBreak.Seconds}";
        }

        public static void UpdateShortTimeString()
        {
            if (NextShortBreak.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak =
                    $"{(int)NextShortBreak.TotalSeconds} {"Strings.EyesGuard.TimeRemaining.ShortBreak.Seconds".Translate()}";
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak =
                    $"{(int)NextShortBreak.TotalMinutes} {"Strings.EyesGuard.TimeRemaining.ShortBreak.Minutes".Translate()}";

            }
            UIViewModels.NotifyIcon.NextShortBreak =
                $"{NextShortBreak.Hours}:{NextShortBreak.Minutes}:{NextShortBreak.Seconds}";
        }

        public static void UpdatePauseTimeString()
        {
            if (PauseProtectionSpan.TotalSeconds < 60)
            {
                UIViewModels.ShortLongBreakTimeRemaining.PauseTime =
                    string.Format("Strings.EyesGuard.TimeRemaining.PauseTime.Seconds".Translate(),
                    (int)PauseProtectionSpan.TotalSeconds);
            }
            else
            {
                UIViewModels.ShortLongBreakTimeRemaining.PauseTime =
                    string.Format("Strings.EyesGuard.TimeRemaining.PauseTime.Minutes".Translate(),
                    (int)PauseProtectionSpan.TotalMinutes);

            }
            UIViewModels.NotifyIcon.PauseRemaining =
                $"{PauseProtectionSpan.Hours}:{PauseProtectionSpan.Minutes}:{PauseProtectionSpan.Seconds}";
        }

        public static void UpdateTimeHandlers()
        {
            if (App.Configuration.ProtectionState == GuardStates.Protecting)
            {
                if(!(Configuration.OnlyOneShortBreak && ShortBreakShownOnce))
                    ShortBreakHandler.Start();

                LongBreakHandler.Start();
            }
            else if (App.Configuration.ProtectionState == GuardStates.NotProtecting)
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
                App.Configuration.PauseCount++;
                App.Configuration.SaveSettingsToFile();
                UIViewModels.Stats.PauseCount = App.Configuration.PauseCount;
            }
            else if (state == GuardStates.NotProtecting)
            {
                App.Configuration.StopCount++;
                App.Configuration.SaveSettingsToFile();
                UIViewModels.Stats.StopCount = App.Configuration.StopCount;
            }
        }

        public static void UpdateStats()
        {
            App.Configuration.SaveSettingsToFile();
            UIViewModels.Stats.ShortCount = App.Configuration.ShortBreaksCompleted;
            UIViewModels.Stats.LongCompletedCount = App.Configuration.LongBreaksCompleted;
            UIViewModels.Stats.LongFailedCount = App.Configuration.LongBreaksFailed;
            UIViewModels.Stats.PauseCount = App.Configuration.PauseCount;
            UIViewModels.Stats.StopCount = App.Configuration.StopCount;

        }

        #endregion

        #region Application :: Protection Action

        public static void PauseProtection(TimeSpan pauseDuration)
        {
            if (App.CheckIfResting()) return;

            if (App.Configuration.SaveStats)
            {
                App.UpdateIntruptOfStats(App.GuardStates.PausedProtecting);
            }
            PauseProtectionSpan = pauseDuration;
            UpdatePauseTimeString();
            IsProtectionPaused = true;
            PauseHandler.Start();

            CurrentMainPage.ProtectionState = GuardStates.PausedProtecting;
        }

        public static void ResumeProtection()
        {
            PauseProtectionSpan = TimeSpan.Zero;
            IsProtectionPaused = false;
            PauseHandler.Stop();
            CurrentMainPage.ProtectionState = GuardStates.Protecting;
        }

        #endregion

        #region Application :: UI Message

        public static void ShowWarning(string message,  WarningPage.PageStates state = WarningPage.PageStates.Warning, Page navPage = null, string pageTitle = "")
        {
            try
            {
                navPage = navPage ?? (Page)GetMainWindow().MainFrame.Content;
                GetMainWindow().MainFrame.Navigate(new WarningPage(navPage, message, state, pageTitle));
            }
            catch { }
        }

        public static string GetShortWindowMessage()
        {
            ShortMessageIteration++;
            ShortMessageIteration = ShortMessageIteration % 5;
            return $"Strings.EyesGuard.ShortWindow.Message{ShortMessageIteration}".Translate();
        }

        #endregion

        #region Application :: Utilities

        #endregion

    }
}
