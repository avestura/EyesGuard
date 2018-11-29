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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
