using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyesGuard.Extensions
{
    public static class DesignerExtensions
    {
        public static bool IsRunningInVisualStudioDesigner
        {
            get
            {
                // Are we looking at this dialog in the Visual Studio Designer or Blend?
                string appname = System.Reflection.Assembly.GetEntryAssembly().FullName;
                return appname.Contains("XDesProc") || DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}
