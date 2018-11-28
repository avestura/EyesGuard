using EyesGuard.AppManagers;
using EyesGuard.Pages;
using EyesGuard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyesGuard.Resources.Menus
{
    public partial class EyesGuardTaskbarIcon
    {
        private void TaskbarIcon_TrayMouseDoubleClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!App.GetMainWindow().IsVisible)
            ChromeManager.Show();
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

            if (!App.GetMainWindow().IsVisible)
                ChromeManager.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void TaskbarIcon_ContextMenuOpening(object sender, System.Windows.Controls.ContextMenuEventArgs e)
        {
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            App.UpdateTaskbarIcon();
        }

        private void StartProtect_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckIfResting()) return;

            if (App.Configuration.ProtectionState == App.GuardStates.PausedProtecting)
                App.ResumeProtection();
            else
                App.CurrentMainPage.ProtectionState = App.GuardStates.Protecting;
        }

        private void StopProtect_Click(object sender, RoutedEventArgs e)
        {
            if (App.CheckIfResting()) return;

            if (App.Configuration.SaveStats) App.UpdateIntruptOfStats(App.GuardStates.NotProtecting);
            App.CurrentMainPage.ProtectionState = App.GuardStates.NotProtecting;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new Settings());

            if (!App.GetMainWindow().IsVisible)
                ChromeManager.Show();
        }
    }
}
