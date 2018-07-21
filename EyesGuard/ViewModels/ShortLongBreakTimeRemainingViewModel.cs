using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyesGuard.ViewModels
{
    public class ShortLongBreakTimeRemainingViewModel : INotifyPropertyChanged
    {
        private string _nextShortBreak = "";
        public string NextShortBreak {
            get
            {
                return _nextShortBreak;
            }
            set
            {
                _nextShortBreak = value;
                OnPropertyChanged();
            }

        }

        private string _nextLongBreak = "";
        public string NextLongBreak
        {
            get
            {
                return _nextLongBreak;
            }
            set
            {
                _nextLongBreak = value;
                OnPropertyChanged();
            }

        }

        private string _pauseTime = "";
        public string PauseTime
        {
            get
            {
                return _pauseTime;
            }
            set
            {
                _pauseTime = value;
                OnPropertyChanged();
            }

        }

        private Visibility _timeRemainVisibility = Visibility.Visible;
        public Visibility TimeRemainingVisibility
        {
            get
            {
                return _timeRemainVisibility;
            }
            set
            {
                _timeRemainVisibility = value;
                OnPropertyChanged();
            }

        }

        private bool _protectionPause = false;
        public bool IsProtectionPaused
        {
            get
            {
                return _protectionPause;
            }
            set
            {
                _protectionPause = value;
                OnPropertyChanged();

                if (_protectionPause)
                {
                    PauseVisibility = Visibility.Visible;
                    LongShortVisibility = Visibility.Collapsed;
                }
                else
                {
                    PauseVisibility = Visibility.Collapsed;
                    LongShortVisibility = Visibility.Visible;

                }
                OnPropertyChanged("PauseVisibility");
                OnPropertyChanged("LongShortVisibility");
            }

        }

        public Visibility PauseVisibility { get; set; } = Visibility.Collapsed;

        private Visibility _longShortVisibility { get; set; } = Visibility.Visible;
        public Visibility LongShortVisibility {
            get { return _longShortVisibility; }
            set {
                _longShortVisibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
