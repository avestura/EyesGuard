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
    public class TimeManipulation
    {
        public string HoursLimit { get; set; }
        public string MinutesLimit { get; set; }
        public string SecondsLimit { get; set; }
        public string SGapMorethanLGap { get; set; }
        public string SDurationMorethanLDuration { get; set; }
        public string SDurationTooHigh { get; set; }
        public string LDurationTooHigh { get; set; }
        public string NotEnoughGapBetweenLandS { get; set; }
        public string ShortGapLow { get; set; }
        public string LongGapLow { get; set; }
        public string ChooseLargerTime { get; set; }
    }
}
