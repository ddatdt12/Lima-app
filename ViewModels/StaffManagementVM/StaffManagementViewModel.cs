using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.Views;
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
            });
            OpenEditStaffCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditStaffWindow editStaffWindow = new EditStaffWindow();
                LoadEditStaff(editStaffWindow);
                editStaffWindow.ShowDialog();
                ResetData();
            });
            CheckedSexCM = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
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
                        MessageBoxCustom mb = new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                    else
                    {
                        MessageBoxCustom mb = new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                }
                else
                {
                    MessageBoxCustom mb = new MessageBoxCustom("Lỗi", error, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
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
                        MessageBoxCustom mb = new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                    else
                    {
                        MessageBoxCustom mb = new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                }
                else
                {
                    MessageBoxCustom mb = new MessageBoxCustom("Lỗi", error, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });
            DeleteStaffCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (bool iss, string mes) = EmployeeService.Ins.DeleteEmployee(SelectedItem.id);
                if (iss)
                {
                    MessageBoxCustom mb = new MessageBoxCustom("Thông báo", mes, MessageType.Success, MessageButtons.OK);
                    mb.ShowDialog();
                    ListStaff.Remove(SelectedItem);
                }
                else
                {
                    MessageBoxCustom mb = new MessageBoxCustom("Lỗi", mes, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });
            OpenChangePasswordWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
                Password = null;
                RePassword = null;
                changePasswordWindow.ShowDialog();
            });
        }
        private (bool valid, string error) IsValidData()
        {

            if (string.IsNullOrEmpty(Name) || Sex is null || Birthday is null || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
            {
                return (false, "Thông tin nhân viên thiếu hoặc không hợp lệ! Vui lòng bổ sung");
            }

            if (!Helper.IsValidEmail(Email))
            {
                return (false, "Email không hợp lệ");
            }

            if (!Helper.IsPhoneNumber(Phone))
            {
                return (false, "Số điện thoại không hợp lệ");
            }

            return (true, null);
        }
    }
}
