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
    public class TaskbarIcon
    {
        public string Protected { get; set; }
        public string NotProtected { get; set; }
        public string PausedProtected { get; set; }
        public Menu Menu { get; set; }
    }
}
