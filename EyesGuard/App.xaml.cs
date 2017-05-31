using EyesGuard.Pages;
using EyesGuard.Resources.Menus;
using EyesGuard.ViewModels;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace EyesGuard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Application :: Fields :: Private Fields
        private static bool _isProtectionPaused = false;

        /// <summary>
        /// Used in <see cref="App.GetShortWindowMessage()"/> to get appropriate message from string resources
        /// </summary>
        private static int ShortMessageIteration = 0;

        /// <summary>
        /// Number of application process. Used to run only one instance
        /// </summary>
        private static int numberOfProgramInstances = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count();
        #endregion Application Fields :: Private Fields

        #region Application :: Fields :: Public Fields
        public static MainPage CurrentMainPage { get; set; }
        public static TaskbarIcon TaskbarIcon { get; set; }
        public static bool ShortBreakShownOnce = false;
        public static Config GlobalConfig { get; set; } = new Config();

        public static bool LaunchMinimized { get; set; } = false;
        public static bool IsProtectionPaused {
            get { return _isProtectionPaused; }
            set { _isProtectionPaused = value; ShortLongBreakTimeRemainingViewModel.IsProtectionPaused = value; }
        }

        public static TimeSpan PauseProtectionSpan { get; set; } = TimeSpan.FromSeconds(0);
        public static TimeSpan NextShortBreak { get; set; } = App.GlobalConfig.ShortBreakGap;
        public static TimeSpan NextLongBreak { get; set; } = App.GlobalConfig.LongBreakGap;
        public static TimeSpan ShortBreakVisibleTime { get; set; } = App.GlobalConfig.ShortBreakDuration;
        public static TimeSpan LongBreakVisibleTime { get; set; } = App.GlobalConfig.LongBreakDuration;

        public static DispatcherTimer ShortBreakHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer LongBreakHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer PauseHandler { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public static DispatcherTimer ShortDurationCounter { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
        public static DispatcherTimer LongDurationCounter { get; set; } = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        public static Window CurrentShortBreakWindow { get; set; } = null;
        public static Window CurrentLongBreakWindow { get; set; } = null;
        #endregion Application Fields :: Public Fields

        #region Application :: Fields :: Public Fields :: ViewModels

        public static ShortLongBreakTimeRemainingViewModel ShortLongBreakTimeRemainingViewModel { get; set; } = new ShortLongBreakTimeRemainingViewModel();
        public static HeaderMenuViewModel HeaderMenuViewModel { get; set; } = new HeaderMenuViewModel();
        public static NotifyIconViewModel NotifyIconViewModel { get; set; } = new NotifyIconViewModel();
        public static ShortBreakViewModel ShortBreakViewModel { get; set; } = new ShortBreakViewModel();
        public static LongBreakWindowViewModel LongBreakViewModel { get; set; } = new LongBreakWindowViewModel();
        public static StatsViewModel StatsViewModel { get; set; } = new StatsViewModel();

        #endregion

        #region Application :: Enums

        public enum GuardStates
        {
            PausedProtecting, Protecting, NotProtecting
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

            if (numberOfProgramInstances > 1)
            {
                MessageBox.Show("نرم افزار در حال اجراست. برای نمایش گزینه های نرم افزار روی آیکون سپر موجود در نوار وظیفه راست کلیک کنید");
                Shutdown();
            }
                
            // Changing UICulture to FARSI-IRAN , ba eftekhaar (:
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fa-IR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fa-IR");

            Config.LoadSettingsFromFile();

            // Ignore paused protecting state
            if (GlobalConfig.ProtectionState == GuardStates.PausedProtecting)
                GlobalConfig.ProtectionState = GuardStates.Protecting;


            if ((int)GlobalConfig.ShortBreakGap.TotalMinutes < 1)
                GlobalConfig.ShortBreakGap = new TimeSpan(0, 1, 0);

            if ((int)GlobalConfig.LongBreakGap.TotalMinutes < 5)
                GlobalConfig.LongBreakGap = new TimeSpan(0, 5, 0);

            if ((int)GlobalConfig.ShortBreakDuration.TotalSeconds < 2)
                GlobalConfig.ShortBreakDuration = new TimeSpan(0, 0, 2);

            if ((int)GlobalConfig.LongBreakDuration.TotalSeconds < 5)
                GlobalConfig.LongBreakDuration = new TimeSpan(0, 0, 5);

            GlobalConfig.SaveSettingsToFile();
            
            NextShortBreak  = App.GlobalConfig.ShortBreakGap;
            NextLongBreak  = App.GlobalConfig.LongBreakGap;
            ShortBreakVisibleTime  = App.GlobalConfig.ShortBreakDuration;
            LongBreakVisibleTime  = App.GlobalConfig.LongBreakDuration;


            if(App.GlobalConfig.ProtectionState == GuardStates.Protecting)
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

            TaskbarIcon = (TaskbarIcon)App.Current.FindResource("App.GlobalTaskbarIcon");
            TaskbarIcon.DataContext = NotifyIconViewModel;

            UpdateTaskbarIcon();

        }

        #endregion

        #region Application :: Timing and Control :: Common

        /// <summary>
        /// This method prevents user to change protection status in resting mode
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfResting()
        {
            if (App.CurrentShortBreakWindow != null ||
                App.CurrentLongBreakWindow != null)
            {
                App.ShowWarning("لطفا تا پایان استراحت صبر کنید", WarningPage.PageStates.Warning);
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

        private async void ShortBreakHandler_Tick(object sender, EventArgs e)
        {
            if (!IsProtectionPaused)
            {
                NextShortBreak = NextShortBreak.Subtract(TimeSpan.FromSeconds(1));
                UpdateShortTimeString();

                if ((int)NextShortBreak.TotalSeconds == 0)
                {
                    ShortLongBreakTimeRemainingViewModel.NextShortBreak = App.Current.FindResource("Strings.EyesGuard.Resting").ToString();

                    NextShortBreak = App.GlobalConfig.ShortBreakGap;
                    ShortBreakShownOnce = true;
                    var shortWindow = new ShortBreakWindow()
                    {
                        DataContext = ShortBreakViewModel
                    };
                    ShortBreakVisibleTime = App.GlobalConfig.ShortBreakDuration;
                    ShortBreakViewModel.TimeRemaining = ((int)ShortBreakVisibleTime.TotalSeconds).ToString();
                    ShortBreakViewModel.ShortMessage = GetShortWindowMessage();
                    try
                    {
                        await shortWindow.ShowUsingLinearAnimationAsync();
                        shortWindow.Show();
                        shortWindow.BringIntoView();
                        shortWindow.Focus();
                    }
                    catch { }
          
                    ShortDurationCounter.Start();

                    ShortBreakHandler.Stop();
                    LongBreakHandler.Stop();
                }
            }

        }

        private async void LongBreakHandler_Tick(object sender, EventArgs e)
        {
            if (!IsProtectionPaused)
            {
                NextLongBreak = NextLongBreak.Subtract(TimeSpan.FromSeconds(1));
                UpdateLongTimeString();

                if ((int)NextLongBreak.TotalSeconds == 0)
                {

                    ShortLongBreakTimeRemainingViewModel.NextLongBreak = App.Current.FindResource("Strings.EyesGuard.Resting").ToString();

                    NextShortBreak = App.GlobalConfig.ShortBreakGap;
                    NextLongBreak = App.GlobalConfig.LongBreakGap;

                    var longWindow = new LongBreakWindow()
                    {
                        DataContext = LongBreakViewModel
                    };
                    LongBreakVisibleTime = App.GlobalConfig.LongBreakDuration;
                    LongBreakViewModel.TimeRemaining = LongBreakVisibleTime.Hours + " ساعت و " + LongBreakVisibleTime.Minutes + " دقیقه و " + LongBreakVisibleTime.Seconds + " ثانیه تا پایان استراحت بلند";
                    LongBreakViewModel.CanCancel = (GlobalConfig.ForceUserToBreak) ? Visibility.Collapsed : Visibility.Visible;

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

                    ShortBreakHandler.Stop();
                    LongBreakHandler.Stop();
                }
            }

      
        }

        #endregion

        #region Application :: Timing and Control :: During Rest

        private async void ShortDurationCounter_Tick(object sender, EventArgs e)
        {
            ShortBreakVisibleTime = ShortBreakVisibleTime.Subtract(TimeSpan.FromSeconds(1));
            ShortBreakViewModel.TimeRemaining = ((int)ShortBreakVisibleTime.TotalSeconds).ToString();
            if ((int)ShortBreakVisibleTime.TotalSeconds == 0)
            {
                if (GlobalConfig.SaveStats)
                {
                    GlobalConfig.ShortBreaksCompleted++;
                    UpdateStats();
                }
      
                ShortLongBreakTimeRemainingViewModel.NextShortBreak = App.Current.FindResource("Strings.EyesGuard.Waiting").ToString();

                await CurrentShortBreakWindow.HideUsingLinearAnimationAsync();
                if(CurrentShortBreakWindow != null)
                {
                    ((ShortBreakWindow)CurrentShortBreakWindow).LetItClose = true;
                    CurrentShortBreakWindow.Close();
                    CurrentShortBreakWindow = null;
                }
                if (!App.GlobalConfig.OnlyOneShortBreak && GlobalConfig.ProtectionState == GuardStates.Protecting)
                {
                    ShortBreakHandler.Start();
                
                }
                LongBreakHandler.Start();
                ShortDurationCounter.Stop();
            }
        }

        private async void LongDurationCounter_Tick(object sender, EventArgs e)
        {
            LongBreakVisibleTime = LongBreakVisibleTime.Subtract(TimeSpan.FromSeconds(1));
            LongBreakViewModel.TimeRemaining = LongBreakVisibleTime.Hours + " ساعت و " + LongBreakVisibleTime.Minutes + " دقیقه و " + LongBreakVisibleTime.Seconds + " ثانیه تا پایان استراحت بلند";
            if ((int)LongBreakVisibleTime.TotalSeconds == 0)
            {
                ((LongBreakWindow)CurrentLongBreakWindow).LetItClose = true;
                if (GlobalConfig.SaveStats)
                {
                    GlobalConfig.LongBreaksCompleted++;
                    UpdateStats();
                }
                await CurrentLongBreakWindow.HideUsingLinearAnimationAsync();
                CurrentLongBreakWindow.Close();
                CurrentLongBreakWindow = null;
                ShortBreakShownOnce = false;
                if(GlobalConfig.ProtectionState == GuardStates.Protecting)
                {
                    ShortBreakHandler.Start();
                    LongBreakHandler.Start();
                }
                LongDurationCounter.Stop();
            }
        }

        #endregion

        #region Application :: Updates

        public static void UpdateLongTimeString()
        {
            if (NextLongBreak.TotalSeconds < 60)
            {
                ShortLongBreakTimeRemainingViewModel.NextLongBreak = (int)NextLongBreak.TotalSeconds + " " + App.Current.FindResource("Strings.EyesGuard.TimeRemaining.LongBreak.Seconds").ToString();
            }
            else
            {
                ShortLongBreakTimeRemainingViewModel.NextLongBreak = (int)NextLongBreak.TotalMinutes + " " + App.Current.FindResource("Strings.EyesGuard.TimeRemaining.LongBreak.Minutes").ToString();

            }
        }

        public static void UpdateShortTimeString()
        {
            if (NextShortBreak.TotalSeconds < 60)
            {
                ShortLongBreakTimeRemainingViewModel.NextShortBreak = (int)NextShortBreak.TotalSeconds + " " + App.Current.FindResource("Strings.EyesGuard.TimeRemaining.ShortBreak.Seconds");
            }
            else
            {
                ShortLongBreakTimeRemainingViewModel.NextShortBreak = (int)NextShortBreak.TotalMinutes + " " + App.Current.FindResource("Strings.EyesGuard.TimeRemaining.ShortBreak.Minutes");

            }
        }

        public static void UpdatePauseTimeString()
        {
            if (PauseProtectionSpan.TotalSeconds < 60)
            {
                ShortLongBreakTimeRemainingViewModel.PauseTime = string.Format(App.Current.FindResource("Strings.EyesGuard.TimeRemaining.PauseTime.Seconds").ToString(), (int)PauseProtectionSpan.TotalSeconds);
            }
            else
            {
                ShortLongBreakTimeRemainingViewModel.PauseTime = string.Format(App.Current.FindResource("Strings.EyesGuard.TimeRemaining.PauseTime.Minutes").ToString(), (int)PauseProtectionSpan.TotalMinutes);

            }
        }

        public static void UpdateTimeHandlers()
        {
            if (App.GlobalConfig.ProtectionState == GuardStates.Protecting)
            {
                if(!(GlobalConfig.OnlyOneShortBreak && ShortBreakShownOnce))
                    ShortBreakHandler.Start();

                LongBreakHandler.Start();
            }
            else if (App.GlobalConfig.ProtectionState == GuardStates.NotProtecting)
            {
                ShortBreakHandler.Stop();
                LongBreakHandler.Stop();
            }
        }

        public static void UpdateKeyTimeVisible()
        {
            if (GlobalConfig.KeyTimesVisible)
            {
                ShortLongBreakTimeRemainingViewModel.TimeRemainingVisibility = Visibility.Visible;
                HeaderMenuViewModel.IsTimeItemChecked = true;
                
            }
            else
            {
                ShortLongBreakTimeRemainingViewModel.TimeRemainingVisibility = Visibility.Collapsed;
                HeaderMenuViewModel.IsTimeItemChecked = false;
            }
        }

        public static void UpdateLongShortVisibility()
        {
            if (GlobalConfig.ProtectionState == GuardStates.Protecting)
                ShortLongBreakTimeRemainingViewModel.LongShortVisibility = Visibility.Visible;
            else
                ShortLongBreakTimeRemainingViewModel.LongShortVisibility = Visibility.Collapsed;
        }

        public static void UpdateTaskbarIcon()
        {
            if (GlobalConfig.ProtectionState == GuardStates.Protecting)
            {
                NotifyIconViewModel.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFBEFFD8")));
                NotifyIconViewModel.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FF12B754")));
                NotifyIconViewModel.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Sheild-Protecting.ico", UriKind.Absolute));
                NotifyIconViewModel.StartProtectVisibility = Visibility.Collapsed;
                NotifyIconViewModel.StopProtectVisibility = Visibility.Visible;
                NotifyIconViewModel.Title = App.Current.FindResource("Strings.EyesGuard.TaskbarIcon.Protected").ToString();
            }
            else if (GlobalConfig.ProtectionState == GuardStates.PausedProtecting)
            {
                NotifyIconViewModel.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFFFDBF")));
                NotifyIconViewModel.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFD6C90D")));
                NotifyIconViewModel.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Sheild-Paused.ico", UriKind.Absolute));
                NotifyIconViewModel.StartProtectVisibility = Visibility.Visible;
                NotifyIconViewModel.StopProtectVisibility = Visibility.Collapsed;
                NotifyIconViewModel.Title = App.Current.FindResource("Strings.EyesGuard.TaskbarIcon.PausedProtected").ToString();

            }
            else if (GlobalConfig.ProtectionState == GuardStates.NotProtecting)
            {
                NotifyIconViewModel.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFFC0BE")));
                NotifyIconViewModel.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFF322A")));
                NotifyIconViewModel.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Shield-Stopped.ico", UriKind.Absolute));
                NotifyIconViewModel.StartProtectVisibility = Visibility.Visible;
                NotifyIconViewModel.StopProtectVisibility = Visibility.Collapsed;
                NotifyIconViewModel.Title = App.Current.FindResource("Strings.EyesGuard.TaskbarIcon.NotProtected").ToString();

            }
        }

        public static void UpdateIntruptOfStats(GuardStates state)
        {
            if (state == GuardStates.PausedProtecting)
            {
                App.GlobalConfig.PauseCount++;
                App.GlobalConfig.SaveSettingsToFile();
                StatsViewModel.PauseCount = App.GlobalConfig.PauseCount;
            }
            else if (state == GuardStates.NotProtecting)
            {
                App.GlobalConfig.StopCount++;
                App.GlobalConfig.SaveSettingsToFile();
                StatsViewModel.StopCount = App.GlobalConfig.StopCount;
            }
        }

        public static void UpdateStats()
        {
            App.GlobalConfig.SaveSettingsToFile();
            StatsViewModel.ShortCount = App.GlobalConfig.ShortBreaksCompleted;
            StatsViewModel.LongCompletedCount = App.GlobalConfig.LongBreaksCompleted;
            StatsViewModel.LongFailedCount = App.GlobalConfig.LongBreaksFailed;
            StatsViewModel.PauseCount = App.GlobalConfig.PauseCount;
            StatsViewModel.StopCount = App.GlobalConfig.StopCount;

        }

        #endregion

        #region Application :: Protection Action

        public static void PauseProtection(TimeSpan pauseDuration)
        {
            if (App.CheckIfResting()) return;

            if (App.GlobalConfig.SaveStats)
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
            return App.Current.FindResource($"Strings.EyesGuard.ShortWindow.Message{ShortMessageIteration}").ToString();
        }

        #endregion

        #region Application :: Utilities

        public static void SetStartUp(bool enable)
        {
            try
            {

                string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
                string appPath = $"\"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\EyesGuard.exe\" /auto";
                string appName = "EyesGuard";

                Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);

                if (enable)
                {
                    if (startupKey.GetValue(appName) == null)
                    {
                        startupKey.Close();
                        startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                        // Add startup reg key
                        startupKey.SetValue(appName, appPath);
                        startupKey.Close();
                    }
                }
                else
                {
                    // remove startup
                    startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                    startupKey.DeleteValue(appName, false);
                    startupKey.Close();
                }
        }
            catch (Exception e) {
         
                    MessageBox.Show(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), e.Message.ToString());
 
            }
        }

        #endregion

        #region Application :: Chrome Actions

        private static bool _hiding = false;
        public async static void Hide()
        {
            if (!_hiding)
            {
                _hiding = true;
                await App.GetMainWindow().HideUsingLinearAnimationAsync();
                App.GetMainWindow().Hide();
                _hiding = false;
            }
        }

        private static bool _closing = false;
        public async static void Close()
        {
            if (!_closing)
            {
                _closing = true;
                await App.GetMainWindow().HideUsingLinearAnimationAsync(200);
                App.GetMainWindow().Close();
                _closing = false;
            }
        }

        private static bool _showing = false;
        public async static void Show()
        {
            if (!_showing)
            {
                _showing = true;
                App.GetMainWindow().Show();
                await App.GetMainWindow().ShowUsingLinearAnimationAsync();
                App.GetMainWindow().BringIntoView();
                _showing = false;
            }
        }

        #endregion








    }
}
