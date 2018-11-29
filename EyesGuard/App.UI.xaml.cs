using EyesGuard.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EyesGuard
{
    using static EyesGuard.Views.Pages.WarningPage;

    public partial class App
    {
        public static void ShowWarning(
            string message,
            PageStates state = PageStates.Warning,
            Page navPage = null,
            string pageTitle = "")
        {
            try
            {
                navPage = navPage ?? (Page)GetMainWindow().MainFrame.Content;
                GetMainWindow().MainFrame.Navigate(new WarningPage(navPage, message, state, pageTitle));
            }
            catch { }
        }

        public static string GetShortWindowMessage()
        {
            var messagesBase = (Configuration.UseLanguageProvedidShortMessages) ?
                LocalizedEnvironment.Translation.EyesGuard.ShortMessageSuggestions :
                Configuration.CustomShortMessages;

            ShortMessageIteration++;
            ShortMessageIteration %= messagesBase.Length;

            return messagesBase[ShortMessageIteration];
        }
    }
}
