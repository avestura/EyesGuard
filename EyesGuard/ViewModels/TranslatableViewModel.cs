using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.ViewModels
{
    public abstract class TranslatableViewModel
    {
        public Localization.Meta Meta => App.LocalizedEnvironment.Meta;
        public Localization.Translation Translation => App.LocalizedEnvironment.Translation;

    }
}
