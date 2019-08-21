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
    public class Translation
    {
        public Application Application { get; set; }
        public EyesGuard EyesGuard { get; set; }
        public Shellextensions ShellExtensions { get; set; }
    }
}
