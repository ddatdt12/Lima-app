using System.Windows;
using System.Windows.Controls;

namespace LibraryManagement.Views.PunishBook
{
    public partial class PrintWindow : Window
    {
        public PrintWindow()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(Print, "FineReceipt");
                    this.Close();
                    MessageBox.Show("In thành công", "Thông báo", MessageBoxButton.OK);
                }
            }
            finally
            {
                this.Close();
            }
        }
    }
}
