using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.Views.ImportBook
{
    public partial class AddBookPage : Page
    {
        public AddBookPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static readonly Regex _regex = new Regex("[^0-9]"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sd = sender as TextBox;

            if (sd.Text.Length <= 0)
                sd.Text = "1";
        }
    }
}
