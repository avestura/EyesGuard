using EyesGuard.Localization.ApplicationStrings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

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
            if (App.LocalizedEnvironment is LocalizedEnvironment env) {
                FlowDirection = env.Meta.CurrentCulture.TextInfo.IsRightToLeft ?
                    FlowDirection.RightToLeft :
                    FlowDirection.LeftToRight;
            }

            Locale = LanguageLoader.DefaultLocale;
        }

        public LocalizedFlowDirection(string locale)
        {
            FlowDirection =
                (new CultureInfo(locale)).TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                return new CultureInfo(LanguageLoader.DefaultLocale).TextInfo.IsRightToLeft ?
                    (FlowDirection.RightToLeft) :
                    (FlowDirection.LeftToRight);
            }
            return FlowDirection;
        }
    }
}
