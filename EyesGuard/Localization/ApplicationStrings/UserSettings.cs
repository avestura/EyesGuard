using EyesGuard.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;

namespace EyesGuard.Localization.ApplicationStrings
{
    public class UserSettings
    {
        public string Title { get; set; }
        public string ForceUser { get; set; }
        public string ForceUserToolTip { get; set; }
        public string OnlyOneShortBreak { get; set; }
        public string OnlyOneShortBreakToolTip { get; set; }
        public string StartupApplication { get; set; }
        public string StartupApplicationToolTip { get; set; }
        public string AlertBeforeLongBreak { get; set; }
        public string AlertBeforeLongBreakToolTip { get; set; }
        public string SystemIdle { get; set; }
        public string[] SystemIdleToolTip { get; set; }

        [JsonIgnore]
        public string SystemIdleToolTipJoined => string.Join("\n", SystemIdleToolTip);
    }
}
