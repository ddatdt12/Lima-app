using LibraryManagement.DTOs;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagement.Views.StaffManagement
{
    public partial class StaffManagementPage : Page
    {
        public StaffManagementPage()
        {
            InitializeComponent();
        }

        private void SearchTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvStaff.ItemsSource);
            view.Filter = Filter;
            CollectionViewSource.GetDefaultView(lvStaff.ItemsSource).Refresh();
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(SearchTbx.Text))
                return true;

            switch (FilterCbb.SelectedValue)
            {
                case "Mã nhân viên":
                    return ((item as EmployeeDTO).id.ToString().IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tên nhân viên":
                    return ((item as EmployeeDTO).name.IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Vai trò":
                    return ((item as EmployeeDTO).account.role.name.IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    return ((item as EmployeeDTO).id.ToString().IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
    }
}
