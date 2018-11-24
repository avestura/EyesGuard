using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for FeedbackPage.xaml
    /// </summary>
    public partial class FeedbackPage : Page
    {
        public FeedbackPage()
        {
            InitializeComponent();
        }

        public Localization.Meta Meta => App.LocalizedEnvironment.Meta;
        public Localization.Translation Translation => App.LocalizedEnvironment.Translation;

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Process.Start("https://github.com/0xaryan/EyesGuard/issues"));
            App.GetMainWindow().MainFrame.Navigate(new MainPage());
        }
    }
}
