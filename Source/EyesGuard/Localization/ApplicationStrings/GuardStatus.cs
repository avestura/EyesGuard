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
    public class GuardStatus
    {
        public string Running { get; set; }
        public string Paused { get; set; }
        public string Stopped { get; set; }
        public string Idle { get; set; }
    }
}
