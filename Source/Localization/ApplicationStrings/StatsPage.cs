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
    public class StatsPage
    {
        public string Title { get; set; }
        public string ShortBreak { get; set; }
        public string LongBreak { get; set; }
        public string Pauses { get; set; }
        public string ShortCount { get; set; }
        public string LongCompletedCount { get; set; }
        public string LongFailedCount { get; set; }
        public string PauseCount { get; set; }
        public string StopCount { get; set; }
    }
}
