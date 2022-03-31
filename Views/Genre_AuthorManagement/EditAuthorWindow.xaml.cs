using System.Windows;
using System.Windows.Markup;

namespace LibraryManagement.Views.Genre_AuthorManagement
{
    public partial class EditAuthorWindow : Window
    {
        public EditAuthorWindow()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("vi-VN");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
