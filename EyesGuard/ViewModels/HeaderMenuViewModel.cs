using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                OnPropertyChanged("IsTimeItemChecked");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
