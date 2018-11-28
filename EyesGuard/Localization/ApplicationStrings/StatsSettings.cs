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
    public class StatsSettings
    {
        public string Title { get; set; }
        public string YesDeleteData { get; set; }
        public string NoKeepData { get; set; }
        public string StatsDeleted { get; set; }
        public string StoreStats { get; set; }
        public string StoreStatsToolTip { get; set; }
        public string ClearStatsText { get; set; }
        public string ClearStats { get; set; }
    }
}
