using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static EyesGuard.App;

namespace EyesGuard.Resources.Controls
{
    public class GuardButton : Button
    {


        public GuardStates GuardState
        {
            get { return (GuardStates)GetValue(GuardStateProperty); }
            set { SetValue(GuardStateProperty, value); }
        }
        public static readonly DependencyProperty GuardStateProperty =
            DependencyProperty.Register("GuardState", typeof(GuardStates), typeof(GuardButton), new PropertyMetadata(GuardStates.Protecting));


    }
}
