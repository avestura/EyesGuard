using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.Extensions
{
    public static class ResourceExtensions
    {
        public static string Translate(this string resourceKey)
        {
            return App.Current.FindResource(resourceKey).ToString();
        }

        public static T Translate<T>(this string resourceKey)
        {
            return (T)App.Current.FindResource(resourceKey);
        }
    }
}
