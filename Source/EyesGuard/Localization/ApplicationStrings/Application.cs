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
    public class Application
    {
        public string OSTitle { get; set; }
        public string HeaderTitle { get; set; }
        public string LoginWarning { get; set; }
        public string HelpPageText { get; set; }
        public string DoNotRunMultipleInstances { get; set; }

        public ApplicationNotifications Notifications { get; set; }

        public Resources Resources { get; set; }
        public About About { get; set; }
        public Donate Donate { get; set; }
        public Feedback Feedback { get; set; }
    }
}
