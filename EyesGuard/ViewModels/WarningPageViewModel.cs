using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EyesGuard.ViewModels
{
    public class WarningPageViewModel : INotifyPropertyChanged
    {
        private FontAwesome.WPF.FontAwesomeIcon icon;
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                OnPropertyChanged("Icon");
            }
        }

        private SolidColorBrush brush;
        public SolidColorBrush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                brush = value;
                OnPropertyChanged("Brush");
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get
            {
                return pageTitle;
            }
            set
            {
                pageTitle = value;
                OnPropertyChanged("PageTitle");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
