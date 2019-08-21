using System.Windows;

namespace EyesGuard.ViewModels
{
    public class LongBreakWindowViewModel : ViewModelBase
    {
        public LongBreakWindowViewModel()
        {
            TimeRemaining = string.Empty;
            CanCancel = Visibility.Visible;
        }

        public string TimeRemaining
        {
            get { return GetValue(() => TimeRemaining); }
            set { SetValue(() => TimeRemaining, value); }
        }

        public Visibility CanCancel
        {
            get { return GetValue(() => CanCancel); }
            set { SetValue(() => CanCancel, value); }

        }
    }
}
