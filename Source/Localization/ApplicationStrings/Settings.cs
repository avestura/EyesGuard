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
    public class Settings
    {
        public string Title { get; set; }
        public string SavedSuccessfully { get; set; }
        public string AreYouSure { get; set; }
        public string LanguageSection { get; set; }
        public TimeSeparators TimeSeparators { get; set; }
        public string SaveSettings { get; set; }
        public TimeSettings TimeSettings { get; set; }
        public UserSettings UserSettings { get; set; }
        public StatsSettings StatsSettings { get; set; }
        public LanguageSettings LanguageSettings { get; set; }
    }
}
