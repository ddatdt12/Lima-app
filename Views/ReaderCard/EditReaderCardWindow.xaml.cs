using System.Windows;

namespace LibraryManagement.Views.ReaderCard
{
    public partial class EditReaderCardWindow : Window
    {
        public EditReaderCardWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
