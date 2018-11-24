using EyesGuard.Extensions;
using FormatWith;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EyesGuard.ViewModels
{
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush darkBrush = Brushes.Green;
        private SolidColorBrush lowBrush = Brushes.Green;
        private ImageSource source;
        private Visibility startProtectVisibility;
        private Visibility stopProtectVisibility;
        private string title = string.Empty;
        private string _nextShortBreak;
        private string _nextLongBreak;
        private Visibility _pausedVisibility = Visibility.Collapsed;
        private string _pauseRemaining;

        public SolidColorBrush DarkBrush {
            get => darkBrush;
            set
            {
                darkBrush = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush LowBrush
        {
            get => lowBrush;
            set
            {
                lowBrush = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        public Visibility StartProtectVisibility
        {
            get => startProtectVisibility;
            set
            {
                startProtectVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility StopProtectVisibility
        {
            get => stopProtectVisibility;
            set
            {
                stopProtectVisibility = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public string NextShortBreak
        {
            get => _nextShortBreak;
            set
            {
                _nextShortBreak = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NextShortBreakFullText));
            }
        }

        public string NextLongBreak
        {
            get => _nextLongBreak;
            set
            {
                _nextLongBreak = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NextLongBreakFullText));
            }
        }

        public Visibility PausedVisibility
        {
            get => _pausedVisibility;
            set
            {
                _pausedVisibility = value;
                OnPropertyChanged();
            }
        }

        public string PauseRemaining
        {
            get => _pauseRemaining;
            set
            {
                _pauseRemaining = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PauseRemainingFullText));
            }
        }

        public string PauseRemainingFullText =>
            App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.PausedFor.FormatWith(new
            {
                PauseRemaining
            });

        public string NextShortBreakFullText =>
            App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.NextShortBreak.FormatWith(new
            {
                NextShortBreak
            });

        public string NextLongBreakFullText =>
            App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.NextLongBreak.FormatWith(new
            {
                NextLongBreak
            });

        public string TooltipTitle => App.LocalizedEnvironment.Translation.Application.HeaderTitle;

        public string Menu_PauseFor => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.PauseFor;
        public string Menu_ShowMainMenu => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.ShowMainMenu;
        public string Menu_StartProtection => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.StartProtection;
        public string Menu_StopProtection => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.StopProtection;
        public string Menu_FiveMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.FiveMins;
        public string Menu_TenMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.TenMins;
        public string Menu_ThirtyMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.ThirtyMins;
        public string Menu_OneHour => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.OneHour;
        public string Menu_TwoHours => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.TwoHours;
        public string Menu_Custom => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.Custom;
        public string Menu_Settings => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.Settings;
        public string Menu_Exit => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.Exit;

        public FlowDirection Menu_FlowDirection => (App.LocalizedEnvironment.Meta.CurrentCulture.TextInfo.IsRightToLeft) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
