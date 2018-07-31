using EyesGuard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard.AppManagers
{
    public class UIViewModels
    {
        public ShortLongBreakTimeRemainingViewModel ShortLongBreakTimeRemaining { get; set; }
            = new ShortLongBreakTimeRemainingViewModel();

        public HeaderMenuViewModel HeaderMenu { get; set; } = new HeaderMenuViewModel();

        public NotifyIconViewModel NotifyIcon { get; set; } = new NotifyIconViewModel();

        public ShortBreakViewModel ShortBreak { get; set; } = new ShortBreakViewModel();

        public LongBreakWindowViewModel LongBreak { get; set; }
            = new LongBreakWindowViewModel();

        public StatsViewModel Stats { get; set; } = new StatsViewModel();

    }
}
