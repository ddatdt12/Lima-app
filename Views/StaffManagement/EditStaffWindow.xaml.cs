using System.Windows;

namespace LibraryManagement.Views.StaffManagement
{
    public partial class EditStaffWindow : Window
    {
        public EditStaffWindow()
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
