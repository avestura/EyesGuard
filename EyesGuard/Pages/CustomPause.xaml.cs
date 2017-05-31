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

namespace EyesGuard.Pages
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
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.HoursLimit").ToString(), 11);

                if(minutes > 59)
                {
                    if(warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.MinutesLimit").ToString(), 59);

                }

                if (seconds > 59)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.SecondsLimit").ToString(), 59);

                }

                if (new TimeSpan(hours, minutes, seconds).TotalSeconds < 5)
                {
                    if (warning != "")
                        warning += "\n";
                    warning += string.Format("» " + App.Current.FindResource("Strings.EyesGuard.ChooseLargerTime").ToString(), 59);

                }

                if (warning == "")
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
                App.ShowWarning($"{App.Current.FindResource("Strings.EyesGuard.OperationFailed")}.\n{App.Current.FindResource("Strings.EyesGuard.CheckInput")}.");
            }
        }
    }
}
