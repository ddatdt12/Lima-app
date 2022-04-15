using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.Views.StaffManagement;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
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

        public static AccountDTO CurrentUser { get; set; }

        public ICommand OpenAddStaffWindowCM { get; set; }
        public ICommand DeleteStaffCM { get; set; }
        public ICommand ExportToExcel { get; set; }
        public ICommand FirstLoadCM { get; set; }

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

            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ListStaff = new ObservableCollection<EmployeeDTO>(EmployeeService.Ins.GetAllEmployees());
                ListRole = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());

                for (int i = 0; i < ListRole.Count; i++)
                    if (ListRole[i].name == "Độc giả")
                        ListRole.Remove(ListRole[i]);

                StartDate = DateTime.Now;
            });
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
                ExportToExcelFunc();
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
                        System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        System.Windows.MessageBox.Show(message, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(error, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            DeleteStaffCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                System.Windows.Forms.DialogResult dlg = (System.Windows.Forms.DialogResult)System.Windows.MessageBox.Show("Bạn muốn xóa dữ liệu này không ? Dữ diệu sẽ không phục hồi được sau khi xóa.", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (dlg == System.Windows.Forms.DialogResult.Yes)
                {
                    (bool iss, string mes) = EmployeeService.Ins.DeleteEmployee(SelectedItem.id);
                    if (iss)
                    {
                        ListStaff.Remove(SelectedItem);
                        System.Windows.MessageBox.Show(mes, "Thông báo", MessageBoxButton.OK);
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    System.Windows.MessageBox.Show("Mật khẩu không trùng khớp, vui lòng nhập đúng!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow();
                var id = SelectedItem.accountId;
                var newPass = Password;
                (bool iss, string mes) = EmployeeService.Ins.UpdatePasswordOfEmployee(id, newPass);
                if (iss)
                {
                    System.Windows.MessageBox.Show(mes, "Thông báo", MessageBoxButton.OK);
                    p.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

            if (DateTime.Now.Year - Birthday.Value.Year < 18)
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

        public void ExportToExcelFunc()
        {
            if (ListStaff.Count == 0) return;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = false;
                    Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

                    ws.Cells[1, 1] = "Thư viện Lima";
                    ws.Cells[2, 1] = "Ngày xuất: " + DateTime.Now.ToString("dd/MM/yyyy");
                    ws.Cells[3, 1] = "Tổng nhân sự: " + ListStaff.Count;

                    ws.Cells[5, 1] = "Mã nhân viên";
                    ws.Cells[5, 2] = "Họ tên";
                    ws.Cells[5, 3] = "Ngày sinh";
                    ws.Cells[5, 4] = "Điện thoại";
                    ws.Cells[5, 5] = "Giới tính";
                    ws.Cells[5, 6] = "Ngày bắt đầu";
                    ws.Cells[5, 7] = "Vai trò";

                    int i2 = 7;
                    foreach (var item in ListStaff)
                    {

                        ws.Cells[i2, 1] = item.id;
                        ws.Cells[i2, 2] = item.name;
                        ws.Cells[i2, 3] = item.birthDate.Value.ToString("dd/MM/yyyy");
                        ws.Cells[i2, 4] = item.phoneNumber;
                        ws.Cells[i2, 5] = item.gender;
                        ws.Cells[i2, 6] = item.startingDate.Value.ToString("dd/MM/yyyy");
                        ws.Cells[i2, 7] = item.account.role.name;

                        i2++;
                    }
                    ws.SaveAs(sfd.FileName);
                    wb.Close();
                    app.Quit();

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                    System.Windows.MessageBox.Show("Xuất file thành công");
                }
            }
        }
    }
}
