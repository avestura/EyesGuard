using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyesGuard.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Background = Brushes.Transparent;
            }

            shortGapHours.Text   = App.Configuration.ShortBreakGap.Hours.ToString();
            shortGapMinutes.Text = App.Configuration.ShortBreakGap.Minutes.ToString();
            shortGapSeconds.Text = App.Configuration.ShortBreakGap.Seconds.ToString();

            longGapHours.Text   = App.Configuration.LongBreakGap.Hours.ToString();
            longGapMinutes.Text = App.Configuration.LongBreakGap.Minutes.ToString();
            longGapSeconds.Text = App.Configuration.LongBreakGap.Seconds.ToString();

            shortDurationHours.Text = App.Configuration.ShortBreakDuration.Hours.ToString();
            shortDurationMinutes.Text = App.Configuration.ShortBreakDuration.Minutes.ToString();
            shortDurationSeconds.Text = App.Configuration.ShortBreakDuration.Seconds.ToString();

            longDurationHours.Text = App.Configuration.LongBreakDuration.Hours.ToString();
            longDurationMinutes.Text = App.Configuration.LongBreakDuration.Minutes.ToString();
            longDurationSeconds.Text = App.Configuration.LongBreakDuration.Seconds.ToString();

            forceUserCheckbox.IsChecked = App.Configuration.ForceUserToBreak;
            onlyOneShortbreakCheckbox.IsChecked = App.Configuration.OnlyOneShortBreak;
            storeStatsCheckbox.IsChecked = App.Configuration.SaveStats;
            alertBeforeLongbreak.IsChecked = App.Configuration.AlertBeforeLongBreak;

            //useSystemDpiCheckbox.IsChecked = App.UserScalingType == App.ScalingType.UseWindowsDPIScaling;
            //ScalingFactorText.Text = App.SystemDpiFactor.ConvertToPercentString();
            //startupCheckbox.IsChecked = App.GlobalConfig.RunAtStartUp;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string warning = "";

                // sg: Short Gap
                // sd: Short Duration
                // lg: Long Gap
                // ld: Long Duration
                int sgH, sgM, sgS, sdH, sdM, sdS, lgH, lgM, lgS, ldH, ldM, ldS;

                sgH = int.Parse(shortGapHours.Text);
                sgM = int.Parse(shortGapMinutes.Text);
                sgS = int.Parse(shortGapSeconds.Text);

                sdH = int.Parse(shortDurationHours.Text);
                sdM = int.Parse(shortDurationMinutes.Text);
                sdS = int.Parse(shortDurationSeconds.Text);

                lgH = int.Parse(longGapHours.Text);
                lgM = int.Parse(longGapMinutes.Text);
                lgS = int.Parse(longGapSeconds.Text);

                ldH = int.Parse(longDurationHours.Text);
                ldM = int.Parse(longDurationMinutes.Text);
                ldS = int.Parse(longDurationSeconds.Text);

                if (sgH > 11 || sdH > 11 || lgH > 11 || ldH > 11)
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.HoursLimit").ToString(), 11);

                if (sgM > 59 || sdM > 59 || lgM > 59 || ldM > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.MinutesLimit").ToString(), 59);

                }

                if (sgS > 59 || sdS > 59 || lgS > 59 || ldS > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.SecondsLimit").ToString(), 59);

                }
                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds >= new TimeSpan(lgH, lgM, lgS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.SGapMorethanLGap");

                }
                if (new TimeSpan(sdH, sdM, sdS).TotalSeconds >= new TimeSpan(ldH, ldM, ldS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.SDurationMorethanLDuration");

                }
                if (new TimeSpan(sdH, sdM, sdS).TotalMinutes > 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.SDurationTooHigh");

                }
                if (new TimeSpan(ldH, ldM, ldS).TotalHours > 2)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.LDurationTooHigh");

                }

                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds + new TimeSpan(sdH, sdM, sdS).TotalSeconds >= new TimeSpan(lgH,lgM,lgS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.NotEnoughGapBetweenLandS");

                }

                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds < 60)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.ShortGapLow");

                }

                if (new TimeSpan(lgH, lgM, lgS).TotalMinutes < 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.Current.FindResource("Strings.EyesGuard.LongGapLow");

                }

                if (warning == "")
                {

                    App.Configuration.ShortBreakGap = new TimeSpan(sgH, sgM, sgS);
                    App.Configuration.ShortBreakDuration = new TimeSpan(sdH, sdM, sdS);
                    App.Configuration.LongBreakGap = new TimeSpan(lgH, lgM, lgS);
                    App.Configuration.LongBreakDuration = new TimeSpan(ldH, ldM, ldS);
                    App.Configuration.ForceUserToBreak = forceUserCheckbox.IsChecked.Value;
                    App.Configuration.SaveStats = storeStatsCheckbox.IsChecked.Value;
                    App.Configuration.OnlyOneShortBreak = onlyOneShortbreakCheckbox.IsChecked.Value;
                    App.Configuration.AlertBeforeLongBreak = alertBeforeLongbreak.IsChecked.Value;
                    //App.GlobalConfig.RunAtStartUp = startupCheckbox.IsChecked.Value;

                    App.Configuration.SaveSettingsToFile();

                    App.ShowWarning(App.Current.FindResource("Strings.EyesGuard.Settings.SavedSuccessfully").ToString(), WarningPage.PageStates.Success, new Settings());

                }
                else
                {
                    App.ShowWarning(warning);
                }
            }
            catch
            {
                App.ShowWarning($"{App.Current.FindResource("Strings.EyesGuard.OperationFailed")}.\n{App.Current.FindResource("Strings.EyesGuard.CheckInput")}.");
            }
        }

        private void ClearHistoryFirstStep_Click(object sender, RoutedEventArgs e)
        {
            uSureBlock.MarginFadeInAnimation(new Thickness(0), new Thickness(5, 0, 0, 0));
        }

        private void ClearHistoryCancel_Click(object sender, RoutedEventArgs e)
        {
            uSureBlock.MarginFadeOutAnimation(new Thickness(5,0,0,0), new Thickness(0));
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            uSureBlock.MarginFadeOutAnimation(new Thickness(5, 0, 0, 0), new Thickness(0));

            App.Configuration.ShortBreaksCompleted = 0;
            App.Configuration.LongBreaksCompleted = 0;
            App.Configuration.LongBreaksFailed = 0;
            App.Configuration.StopCount = 0;
            App.Configuration.PauseCount = 0;
            App.Configuration.SaveSettingsToFile();
            App.UpdateStats();
            
            App.ShowWarning(App.Current.FindResource("Strings.EyesGuard.Settings.StatsDeleted").ToString(),
                WarningPage.PageStates.Success);
        }
    }
}
