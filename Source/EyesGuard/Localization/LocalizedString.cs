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
using EyesGuard.Data;
using static EyesGuard.Data.LanguageLoader;

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
            Locale = FsLanguageLoader.DefaultLocale;
        }

        public LocalizedString(string key)
        {
            Value = key;
            Locale = FsLanguageLoader.DefaultLocale;
        }

        public LocalizedString(string key, string locale)
        {
            Value = key;
            Locale = locale;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            if (DesignerExtensions.IsRunningInVisualStudioDesigner)
            {
                var env = FsLanguageLoader.CreateEnvironment(Locale, designMode: true);

                return env.Translation.GetPropValue<string>(Value);
            }

            return App.LocalizedEnvironment.Translation.GetPropValue<string>(Value);
        }
    }
}
