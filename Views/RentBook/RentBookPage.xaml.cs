using System.Windows.Controls;
using System.Windows.Markup;

namespace LibraryManagement.Views.RentBook
{
    public partial class RentBookPage : Page
    {
        public RentBookPage()
        {
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage("vi-VN");
        }
    }
}
