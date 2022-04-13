using LibraryManagement.Views.StaffManagement;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        public ICommand OpenEditStaffCM { get; set; }
        public ICommand UpdateStaffCM { get; set; }

        public void LoadEditStaff(EditStaffWindow w)
        {
            Name = SelectedItem.name;
            if (SelectedItem.gender == "Nam")
            {
                w.Man.IsChecked = true;
            }
            else
            {
                w.Woman.IsChecked = true;
            }
            Sex = SelectedItem.gender;
            Birthday = SelectedItem.birthDate;
            Email = SelectedItem.email;
            Phone = SelectedItem.phoneNumber;
            StartDate = SelectedItem.startingDate;
            Username = SelectedItem.account.username;
            Password = SelectedItem.account.password;
            Role = SelectedItem.account.role.name;
            w._Matkhau.Password = SelectedItem.account.password;
        }
    }
}
