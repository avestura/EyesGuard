using EyesGuard.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyesGuard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        private  void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if(App.TaskbarIcon != null && !App.Configuration.TrayNotificationSaidBefore)
            {
                App.TaskbarIcon.ShowBalloonTip("در حال اجرا", "نرم افزار همچنان در پشت زمینه در حال اجراست. برای استفاده از امکانات نرم افزار روی نماد سپر در نوار وظیفه راست کلیک کنید", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
                App.Configuration.TrayNotificationSaidBefore = true;
                App.Configuration.SaveSettingsToFile();
            }
            App.Hide();

        }

        private void MaxRestoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else WindowState = WindowState.Maximized;
        }

        bool minimizing = false;
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!minimizing)
            {
                minimizing = true;

                WindowState = WindowState.Minimized;
                minimizing = false;
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MaximizeRect.Visibility = Visibility.Collapsed;
                RestoreCanvas.Visibility = Visibility.Visible;
            }
            else
            {
                MaximizeRect.Visibility = Visibility.Visible;
                RestoreCanvas.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //App.SystemDpiFactor = System.Windows.PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;
            

            if (App.LaunchMinimized)
                this.Hide();
            Config.LoadSettingsFromFile();

            //wc.Ellipse.Stroke = Brushes.DodgerBlue;
            //wc.Ellipse.Fill = Brushes.LightCyan;


            //if(App.UserScalingType == App.ScalingType.UseCutomScaling)
            //{
            //    double userRequest = App.UserScalingFactor.ConvertToDouble();
            //    double osRequest = App.SystemDpiFactor;

            //    double ratio = userRequest / osRequest;

            //    MainContainer.UserInterfaceCustomScale(ratio, false);

            //    Width *= ratio; Height *= ratio;

            //    this.BringWindowCenterScreen();
            //}


        }




        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

            try
            {
                CurrentPageTitleBlock.Text = ((Page)MainFrame.Content).Title;
                CurrentPageTitleBlock.MarginFadeInAnimation(new Thickness(0), new Thickness(20, 0, 0, 0), TimeSpan.FromMilliseconds(500));

                try
                {
                    MainFrame.MarginFadeInAnimation(new Thickness(0), new Thickness(20,0,0,0), TimeSpan.FromMilliseconds(500));
                }
                catch (Exception ee) { MessageBox.Show(ee.Message); }
            }
            catch { }
        }

        private void Title_Click(object sender, RoutedEventArgs e)
        {
            App.GetMainWindow().MainFrame.Navigate(new MainPage());
        }
    }
}
