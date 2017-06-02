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


            shortGapHours.Text   = App.GlobalConfig.ShortBreakGap.Hours.ToString();
            shortGapMinutes.Text = App.GlobalConfig.ShortBreakGap.Minutes.ToString();
            shortGapSeconds.Text = App.GlobalConfig.ShortBreakGap.Seconds.ToString();

            longGapHours.Text   = App.GlobalConfig.LongBreakGap.Hours.ToString();
            longGapMinutes.Text = App.GlobalConfig.LongBreakGap.Minutes.ToString();
            longGapSeconds.Text = App.GlobalConfig.LongBreakGap.Seconds.ToString();

            shortDurationHours.Text = App.GlobalConfig.ShortBreakDuration.Hours.ToString();
            shortDurationMinutes.Text = App.GlobalConfig.ShortBreakDuration.Minutes.ToString();
            shortDurationSeconds.Text = App.GlobalConfig.ShortBreakDuration.Seconds.ToString();

            longDurationHours.Text = App.GlobalConfig.LongBreakDuration.Hours.ToString();
            longDurationMinutes.Text = App.GlobalConfig.LongBreakDuration.Minutes.ToString();
            longDurationSeconds.Text = App.GlobalConfig.LongBreakDuration.Seconds.ToString();

            forceUserCheckbox.IsChecked = App.GlobalConfig.ForceUserToBreak;
            onlyOneShortbreakCheckbox.IsChecked = App.GlobalConfig.OnlyOneShortBreak;
            storeStatsCheckbox.IsChecked = App.GlobalConfig.SaveStats;
            alertBeforeLongbreak.IsChecked = App.GlobalConfig.AlertBeforeLongBreak;
            //startupCheckbox.IsChecked = App.GlobalConfig.RunAtStartUp;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string warning = "";
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

                    App.GlobalConfig.ShortBreakGap = new TimeSpan(sgH, sgM, sgS);
                    App.GlobalConfig.ShortBreakDuration = new TimeSpan(sdH, sdM, sdS);
                    App.GlobalConfig.LongBreakGap = new TimeSpan(lgH, lgM, lgS);
                    App.GlobalConfig.LongBreakDuration = new TimeSpan(ldH, ldM, ldS);
                    App.GlobalConfig.ForceUserToBreak = forceUserCheckbox.IsChecked.Value;
                    App.GlobalConfig.SaveStats = storeStatsCheckbox.IsChecked.Value;
                    App.GlobalConfig.OnlyOneShortBreak = onlyOneShortbreakCheckbox.IsChecked.Value;
                    App.GlobalConfig.AlertBeforeLongBreak = alertBeforeLongbreak.IsChecked.Value;
                    //App.GlobalConfig.RunAtStartUp = startupCheckbox.IsChecked.Value;

                    App.GlobalConfig.SaveSettingsToFile();

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

            App.GlobalConfig.ShortBreaksCompleted = 0;
            App.GlobalConfig.LongBreaksCompleted = 0;
            App.GlobalConfig.LongBreaksFailed = 0;
            App.GlobalConfig.StopCount = 0;
            App.GlobalConfig.PauseCount = 0;
            App.GlobalConfig.SaveSettingsToFile();
            App.UpdateStats();
            
            App.ShowWarning(App.Current.FindResource("Strings.EyesGuard.Settings.StatsDeleted").ToString(),
                WarningPage.PageStates.Success);
        }
    }
}
