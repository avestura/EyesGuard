using EyesGuard.Views.Windows;
using System.Windows;

namespace EyesGuard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App AsApp() => (App)Current;

        public static MainWindow GetMainWindow() => (MainWindow)App.Current.MainWindow;

        public enum GuardStates
        {
            PausedProtecting, Protecting, NotProtecting
        }
    }
}
