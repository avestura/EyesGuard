using EyesGuard.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using static EyesGuard.Data.LanguageLoader;

namespace EyesGuard.Localization
{
    [MarkupExtensionReturnType(typeof(FlowDirection))]
    public class LocalizedFlowDirection : MarkupExtension
    {
        public FlowDirection FlowDirection { get; }

        [ConstructorArgument("locale")]
        public string Locale { get; }

        public LocalizedFlowDirection()
        {

            if (App.LocalizedEnvironment is LocalizedEnvironment env)
            {
                FlowDirection = App.CurrentLocale.TextInfo.IsRightToLeft ?
                    FlowDirection.RightToLeft :
                    FlowDirection.LeftToRight;
            }

            Locale = FsLanguageLoader.DefaultLocale;
        }

        public LocalizedFlowDirection(string locale)
        {
            FlowDirection =
                (new CultureInfo(locale)).TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerExtensions.IsRunningInVisualStudioDesigner)
            {
                return new CultureInfo(FsLanguageLoader.DefaultLocale).TextInfo.IsRightToLeft ?
                    (FlowDirection.RightToLeft) :
                    (FlowDirection.LeftToRight);
            }
            return FlowDirection;

        }
    }
}
