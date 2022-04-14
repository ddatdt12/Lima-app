using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LibraryManagement.Views.PunishBook
{
    public partial class PunishBookWindow : Window
    {
        public PunishBookWindow()
        {
            InitializeComponent();
        }
        private static readonly Regex _regex = new Regex("[^0-9]"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }

        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            TextBox sd = sender as TextBox;

            if (sd.Text.Length <= 0)
            {
                sd.Text = "0";
            }
        }

        private void TextBlock_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            TextBlock sd = sender as TextBlock;
            if (sd.Text == "") return;

            if (sd.Text[0].ToString() == "-")
            {
                sd.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
                sd.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
