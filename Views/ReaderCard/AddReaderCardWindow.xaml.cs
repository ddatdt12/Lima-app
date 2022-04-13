using System.Windows;

namespace LibraryManagement.View.ReaderCard
{
    public partial class AddReaderCardWindow : Window
    {
        public AddReaderCardWindow()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-Vn");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
