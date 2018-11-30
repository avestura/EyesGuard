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
    public class EyesGuard
    {
        public ControlPanel ControlPanel { get; set; }
        public GuardStatus GuardStatus { get; set; }
        public TimeRemaining TimeRemaining { get; set; }
        public CustomPause CustomPause { get; set; }
        public WarningPage WarningPage { get; set; }
        public string OperationFailed { get; set; }
        public string CheckInput { get; set; }
        public TimeManipulation TimeManipulation { get; set; }
        public Settings Settings { get; set; }
        public string Resting { get; set; }
        public string Waiting { get; set; }
        public string[] ShortMessageSuggestions { get; set; }
        public string LongWindowMessage { get; set; }
        public string LongWindowCancelButton { get; set; }
        public EyesGuardNotifications Notifications { get; set; }
        public string WaitUnitlEndOfBreak { get; set; }
        public string LongBreakTimeRemaining { get; set; }
        public StatsPage StatsPage { get; set; }
        public AlertPages AlertPages { get; set; }
        public HeaderMenu HeaderMenu { get; set; }
        public UserManager UserManager { get; set; }
    }
}
