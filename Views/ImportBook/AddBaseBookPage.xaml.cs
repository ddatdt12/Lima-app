using System.Windows.Controls;

namespace LibraryManagement.Views.ImportBook
{
    public partial class AddBaseBookPage : Page
    {
        public static string PreEnterBaseBook;
        public AddBaseBookPage()
        {
            InitializeComponent();
            basebooktb.Text = PreEnterBaseBook;
        }
    }
}
