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
    /// Interaction logic for Donate.xaml
    /// </summary>
    public partial class Donate : Page
    {
        public Donate()
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

        private void DonateButton_Click(object sender, RoutedEventArgs e)
        {

            int pay = 0;

            const int MinPay = 10000;
            const int MaxPay = 150000000;

            try
            {
                pay = int.Parse(DonatePay.Text + "0");
                if (pay < MinPay) pay = MinPay;
                if (pay > MaxPay) pay = MaxPay;
            } catch { }

            try
            {
                Process.Start($"https://idpay.ir/aryansoftware-donate?amount={pay}&desc={DonateMessage.Text}&name={DonateName.Text}");

            } catch { }

            App.ShowWarning(App.Current.FindResource("Strings.Application.Donate").ToString(), WarningPage.PageStates.Donate, new MainPage());

        }
    }
}
