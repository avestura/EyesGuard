using EyesGuard.Views.Animations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.AppManagers
{
    public static class ChromeManager
    {
        private static bool _hiding = false;

        public async static void Hide()
        {
            if (!_hiding)
            {
                _hiding = true;
                await App.GetMainWindow().HideUsingLinearAnimationAsync();
                App.GetMainWindow().Hide();
                _hiding = false;
            }
        }

        private static bool _closing = false;

        public async static void Close()
        {
            if (!_closing)
            {
                _closing = true;
                await App.GetMainWindow().HideUsingLinearAnimationAsync(200);
                App.GetMainWindow().Close();
                _closing = false;
            }
        }

        private static bool _showing = false;

        public async static void Show()
        {
            if (!_showing)
            {
                _showing = true;
                App.GetMainWindow().Show();
                await App.GetMainWindow().ShowUsingLinearAnimationAsync();
                App.GetMainWindow().Show();
                App.GetMainWindow().BringIntoView();
                _showing = false;
            }
        }
    }
}
