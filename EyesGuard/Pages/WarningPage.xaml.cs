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

namespace EyesGuard.Pages
{
    /// <summary>
    /// Interaction logic for WarningPage.xaml
    /// </summary>
    public partial class WarningPage : Page
    {

        public enum PageStates
        {
            Error, Warning, Success, Info, About
        }


        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(WarningPage), new PropertyMetadata("بدون پیام"));





        public PageStates PageState
        {
            get { return (PageStates)GetValue(PageStateProperty); }
            set { SetValue(PageStateProperty, value); }
        }
        public static readonly DependencyProperty PageStateProperty =
            DependencyProperty.Register("PageState", typeof(PageStates), typeof(WarningPage), new PropertyMetadata(PageStates.Warning));




        public Page ReturnPage { get; set; }

        public WarningPage(Page returnPage, string message, PageStates state = PageStates.Warning, string pageTitle = "")
        {
            Message = message;
            ReturnPage = returnPage;
            PageState = state;

            var vm = new WarningPageViewModel();

            DataContext = vm;

            if (PageState == PageStates.Warning)
            {
                vm.Icon = FontAwesome.WPF.FontAwesomeIcon.Warning;
                vm.Brush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFEEC78A")));
                if (pageTitle == "")
                    vm.PageTitle = "توجه";
            }
            else if (PageState == PageStates.Success)
            {
                vm.Icon = FontAwesome.WPF.FontAwesomeIcon.Check;
                vm.Brush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#8ED28A")));
                if (pageTitle == "")
                    vm.PageTitle = "موفقیت آمیز";
            }
            else if (PageState == PageStates.Error)
            {
                vm.Icon = FontAwesome.WPF.FontAwesomeIcon.ExclamationCircle;
                vm.Brush = new SolidColorBrush(((Color)ColorConverter.ConvertFromString("#FFFD9884")));
                if (pageTitle == "")
                    vm.PageTitle = "خطا";
            }
            else if (PageState == PageStates.Info)
            {
                vm.Icon = FontAwesome.WPF.FontAwesomeIcon.Comment;
                vm.Brush = Brushes.White;
                if (pageTitle == "")
                    vm.PageTitle = "اطلاعات";
            }
            else if (PageState == PageStates.About)
            {
                vm.Icon = FontAwesome.WPF.FontAwesomeIcon.Rocket;
                vm.Brush = Brushes.White;
                if (pageTitle == "")
                    vm.PageTitle = "درباره نرم افزار";
            }

            if (pageTitle != "")
                vm.PageTitle = pageTitle;

            InitializeComponent();

    

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                this.Background = Brushes.Transparent;
            }

         

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(ReturnPage);
        }
    }
}
