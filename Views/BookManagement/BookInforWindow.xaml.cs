using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.Views.BookManagement
{
    public partial class BookInforWindow : Window
    {
        public BookInforWindow()
        {
            InitializeComponent();
        }

        private static readonly Regex _regex = new Regex("[^0-9]"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
