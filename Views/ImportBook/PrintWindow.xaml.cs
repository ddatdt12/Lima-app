using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Views.ImportBook
{
    public partial class PrintWindow : Window
    {
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Print, "ImportReceipt");
                    this.Close();
                }
            }
            finally
            {
                this.Close();
            }
        }
    }
}
