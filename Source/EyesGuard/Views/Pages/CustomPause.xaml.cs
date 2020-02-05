using EyesGuard.Extensions;
using EyesGuard.Localization;
using EyesGuard.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FormatWith;

namespace EyesGuard.Views.Pages
{
    /// <summary>
    /// Interaction logic for CustomPause.xaml
    /// </summary>
    public partial class CustomPause : Page
    {
        public CustomPause()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Background = Brushes.Transparent;
            }
        }

        private void PauseProtection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string warning = "";
                int hours, minutes, seconds;
                hours = int.Parse(HoursUI.Text);
                minutes = int.Parse(MinutesUI.Text);
                seconds = int.Parse(SecondsUI.Text);

                if (hours > 11)
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.HoursLimit.FormatWith(new { Hours = 11 });

                if (minutes > 59)
                {
                    if(warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.MinutesLimit.FormatWith(new { Minutes = 59 });
                }

                if (seconds > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.SecondsLimit.FormatWith(new { Minutes = 59 });
                }

                if (new TimeSpan(hours, minutes, seconds).TotalSeconds < 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += "» " + App.LocalizedEnvironment.Translation.EyesGuard.TimeManipulation.ChooseLargerTime;
                }

                if (warning?.Length == 0)
                {
                    App.PauseProtection(new TimeSpan(hours, minutes, seconds));
                    App.GetMainWindow().MainFrame.Navigate(new MainPage());
                }
                else
                {
                    App.ShowWarning(warning);
                }
            }
            catch {
                App.ShowWarning(
                    $"{App.LocalizedEnvironment.Translation.EyesGuard.OperationFailed}.\n{App.LocalizedEnvironment.Translation.EyesGuard.CheckInput}."
                );
            }
        }
    }
}
