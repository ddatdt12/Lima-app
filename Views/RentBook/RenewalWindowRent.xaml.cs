using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.Views.RentBook
{
    public partial class RenewalWindowRent : Window
    {
        public RenewalWindowRent()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Nếu thẻ còn hạn, ngày hết hạn mới sẽ được tính từ ngày hết hạn của đợt gia hạn trước \nNếu thẻ quá hạn, ngày hết hạn mới sẽ được tính từ ngày hiện tại", "Quy định", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
