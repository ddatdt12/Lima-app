using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Views.ReaderCard
{
    public partial class PrintReaderCardWindow : Window
    {
        public PrintReaderCardWindow()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-Vn");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Print, "ReaderCard");
                    this.Close();
                }
            }
            finally
            {
                this.IsEnabled = true;
                this.Close();
            }
        }
    }
}
