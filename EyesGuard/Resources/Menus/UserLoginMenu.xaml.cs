using EyesGuard.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyesGuard.Resources.Menus
{
    public partial class UserLoginMenu : UserControl
    {
        public UserLoginMenu()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            App.ShowWarning("Strings.Application.NoLoginImplemented".Translate(), WarningPage.PageStates.Info);
        }
    }
}
