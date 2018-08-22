using EyesGuard.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EyesGuard.ViewModels
{
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        private SolidColorBrush darkBrush = Brushes.Green;
        private SolidColorBrush lowBrush = Brushes.Green;
        private ImageSource source;
        private Visibility startProtectVisibility;
        private Visibility stopProtectVisibility;
        private string title = string.Empty;

        public SolidColorBrush DarkBrush {
            get => darkBrush;
            set
            {
                darkBrush = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush LowBrush
        {
            get => lowBrush;
            set
            {
                lowBrush = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        public Visibility StartProtectVisibility
        {
            get => startProtectVisibility;
            set
            {
                startProtectVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility StopProtectVisibility
        {
            get => stopProtectVisibility;
            set
            {
                stopProtectVisibility = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
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
