using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EyesGuard.ViewModels
{
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush darkBrush = Brushes.Green;
        public SolidColorBrush DarkBrush {
            get
            {
                return darkBrush;
            }
            set
            {
                darkBrush = value;
                OnPropertyChanged("DarkBrush");
            }
        }

        private SolidColorBrush lowBrush = Brushes.Green;
        public SolidColorBrush LowBrush
        {
            get
            {
                return darkBrush;
            }
            set
            {
                darkBrush = value;
                OnPropertyChanged("LowBrush");
            }
        }

        private ImageSource source ;
        public ImageSource Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
                OnPropertyChanged("Source");
            }
        }

        private Visibility startProtectVisibility;
        public Visibility StartProtectVisibility
        {
            get
            {
                return startProtectVisibility;
            }
            set
            {
                startProtectVisibility = value;
                OnPropertyChanged("StartProtectVisibility");
            }
        }

        private Visibility stopProtectVisibility;
        public Visibility StopProtectVisibility
        {
            get
            {
                return stopProtectVisibility;
            }
            set
            {
                stopProtectVisibility = value;
                OnPropertyChanged("StopProtectVisibility");
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
