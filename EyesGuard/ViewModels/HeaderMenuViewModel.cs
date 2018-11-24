using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
                OnPropertyChanged();
            }
        }

        private bool manualBreakEnabled = true;
        public bool ManualBreakEnabled
        {
            get { return manualBreakEnabled; }
            set
            {
                manualBreakEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool isFeedbackAvailable = true;
        public bool IsFeedbackAvailable
        {
            get { return isFeedbackAvailable; }
            set
            {
                isFeedbackAvailable = value;
                OnPropertyChanged();
            }
        }

        public string Header_EyesGuard_Title => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.EyesGuard.Header;
        public string Header_EyesGuard_MainMenu => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.EyesGuard.MainMenu;
        public string Header_EyesGuard_Hide => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.EyesGuard.Hide;
        public string Header_EyesGuard_Exit => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.EyesGuard.Exit;

        public string Menu_PauseFor => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.PauseFor;
        public string Menu_FiveMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.FiveMins;
        public string Menu_TenMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.TenMins;
        public string Menu_ThirtyMins => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.ThirtyMins;
        public string Menu_OneHour => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.OneHour;
        public string Menu_TwoHours => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.TwoHours;
        public string Menu_Custom => App.LocalizedEnvironment.Translation.ShellExtensions.TaskbarIcon.Menu.Custom;

        public string Header_Tools_Title => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Tools.Header;
        public string Header_Tools_Stats => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Tools.Stats;
        public string Header_Tools_Settings => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Tools.Settings;

        public string Header_Breaks_Title => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Breaks.Header;
        public string Header_Breaks_GoShort => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Breaks.GoShort;
        public string Header_Breaks_GoLong => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Breaks.GoLong;

        public string Header_View_Title => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.View.Header;
        public string Header_View_KeyTimes => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.View.KeyTimes;

        public string Header_Help_Title => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.Header;
        public string Header_Help_Resources => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.Resources;
        public string Header_Help_SendFeedback => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.SendFeedback;
        public string Header_Help_EyesGuardHelp => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.EyesGuardHelp;
        public string Header_Help_Donate => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.Donate;
        public string Header_Help_About => App.LocalizedEnvironment.Translation.EyesGuard.HeaderMenu.Help.About;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
