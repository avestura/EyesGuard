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
    public class Menu
    {
        public string PauseFor { get; set; }
        public string PausedFor { get; set; }
        public string NextShortBreak { get; set; }
        public string NextLongBreak { get; set; }
        public string ShowMainMenu { get; set; }
        public string StartProtection { get; set; }
        public string StopProtection { get; set; }
        public string FiveMins { get; set; }
        public string TenMins { get; set; }
        public string ThirtyMins { get; set; }
        public string OneHour { get; set; }
        public string TwoHours { get; set; }
        public string Custom { get; set; }
        public string Settings { get; set; }
        public string Exit { get; set; }
    }
}
