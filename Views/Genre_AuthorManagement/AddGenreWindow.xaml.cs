using System.Windows;

namespace LibraryManagement.Views.Genre_AuthorManagement
{
    public partial class AddGenreWindow : Window
    {
        public AddGenreWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
