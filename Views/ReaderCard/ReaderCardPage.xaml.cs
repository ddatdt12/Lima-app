using LibraryManagement.Views.ReaderCard;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddReaderTypeWindow addReaderTypeWindow = new AddReaderTypeWindow();
            addReaderTypeWindow.ShowDialog();
        }
    }
}
