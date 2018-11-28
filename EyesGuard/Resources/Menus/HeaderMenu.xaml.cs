using EyesGuard.Animations;
using EyesGuard.AppManagers;
using EyesGuard.Extensions;
using EyesGuard.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyesGuard.Resources.Menus
{
    public partial class HeaderMenu : UserControl
    {
        public HeaderMenu()
        {
            InitializeComponent();

            DataContext = App.UIViewModels.HeaderMenu;
        }

        private void GoToStatictictsPage_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new Statistics());
        }

        private void GoToEyeSightImprove_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GoToSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new Settings());
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new MainPage());
        }

        private void ShowHideTimeRemaining_Click(object sender, RoutedEventArgs e)
        {
            App.Configuration.KeyTimesVisible = (App.Configuration.KeyTimesVisible) ? false : true;
            App.Configuration.SaveSettingsToFile();
        }

        private void FiveMinutesPause_Click(object sender, RoutedEventArgs e)
        {
            App.PauseProtection(TimeSpan.FromMinutes(5));
        }

        private void TenMinutesPause_Click(object sender, RoutedEventArgs e)
        {
            App.PauseProtection(TimeSpan.FromMinutes(10));
        }

        private void ThirtyMinutesPause_Click(object sender, RoutedEventArgs e)
        {
            App.PauseProtection(TimeSpan.FromMinutes(30));
        }

        private void OneHourPause_Click(object sender, RoutedEventArgs e)
        {
            App.PauseProtection(TimeSpan.FromHours(1));
        }

        private void TwoHourPause_Click(object sender, RoutedEventArgs e)
        {
            App.PauseProtection(TimeSpan.FromHours(2));
        }

        private void CustomPause_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new CustomPause());
        }

        private async void ApplicationExit_Click(object sender, RoutedEventArgs e)
        {
            await App.GetMainWindow().HideUsingLinearAnimationAsync();
            App.Current.Shutdown();
        }

        private void HideApp_Click(object sender, RoutedEventArgs e)
        {
            ChromeManager.Hide();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            App.ShowWarning(
                App.LocalizedEnvironment.Translation.Application.HelpPageText,
                WarningPage.PageStates.Info);
        }

        private void Resources_Click(object sender, RoutedEventArgs e)
        {
            var resourcesBase = App.LocalizedEnvironment.Translation.Application.Resources;
            App.ShowWarning(
                $"    - {resourcesBase.Content.Icons}\n    - {resourcesBase.Content.UIKit}"
                , WarningPage.PageStates.Info);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutBase = App.LocalizedEnvironment.Translation.Application.About;
            App.ShowWarning(
                $"{aboutBase.Content.InnerTitle}\n\n   {aboutBase.Content.PublisherInfo}\n   {aboutBase.Content.Repo}"
                , WarningPage.PageStates.About);
        }

        private void StartShortBreak_Click(object sender, RoutedEventArgs e)
        {
            App.AsApp().StartShortBreak();
        }

        private void StartLongBreak_Click(object sender, RoutedEventArgs e)
        {
            App.AsApp().StartLongBreak();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var aboutBase = App.LocalizedEnvironment.Translation.Application.About;
            App.ShowWarning(
                $"{aboutBase.Content.InnerTitle}\n\n   {aboutBase.Content.PublisherInfo}\n   {aboutBase.Content.Repo}"
                , WarningPage.PageStates.About);
        }

        private void Donate_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new Donate());
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Feedback_Menu_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new FeedbackPage());
        }
    }
}
