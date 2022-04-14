using System.Windows;

namespace LibraryManagement.Views.Genre_AuthorManagement
{
    public partial class EditGenreWindow : Window
    {
        public EditGenreWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
