using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.View.ReaderCard
{
    /// <summary>
    /// Interaction logic for ReaderCardPage.xaml
    /// </summary>
    public partial class ReaderCardPage : Page
    {
        public ReaderCardPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddReaderCardWindow addReaderCardWindow = new AddReaderCardWindow();
            addReaderCardWindow.ShowDialog();
        }
    }
}
