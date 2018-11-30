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
    public class HeaderMenu
    {
        public EyesguardMenu EyesGuard { get; set; }
        public ToolsMenu Tools { get; set; }
        public BreaksMenu Breaks { get; set; }
        public ViewMenu View { get; set; }
        public HelpMenu Help { get; set; }
    }
}
