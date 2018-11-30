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
    public class UserManager
    {
        public string User { get; set; }
        public string NotLoggedIn { get; set; }
        public string NoTitle { get; set; }
        public string Login { get; set; }
        public string Profile { get; set; }
        public string Logout { get; set; }
    }
}
