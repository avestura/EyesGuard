using EyesGuard.AppManagers;
using EyesGuard.Configurations;
using EyesGuard.Extensions;
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

            if (App.LaunchMinimized)
                this.Hide();
        }

        public Localization.Meta Meta => App.LocalizedEnvironment.Meta;
        public Localization.Translation Translation => App.LocalizedEnvironment.Translation;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ChromeManager.Hide();
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
            Configuration.LoadSettingsFromFile();
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
