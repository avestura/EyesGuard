using System.Windows;

namespace EyesGuard.ViewModels
{
    public class ShortLongBreakTimeRemainingViewModel : ViewModelBase
    {
        public ShortLongBreakTimeRemainingViewModel()
        {
           NextShortBreak = string.Empty; ;
           NextLongBreak = string.Empty; ;
           PauseTime = string.Empty; ;
           TimeRemainingVisibility = Visibility.Visible;
        }

       
        public string NextShortBreak {
            get { return GetValue(() => NextShortBreak); }
            set { SetValue(() => NextShortBreak, value); }
        }

       
        public string NextLongBreak
        {
            get { return GetValue(() => NextLongBreak); }
            set { SetValue(() => NextLongBreak, value); }
        }


        public string PauseTime
        {
            get { return GetValue(() => PauseTime); }
            set { SetValue(() => PauseTime, value); }
        }

        public Visibility TimeRemainingVisibility
        {
            get { return GetValue(() => TimeRemainingVisibility); }
            set { SetValue(() => TimeRemainingVisibility, value); }
        }

        private bool _protectionPause = false;
        public bool IsProtectionPaused
        {
            get => _protectionPause;

            set
            {
                if (SetField(ref _protectionPause, value))
                {
                    PauseVisibility = Visibility.Visible;
                    LongShortVisibility = Visibility.Collapsed;
                }
                else
                {
                    PauseVisibility = Visibility.Collapsed;
                    LongShortVisibility = Visibility.Visible;
                }

                OnPropertyChanged(nameof(PauseVisibility));
                OnPropertyChanged(nameof(LongShortVisibility));
            }
        }

        public Visibility PauseVisibility { get; set; } = Visibility.Collapsed;

        private Visibility longShortVisibility { get; set; } = Visibility.Visible;
        public Visibility LongShortVisibility {
            get => longShortVisibility; 
            set {
                longShortVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility idleVisibility { get; set; } = Visibility.Collapsed;
        public Visibility IdleVisibility
        {
            get => idleVisibility;
            set
            {
                idleVisibility = value;
                OnPropertyChanged();
            }
        }
    }
}
