using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.Views.StaffManagement;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        private ObservableCollection<EmployeeDTO> _ListStaff;
        public ObservableCollection<EmployeeDTO> ListStaff
        {
            get { return _ListStaff; }
            set { _ListStaff = value; OnPropertyChanged(); }
        }

        private ObservableCollection<RoleDTO> _ListRole;
        public ObservableCollection<RoleDTO> ListRole
        {
            get { return _ListRole; }
            set { _ListRole = value; OnPropertyChanged(); }
        }

        private EmployeeDTO _SelectedItem;

        public EmployeeDTO SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }


        public ICommand OpenAddStaffWindowCM { get; set; }
        public ICommand DeleteStaffCM { get; set; }
        public ICommand ExportToExcel { get; set; }

        public void ResetData()
        {
            SelectedItem = null;
            Name = null;
            Birthday = null;
            Email = null;
            Phone = null;
            Role = null;
            StartDate = null;
            Username = null;
            Password = null;
            Sex = null;
        }

        public StaffManagementViewModel()
        {
            ListStaff = new ObservableCollection<EmployeeDTO>(EmployeeService.Ins.GetAllEmployees());
            ListRole = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());
            StartDate = DateTime.Now;
            OpenAddStaffWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddStaffWindow addStaffWindow = new AddStaffWindow();
                addStaffWindow.ShowDialog();
                ResetData();
            });
            OpenEditStaffCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditStaffWindow editStaffWindow = new EditStaffWindow();
                LoadEditStaff(editStaffWindow);
                editStaffWindow.ShowDialog();
                ResetData();
            });
            CheckedSexCM = new RelayCommand<System.Windows.Controls.RadioButton>((p) => { return true; }, (p) =>
            {
                Sex = p.Content.ToString();
            });
            GetPasswordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });
            GetRePasswordCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                RePassword = p.Password;
            });
            ExportToExcel = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
            });
            AddStaffCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                (bool isvalid, string error) = IsValidData();
                if (isvalid)
                {
                    EmployeeDTO employee = new EmployeeDTO()
                    {
                        name = Name,
                        email = Email,
                        gender = Sex,
                        phoneNumber = Phone,
                        startingDate = StartDate,
                        account = new AccountDTO()
                        {
                            username = Username,
                            password = Password,
                            roleId = (ListRole.FirstOrDefault(s => s.name == Role)).id,
                        },
                        birthDate = (DateTime)Birthday,
                    };

                    (bool Successful, string message) = EmployeeService.Ins.CreateNewEmployee(employee);

                    if (Successful)
                    {
                        ListStaff.Add(employee);
                        p.Close();
                        MessageBox.Show(message, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            UpdateStaffCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                (bool isvalid, string error) = IsValidData();
                if (isvalid)
                {
                    EmployeeDTO employee = new EmployeeDTO()
                    {
                        id = SelectedItem.id,
                        name = Name,
                        email = Email,
                        gender = Sex,
                        phoneNumber = Phone,
                        startingDate = StartDate,
                        accountId = SelectedItem.accountId,
                        account = new AccountDTO()
                        {
                            username = Username,
                            password = Password,
                            roleId = (ListRole.FirstOrDefault(s => s.name == Role)).id,
                        },
                        birthDate = (DateTime)Birthday,
                    };

                    (bool Successful, string message) = EmployeeService.Ins.UpdateEmployee(employee);

                    if (Successful)
                    {
                        var readerCardFound = ListStaff.FirstOrDefault(s => s.id == employee.id);
                        ListStaff[ListStaff.IndexOf(readerCardFound)] = employee;
                        p.Close();
                        MessageBox.Show(message, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            DeleteStaffCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                System.Windows.Forms.DialogResult dlg = (System.Windows.Forms.DialogResult)MessageBox.Show("Bạn muốn xóa dữ liệu này không ? Dữ diệu sẽ không phục hồi được sau khi xóa.", "Thông báo", MessageBoxButton.YesNo,MessageBoxImage.Information);
                if(dlg == System.Windows.Forms.DialogResult.Yes)
                {
                    (bool iss, string mes) = EmployeeService.Ins.DeleteEmployee(SelectedItem.id);
                    if (iss)
                    {
                        ListStaff.Remove(SelectedItem);
                        MessageBox.Show(mes, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }    
            });
            OpenChangePasswordWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
                Password = null;
                RePassword = null;
                changePasswordWindow.ShowDialog();
            });
            ChangePassCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (Password != RePassword)
                {
                    MessageBox.Show("Mật khẩu không trùng khớp, vui lòng nhập đúng!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
                var id = SelectedItem.accountId;
                var newPass = Password;
                (bool iss, string mes) = EmployeeService.Ins.UpdatePasswordOfEmployee(id, newPass);
                if (iss)
                {
                    MessageBox.Show(mes, "Thông báo", MessageBoxButton.OK);
                    p.Close();
                }
                else
                {
                    MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            });
        }
        private (bool valid, string error) IsValidData()
        {

            if (string.IsNullOrEmpty(Name) || Sex is null || Birthday is null || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
            {
                return (false, "Thông tin nhân viên thiếu hoặc không hợp lệ! Vui lòng bổ sung");
            }

            if(DateTime.Now.Year - Birthday.Value.Year < 18)
            {
                return (false, "Nhân viên chưa đủ 18 tuổi");
            }
            if (!Helper.IsPhoneNumber(Phone))
            {
                return (false, "Số điện thoại không hợp lệ");
            }

            if (!Helper.IsValidEmail(Email))
            {
                return (false, "Email không hợp lệ");
            }

            return (true, null);
        }
    }
}
