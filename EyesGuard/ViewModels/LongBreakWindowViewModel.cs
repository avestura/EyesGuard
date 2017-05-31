using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyesGuard.ViewModels
{
    public class LongBreakWindowViewModel : INotifyPropertyChanged
    {
        private string timeRemaining = "";
        public string TimeRemaining
        {
            get
            {
                return timeRemaining;
            }
            set
            {
                timeRemaining = value;
                OnPropertyChanged("TimeRemaining");
            }
        }

        private Visibility _canCancel = Visibility.Visible;
        public Visibility CanCancel
        {
            get
            {
                return _canCancel;
            }
            set
            {
                _canCancel = value;
                OnPropertyChanged("CanCancel");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
