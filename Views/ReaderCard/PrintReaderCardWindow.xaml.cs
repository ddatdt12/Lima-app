using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryManagement.Views.ReaderCard
{
    /// <summary>
    /// Interaction logic for PrintReaderCardWindow.xaml
    /// </summary>
    public partial class PrintReaderCardWindow : Window
    {
        public PrintReaderCardWindow()
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
