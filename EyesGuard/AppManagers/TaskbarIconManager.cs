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

                App.UIViewModels.NotifyIcon.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFBEFFD8")));
                App.UIViewModels.NotifyIcon.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FF12B754")));
                App.UIViewModels.NotifyIcon.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Sheild-Protecting.ico", UriKind.Absolute));
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.Title = "Strings.EyesGuard.TaskbarIcon.Protected".Translate();
            }
            else if (Configuration.ProtectionState == GuardStates.PausedProtecting)
            {
                App.UIViewModels.NotifyIcon.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFFFDBF")));
                App.UIViewModels.NotifyIcon.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFD6C90D")));
                App.UIViewModels.NotifyIcon.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Sheild-Paused.ico", UriKind.Absolute));
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.Title = "Strings.EyesGuard.TaskbarIcon.PausedProtected".Translate();

            }
            else if (Configuration.ProtectionState == GuardStates.NotProtecting)
            {
                App.UIViewModels.NotifyIcon.LowBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFFC0BE")));
                App.UIViewModels.NotifyIcon.DarkBrush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFF322A")));
                App.UIViewModels.NotifyIcon.Source = new BitmapImage(new Uri("pack://application:,,,/EyesGuard;component/Resources/Images/NonVectorIcons/Shield-Stopped.ico", UriKind.Absolute));
                App.UIViewModels.NotifyIcon.StartProtectVisibility = Visibility.Visible;
                App.UIViewModels.NotifyIcon.StopProtectVisibility = Visibility.Collapsed;
                App.UIViewModels.NotifyIcon.Title = "Strings.EyesGuard.TaskbarIcon.NotProtected".Translate();

            }
        }

    }
}
