using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EyesGuard
{
    public partial class App
    {
        private static bool _isProtectionPaused = false;
        private const int EyesGuardIdleDetectionThreshold = 70;

        /// <summary>
        /// Used in <see cref="App.GetShortWindowMessage()"/> to get appropriate message from string resources
        /// </summary>
        private static int ShortMessageIteration = 0;

        /// <summary>
        /// Number of application process. Used to run only one instance
        /// </summary>
        private static readonly int programInstancesCount =
            Process.GetProcessesByName(
                Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)
            ).Length;
    }
}
