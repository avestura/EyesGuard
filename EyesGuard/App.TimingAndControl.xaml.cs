using EyesGuard.Animations;
using EyesGuard.Pages;
using FormatWith;
using Hardcodet.Wpf.TaskbarNotification;
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
        #region Timing and Control :: Common

        /// <summary>
        /// This method prevents user to change protection status in resting mode
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfResting(bool showWarning = true)
        {
            if (App.CurrentShortBreakWindow != null
                || App.CurrentLongBreakWindow != null)
            {
                if (showWarning)
                    App.ShowWarning(App.LocalizedEnvironment.Translation.EyesGuard.WaitUnitlEndOfBreak, WarningPage.PageStates.Warning);
                return true;
            }
            return false;
        }

        #endregion

        #region Timing and Control :: Handlers

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
            UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak = App.LocalizedEnvironment.Translation.EyesGuard.Resting;
            UIViewModels.NotifyIcon.NextShortBreak = LocalizedEnvironment.Translation.EyesGuard.Resting;

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

                if (App.Configuration.AlertBeforeLongBreak && (int)NextLongBreak.TotalSeconds == 60)
                {
                    App.TaskbarIcon.ShowBalloonTip(
                        LocalizedEnvironment.Translation.EyesGuard.Notifications.LongBreakAlert.Title,
                        LocalizedEnvironment.Translation.EyesGuard.Notifications.LongBreakAlert.Message,
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
            UIViewModels.ShortLongBreakTimeRemaining.NextLongBreak = LocalizedEnvironment.Translation.EyesGuard.Resting;
            UIViewModels.NotifyIcon.NextLongBreak = LocalizedEnvironment.Translation.EyesGuard.Resting;

            NextShortBreak = App.Configuration.ShortBreakGap;
            NextLongBreak = App.Configuration.LongBreakGap;

            var longWindow = new LongBreakWindow()
            {
                DataContext = UIViewModels.LongBreak
            };
            LongBreakVisibleTime = App.Configuration.LongBreakDuration;
            UIViewModels.LongBreak.TimeRemaining =
                LocalizedEnvironment.Translation.EyesGuard.LongBreakTimeRemaining.FormatWith(new
                {
                    LongBreakVisibleTime.Hours,
                    LongBreakVisibleTime.Minutes,
                    LongBreakVisibleTime.Seconds
                });

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

        #region Timing and Control :: During Rest

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

            UIViewModels.ShortLongBreakTimeRemaining.NextShortBreak = LocalizedEnvironment.Translation.EyesGuard.Waiting;
            UIViewModels.NotifyIcon.NextShortBreak = LocalizedEnvironment.Translation.EyesGuard.Waiting;

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
            UIViewModels.LongBreak.TimeRemaining =
                LocalizedEnvironment.Translation.EyesGuard.LongBreakTimeRemaining.FormatWith(new
                {
                    LongBreakVisibleTime.Hours,
                    LongBreakVisibleTime.Minutes,
                    LongBreakVisibleTime.Seconds
                });

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
    }
}
