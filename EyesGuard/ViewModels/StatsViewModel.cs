using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.ViewModels
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        private long shortCount = 0;
        public long ShortCount
        {
            get
            {
                return shortCount;
            }
            set
            {
                shortCount = value;
                OnPropertyChanged("ShortCount");
            }
        }

        private long longCompletedCount = 0;
        public long LongCompletedCount
        {
            get
            {
                return longCompletedCount;
            }
            set
            {
                longCompletedCount = value;
                OnPropertyChanged("LongCompletedCount");
            }
        }


        private long longfailedCount = 0;
        public long LongFailedCount
        {
            get
            {
                return longfailedCount;
            }
            set
            {
                longfailedCount = value;
                OnPropertyChanged("LongFailedCount");
            }
        }

        private long pauseCount = 0;
        public long PauseCount
        {
            get
            {
                return pauseCount;
            }
            set
            {
                pauseCount = value;
                OnPropertyChanged("PauseCount");
            }
        }

        private long stopCount = 0;
        public long StopCount
        {
            get
            {
                return stopCount;
            }
            set
            {
                stopCount = value;
                OnPropertyChanged("StopCount");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
