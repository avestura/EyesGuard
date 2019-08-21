namespace EyesGuard.ViewModels
{
    public class ShortBreakViewModel : ViewModelBase
    {
        public ShortBreakViewModel()
        {
            ShortMessage = string.Empty; ;
            TimeRemaining = string.Empty; ;
        }

        public string ShortMessage
        {
            get { return GetValue(() => ShortMessage); }
            set { SetValue(() => ShortMessage, value); }
        }

        public string TimeRemaining
        {
            get { return GetValue(() => TimeRemaining); }
            set { SetValue(() => TimeRemaining, value); }
        }
    
    }
}
