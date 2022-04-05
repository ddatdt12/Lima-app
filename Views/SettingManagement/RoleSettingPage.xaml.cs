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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagement.Views.SettingManagement
{
    /// <summary>
    /// Interaction logic for RoleSettingPage.xaml
    /// </summary>
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
