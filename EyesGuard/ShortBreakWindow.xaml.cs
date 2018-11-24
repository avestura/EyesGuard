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

namespace EyesGuard
{
    /// <summary>
    /// Interaction logic for ShortBreakWindow.xaml
    /// </summary>
    public partial class ShortBreakWindow : Window
    {
        public ShortBreakWindow()
        {
            App.CurrentShortBreakWindow = this;
            InitializeComponent();
        }

        public Localization.Meta Meta => App.LocalizedEnvironment.Meta;
        public Localization.Translation Translation => App.LocalizedEnvironment.Translation;

        public bool LetItClose { get; set; } = false;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!LetItClose)
                e.Cancel = true;
        }
    }
}
