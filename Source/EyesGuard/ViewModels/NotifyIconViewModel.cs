using FormatWith;
using System.Windows;
using System.Windows.Media;

namespace EyesGuard.ViewModels
{
    public class NotifyIconViewModel : ViewModelBase
    {

        private string _nextShortBreak;
        private string _nextLongBreak;
        private string _pauseRemaining;

        public NotifyIconViewModel()
        {
            Title = string.Empty;
            DarkBrush = Brushes.Green;
            LowBrush = Brushes.Green;
            PausedVisibility = Visibility.Collapsed;
            NextLongBreak = string.Empty;
            NextShortBreak = string.Empty;
            PauseRemaining = string.Empty;
        }

        public SolidColorBrush DarkBrush {
            get { return GetValue(() => DarkBrush); }
            set { SetValue(() => DarkBrush, value); }
        }

        public SolidColorBrush LowBrush
        {
            get { return GetValue(() => LowBrush); }
            set { SetValue(() => LowBrush, value); }
        }

        public ImageSource Source
        {
            get { return GetValue(() => Source); }
            set { SetValue(() => Source, value); }
        }


        public Visibility StartProtectVisibility
        {
           get { return GetValue(() => StartProtectVisibility); }
           set { SetValue(() => StartProtectVisibility, value); }
        }

        public Visibility StopProtectVisibility
        {
            get { return GetValue(() => StopProtectVisibility); }
            set { SetValue(() => StopProtectVisibility, value); }
        }

        public string Title
        {
            get { return GetValue(() => Title); }
            set { SetValue(() => Title, value); }
        }

        public string NextShortBreak
        {
            get => _nextShortBreak;
            set
            {
                SetField(ref _nextShortBreak, value);
                OnPropertyChanged(nameof(NextShortBreakFullText));
            }
        }

        public string NextLongBreak
        {
            get => _nextLongBreak;
            set
            {
                SetField(ref _nextLongBreak, value);
                OnPropertyChanged(nameof(NextLongBreakFullText));
            }
        }

        public Visibility PausedVisibility
        {
            get { return GetValue(() => PausedVisibility); }
            set { SetValue(() => PausedVisibility, value); }
        }

        public string PauseRemaining
        {
            get => _pauseRemaining;
            set
            {
                SetField(ref _pauseRemaining, value);
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

    }
}
