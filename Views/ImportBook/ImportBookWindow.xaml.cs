using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagement.Views.ImportBook
{

    public partial class ImportBookWindow : Window
    {
        public ImportBookWindow()
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
