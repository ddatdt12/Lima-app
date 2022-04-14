using System.Windows.Controls;

namespace LibraryManagement.Views.SettingManagement
{
    public partial class RoleSettingPage : Page
    {
        public RoleSettingPage()
        {
            InitializeComponent();
        }

        private void rolelistview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roleChkbox != null)
                roleChkbox.IsEnabled = false;
        }
    }
}
