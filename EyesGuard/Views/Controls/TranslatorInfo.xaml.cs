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

namespace EyesGuard.Views.Controls
{
    /// <summary>
    /// Interaction logic for TranslatorInfo.xaml
    /// </summary>
    public partial class TranslatorInfo : UserControl
    {
        public TranslatorInfo()
        {
            InitializeComponent();
        }

        public string TranslatorName
        {
            get { return (string)GetValue(TranslatorNameProperty); }
            set { SetValue(TranslatorNameProperty, value); }
        }

        public static readonly DependencyProperty TranslatorNameProperty =
            DependencyProperty.Register("TranslatorName", typeof(string), typeof(TranslatorInfo), null);

        public string GitHubUsername
        {
            get { return (string)GetValue(GitHubUsernameProperty); }
            set { SetValue(GitHubUsernameProperty, value); }
        }

        public static readonly DependencyProperty GitHubUsernameProperty =
            DependencyProperty.Register("GitHubUsername", typeof(string), typeof(TranslatorInfo), null);

        public string WebsiteUrl
        {
            get { return (string)GetValue(WebsiteUrlProperty); }
            set { SetValue(WebsiteUrlProperty, value); }
        }

        public static readonly DependencyProperty WebsiteUrlProperty =
            DependencyProperty.Register("WebsiteUrl", typeof(string), typeof(TranslatorInfo));

        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public static readonly DependencyProperty NotesProperty =
            DependencyProperty.Register("Notes", typeof(string), typeof(TranslatorInfo), null);
    }
}