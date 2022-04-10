using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.Views.ImportBook
{

    public partial class ImportBookWindow : Window
    {
        public static string PreEnterBaseBook;

        public ImportBookWindow()
        {
            InitializeComponent();
            AddBaseBookPage.PreEnterBaseBook = PreEnterBaseBook;
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sd = sender as TextBox;

            if (sd.Text.Length <= 0)
                sd.Text = "1";
        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            headertxt.Text = "Thêm sách";
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            headertxt.Text = "Thêm đầu sách";
        }
    }
}
