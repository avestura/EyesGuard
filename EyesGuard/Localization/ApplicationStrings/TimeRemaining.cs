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
    public class TimeRemaining
    {
        public ShortBreak ShortBreak { get; set; }
        public LongBreak LongBreak { get; set; }
        public PauseTime PauseTime { get; set; }
    }
}
