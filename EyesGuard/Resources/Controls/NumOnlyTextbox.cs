using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EyesGuard.Resources.Controls
{
    public class NumOnlyTextbox : TextBox
    {
        Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text


        public NumOnlyTextbox() : base()
        {
            CommandManager.AddPreviewExecutedHandler(this, textBox_PreviewExecuted);
            ContextMenu = null;
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            if (regex.IsMatch(e.Text) || e.Text == " ") e.Handled = true;
            base.OnTextInput(e);
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (Text.Contains(" ") || regex.IsMatch(Text)) e.Handled = true;
            base.OnTextChanged(e);
            if (Text == "") Text = "0";
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
            base.OnPreviewKeyDown(e);
        }



    }
}
