using System.Windows.Media;

namespace EyesGuard.ViewModels
{
    public class WarningPageViewModel : ViewModelBase
    {
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return GetValue(() => Icon); }
            set { SetValue(() => Icon, value); }
        }

        public SolidColorBrush Brush

        {
            get { return GetValue(() => Brush); }
            set { SetValue(() => Brush, value); }
        }

        public string PageTitle
        {
            get { return GetValue(() => PageTitle); }
            set { SetValue(() => PageTitle, value); }
        }
    }
}
