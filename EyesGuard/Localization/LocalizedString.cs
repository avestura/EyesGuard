using EyesGuard.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace EyesGuard.Localization
{
    [MarkupExtensionReturnType(typeof(string))]
    public class LocalizedString : MarkupExtension
    {
        public string Value { get; }

        [ConstructorArgument("locale")]
        public string Locale { get; }

        public LocalizedString()
        {
            Value = string.Empty;
            Locale = LocalizedEnvironment.DefaultLocale;
        }

        public LocalizedString(string key)
        {
            Value = key;
            Locale = LocalizedEnvironment.DefaultLocale;
        }

        public LocalizedString(string key, string locale)
        {
            Value = key;
            Locale = locale;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                var env = LanguageLoader.CreateEnvironment(Locale, designMode: true);
                return env.Translation.GetPropValue<string>(Value);
            }

            return App.LocalizedEnvironment.Translation.GetPropValue<string>(Value);
        }
    }
}
