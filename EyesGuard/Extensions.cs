using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static EyesGuard.App;

namespace EyesGuard
{
    public static class Extensions
    {

        public static void UserInterfaceCustomScale(this FrameworkElement element, double customScale, bool chengeDimentions = true)
        {

            element.LayoutTransform = new ScaleTransform(customScale, customScale, 0, 0);

            if (chengeDimentions)
            {
                element.Width *= customScale;
                element.Height *= customScale;
            }

        }

        public static void BringWindowCenterScreen(this Window window)
        {

            // Bring window center screen
            var screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            var screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            window.Top = ( screenHeight - window.Height ) / 2;
            window.Left = ( screenWidth - window.Width ) / 2;
        }

        public static double ConvertToDouble(this ScalingSize scalingSize)
        {
            switch(scalingSize)
            {
                case ScalingSize.Unset:
                case ScalingSize.X100:
                    return 1.00;
                case ScalingSize.X125:
                    return 1.25;
                case ScalingSize.X150:
                    return 1.5;
                case ScalingSize.X175:
                    return 1.75;
                case ScalingSize.X200:
                    return 2.00;
            }
            return 1.00;
        }

        public static string ConvertToPercentString(this double scalingSize)
        {
            return $"({(scalingSize * 100)} درصد)";
        }

    }
}
