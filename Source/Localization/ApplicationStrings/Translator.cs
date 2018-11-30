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
    public class Translator
    {
        public string Name { get; set; }
        public string GitHubUsername { get; set; }
        public string Website { get; set; }
        public string Notes { get; set; }
    }
}
