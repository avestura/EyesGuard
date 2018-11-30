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
    public class TimeSettings
    {
        public string Title { get; set; }
        public string GapBetweenTwoShortBreak { get; set; }
        public string GapBetweenTwoLongBreak { get; set; }
        public string ShortBreakDuration { get; set; }
        public string LongBreakDuration { get; set; }
    }
}
