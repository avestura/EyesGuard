using EyesGuard.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static EyesGuard.App;

namespace EyesGuard.AppManagers
{
    public static class TaskbarIconManager
    {

        public static void UpdateTaskbarIcon()
        {
            if (Configuration.ProtectionState == GuardStates.Protecting)
            {

                App.UIViewModels.NotifyIcon.DarkBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.Protecting.DarkBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.LowBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.Protecting.LowBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.Source = "Images.Bitmap.EyesGuard.TaskbarIcon.Protecting".Translate<BitmapImage>();
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.Title = LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Protected;
            }
            else if (Configuration.ProtectionState == GuardStates.PausedProtecting)
            {
                App.UIViewModels.NotifyIcon.DarkBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.PausedProtecting.DarkBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.LowBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.PausedProtecting.LowBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.Source = "Images.Bitmap.EyesGuard.TaskbarIcon.PausedProtecting".Translate<BitmapImage>();
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.Title = LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.PausedProtected;

            }
            else if (Configuration.ProtectionState == GuardStates.NotProtecting)
            {
                App.UIViewModels.NotifyIcon.DarkBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.NotProtecting.DarkBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.LowBrush = "EyesGuard.SolidColorBrushes.TaskbarIcon.NotProtecting.LowBrush".Translate<SolidColorBrush>();
                App.UIViewModels.NotifyIcon.Source = "Images.Bitmap.EyesGuard.TaskbarIcon.NotProtecting".Translate<BitmapImage>();
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.Title = LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.NotProtected;

            }
        }

    }
}
