using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.ViewModels
{
    public class ShortBreakViewModel : INotifyPropertyChanged
    {

        private string shortMessage = "";
        public string ShortMessage
        {
            get
            {
                return shortMessage;
            }
            set
            {
                shortMessage = value;
                OnPropertyChanged("ShortMessage");
            }
        }

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


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
