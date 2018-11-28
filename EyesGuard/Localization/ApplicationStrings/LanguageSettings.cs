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
    public class LanguageSettings
    {
        public string Title { get; set; }
        public string SelectLanguage { get; set; }
        public string MaintainMessage { get; set; }
        public string RestartRequired { get; set; }
        public string MessagesFromLangFile { get; set; }
        public string MessagesFromLangFileToolTip { get; set; }
        public string AddMessage { get; set; }
        public string RemoveMessage { get; set; }
        public string NoAccount { get; set; }
        public string NoWebsite { get; set; }
        public string NoNotes { get; set; }
        public string EnterNewMessage { get; set; }
        public string Submit { get; set; }
    }
}
