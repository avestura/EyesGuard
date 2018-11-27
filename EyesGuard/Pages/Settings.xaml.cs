using EyesGuard.Extensions;
using EyesGuard.Localization;
using EyesGuard.Resources.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static EyesGuard.Localization.LanguageLoader;

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

        public Localization.Meta Meta => App.LocalizedEnvironment.Meta;
        public Localization.Translation Translation => App.LocalizedEnvironment.Translation;

        public ObservableCollection<string> ShortMessagesSource { get; set; } = new ObservableCollection<string>();

        public Visibility DeleteButtonVisibility
        {
            get { return (Visibility)GetValue(DeleteButtonVisibilityProperty); }
            set { SetValue(DeleteButtonVisibilityProperty, value); }
        }

        public static readonly DependencyProperty DeleteButtonVisibilityProperty =
            DependencyProperty.Register("DeleteButtonVisibility", typeof(Visibility), typeof(Settings));

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Background = Brushes.Transparent;
            }

            FillComboBoxWithLanguages();
            FillShortMessages();

            LanguagesCombo.SelectedItem =
                LanguagesBriefData.Value.First(x => x.Name == App.LocalizedEnvironment.Meta.CurrentCulture.Name);

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

            sytemIdleCheckbox.IsChecked = App.Configuration.SystemIdleDetectionEnabled;

            UseLanguageAsSourceCheckbox.IsChecked = App.Configuration.UseLanguageProvedidShortMessages;
        }

        private void FillShortMessages()
        {

                if (UseLanguageAsSourceCheckbox.IsChecked == true)
                {
                    ShortMessagesSource = new ObservableCollection<string>(App.LocalizedEnvironment.Translation.EyesGuard.ShortMessageSuggestions);
                    AddBtn.Visibility = Visibility.Collapsed;
                    DeleteButtonVisibility = Visibility.Collapsed;
                }
                else
                {
                    ShortMessagesSource = new ObservableCollection<string>(App.Configuration.CustomShortMessages);
                    DeleteButtonVisibility = Visibility.Visible;
                    AddBtn.Visibility = Visibility.Visible;
                }

                ShortMessages.ItemsSource = ShortMessagesSource;

        }

        private void FillComboBoxWithLanguages()
        {

            LanguagesCombo.ItemsSource = LanguagesBriefData.Value;
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
                    warning += string.Format("» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.HoursLimit, 11);

                if (sgM > 59 || sdM > 59 || lgM > 59 || ldM > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.MinutesLimit, 59);
                }

                if (sgS > 59 || sdS > 59 || lgS > 59 || ldS > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.SecondsLimit, 59);
                }
                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds >= new TimeSpan(lgH, lgM, lgS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.SGapMorethanLGap;
                }
                if (new TimeSpan(sdH, sdM, sdS).TotalSeconds >= new TimeSpan(ldH, ldM, ldS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.SDurationMorethanLDuration;
                }
                if (new TimeSpan(sdH, sdM, sdS).TotalMinutes > 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.SDurationTooHigh;
                }
                if (new TimeSpan(ldH, ldM, ldS).TotalHours > 2)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.LDurationTooHigh;
                }

                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds + new TimeSpan(sdH, sdM, sdS).TotalSeconds >= new TimeSpan(lgH,lgM,lgS).TotalSeconds)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.NotEnoughGapBetweenLandS;
                }

                if (new TimeSpan(sgH, sgM, sgS).TotalSeconds < 60)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.ShortGapLow;
                }

                if (new TimeSpan(lgH, lgM, lgS).TotalMinutes < 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.LongGapLow;
                }

                if (warning?.Length == 0)
                {
                    App.Configuration.ShortBreakGap = new TimeSpan(sgH, sgM, sgS);
                    App.Configuration.ShortBreakDuration = new TimeSpan(sdH, sdM, sdS);
                    App.Configuration.LongBreakGap = new TimeSpan(lgH, lgM, lgS);
                    App.Configuration.LongBreakDuration = new TimeSpan(ldH, ldM, ldS);
                    App.Configuration.ForceUserToBreak = forceUserCheckbox.IsChecked.Value;
                    App.Configuration.SaveStats = storeStatsCheckbox.IsChecked.Value;
                    App.Configuration.OnlyOneShortBreak = onlyOneShortbreakCheckbox.IsChecked.Value;
                    App.Configuration.AlertBeforeLongBreak = alertBeforeLongbreak.IsChecked.Value;
                    App.Configuration.SystemIdleDetectionEnabled = sytemIdleCheckbox.IsChecked.Value;
                    App.Configuration.ApplicationLocale = (LanguagesCombo.SelectedItem as LanguageHolder)?.Name ?? LocalizedEnvironment.DefaultLocale;
                    App.Configuration.UseLanguageProvedidShortMessages = UseLanguageAsSourceCheckbox.IsChecked.Value;

                    if (!App.Configuration.UseLanguageProvedidShortMessages)
                    {
                        App.Configuration.CustomShortMessages =
                            (ShortMessagesSource.Count > 0) ?
                            ShortMessagesSource.ToArray() :
                            new string[] { "Stare far-off" };
                    }

                    App.Configuration.SaveSettingsToFile();

                    App.ShowWarning(
                        App.LocalizedEnvironment.Translation.EyesGuard.Settings.SavedSuccessfully,
                        WarningPage.PageStates.Success,
                        new Settings());
                }
                else
                {
                    App.ShowWarning(warning);
                }
            }
            catch
            {
                App.ShowWarning($"{App.LocalizedEnvironment.Translation.EyesGuard.OperationFailed}.\n{App.LocalizedEnvironment.Translation.EyesGuard.CheckInput}.");
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

            App.ShowWarning(
                App.LocalizedEnvironment.Translation.EyesGuard.Settings.StatsSettings.StatsDeleted,
                WarningPage.PageStates.Success);
        }

        private void startupCheckbox_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void LanguagesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MaintainersLinks.Inlines.Clear();
                var translators = LanguageLoader.LoadMeta(((LanguageHolder)LanguagesCombo.SelectedItem).Name).Meta.Translators;
                for (int i = 0; i < translators.Length; i++)
                {
                    var translator = translators[i];
                    var link = new Hyperlink(new Run(translator.Name));
                    var popup = new Popup
                    {
                        Child = new TranslatorInfo
                        {
                            TranslatorName = translator.Name,
                            GitHubUsername = (string.IsNullOrWhiteSpace(translator.GitHubUsername)) ? App.LocalizedEnvironment.Translation.EyesGuard.Settings.LanguageSettings.NoAccount : $"@{translator.GitHubUsername}",
                            WebsiteUrl = (string.IsNullOrWhiteSpace(translator.Website)) ?App.LocalizedEnvironment.Translation.EyesGuard.Settings.LanguageSettings.NoWebsite : translator.Website,
                            Notes = (string.IsNullOrWhiteSpace(translator.Notes)) ? App.LocalizedEnvironment.Translation.EyesGuard.Settings.LanguageSettings.NoNotes : translator.Notes
                        },
                        PlacementTarget = MaintainersLinks,
                        Placement = PlacementMode.Mouse,
                        StaysOpen = false,
                        PopupAnimation = PopupAnimation.Slide
                    };
                    MaintainersLinks.Inlines.Add(popup);
                    link.Click += (s, ev) => popup.IsOpen = !popup.IsOpen;
                    MaintainersLinks.Inlines.Add(link);
                    if (i != translators.Length - 1)
                    {
                        MaintainersLinks.Inlines.Add(new Run($" {App.LocalizedEnvironment.Meta.CurrentCulture.TextInfo.ListSeparator} ")
                        {
                            Foreground = Brushes.White,
                            FontSize = 12
                        });
                    }
                }
            }
            catch { }
        }

        private void UseLanguageAsSourceCheckbox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if(this.IsLoaded)
                FillShortMessages();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ShortMessagesSource.Count <= 1) return;
            var btn = sender as Button;
            try
            {
                if(ShortMessagesSource.FirstOrDefault(x => x == (string)btn.Tag) is string s)
                {
                    ShortMessagesSource.Remove(s);
                }
            }
            catch { }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            NewMessagePopup.IsOpen = !NewMessagePopup.IsOpen;
        }

        private void SubmitMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if(UseLanguageAsSourceCheckbox.IsChecked == false
                && !ShortMessagesSource.Contains(MessageContent.Text))
            {
                ShortMessagesSource.Add(MessageContent.Text);
            }
        }
    }
}
