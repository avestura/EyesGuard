using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
