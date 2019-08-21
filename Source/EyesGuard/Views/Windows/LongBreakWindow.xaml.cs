using EyesGuard.Views.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static EyesGuard.App;

namespace EyesGuard.Views.Windows
{
    /// <summary>
    /// Interaction logic for LongBreakWindow.xaml
    /// </summary>
    public partial class LongBreakWindow : Window
    {
        public LongBreakWindow()
        {
            App.CurrentLongBreakWindow = this;
            InitializeComponent();
        }

        public bool LetItClose { get; set; } = false;

        private async void CloseLongBreak_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LetItClose = true;
                if (App.Configuration.SaveStats)
                {
                    App.Configuration.LongBreaksFailed++;
                    App.Configuration.SaveSettingsToFile();
                }
                await App.CurrentLongBreakWindow.HideUsingLinearAnimationAsync();
                App.CurrentLongBreakWindow.Close();
                App.CurrentLongBreakWindow = null;
                App.ShortBreakShownOnce = false;
                if (App.Configuration.ProtectionState == GuardStates.Protecting)
                {
                    App.ShortBreakHandler.Start();
                    App.LongBreakHandler.Start();
                }
                LongDurationCounter.Stop();
                App.UIViewModels.HeaderMenu.ManualBreakEnabled = true;
            }
            catch { }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!LetItClose)
                e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.Configuration.ForceUserToBreak)
                Cursor = Cursors.None;
        }
    }
}
