using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.ViewModels
{
    public class HeaderMenuViewModel : INotifyPropertyChanged
    {
        private bool _isTimeItemChecked = true;

        public bool IsTimeItemChecked
        {
            get { return _isTimeItemChecked; }
            set
            {
                _isTimeItemChecked = value;
                OnPropertyChanged();
            }
        }

        private bool manualBreakEnabled = true;

        public bool ManualBreakEnabled
        {
            get { return manualBreakEnabled; }
            set
            {
                manualBreakEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isFeedbackAvailable = true;

        public bool IsFeedbackAvailable
        {
            get { return isFeedbackAvailable; }
            set
            {
                isFeedbackAvailable = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
