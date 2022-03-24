using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {
        private ObservableCollection<RoleDTO> roleList;
        public ObservableCollection<RoleDTO> RoleList
        {
            get { return roleList; }
            set { roleList = value; OnPropertyChanged(); }
        }

        private RoleDTO selectedRole;
        public RoleDTO SelectedRole
        {
            get { return selectedRole; }
            set { selectedRole = value; OnPropertyChanged(); }
        }

        private bool isBookManagement;
        public bool IsBookManagement
        {
            get { return isBookManagement; }
            set { isBookManagement = value; OnPropertyChanged(); }
        }

        private bool isImportBook;
        public bool IsImportBook
        {
            get { return isImportBook; }
            set { isImportBook = value; OnPropertyChanged(); }
        }

        private bool isReaderManagement;
        public bool IsReaderManagement
        {
            get { return isReaderManagement; }
            set { isReaderManagement = value; OnPropertyChanged(); }
        }

        private bool isStaffManagement;
        public bool IsStaffManagement
        {
            get { return isStaffManagement; }
            set { isStaffManagement = value; OnPropertyChanged(); }
        }

        private bool isStatictis;
        public bool IsStatictis
        {
            get { return isStatictis; }
            set { isStatictis = value; OnPropertyChanged(); }
        }

        private bool isSetting;
        public bool IsSetting
        {
            get { return isSetting; }
            set { isSetting = value; OnPropertyChanged(); }
        }

        private string txtRole;
        public string TxtRole
        {
            get { return txtRole; }
            set { txtRole = value; OnPropertyChanged(); }
        }


        public ICommand DeleteRoleCM { get; set; }
        public ICommand OpenAddRoleCM { get; set; }
        public ICommand AddRoleCM { get; set; }
        public ICommand ApplyChangeRoleCM { get; set; }

        public void ApplyChangeRoleFunc(StackPanel p)
        {
            if (SelectedRole is null) return;


            List<RoleDetailsDTO> listDetail = new List<RoleDetailsDTO>
            {
                 new RoleDetailsDTO
                        {
                        permission = 0,
                        isPermitted = IsBookManagement
                        },
                         new RoleDetailsDTO
                         {
                        permission = 1,
                        isPermitted = IsImportBook
                        },
                        new RoleDetailsDTO{
                        permission = 2,
                        isPermitted = IsReaderManagement
                        },
                        new RoleDetailsDTO{
                        permission = 3,
                        isPermitted = IsStatictis
                        },
                        new RoleDetailsDTO{
                        permission = 4,
                        isPermitted = IsStaffManagement
                        },
                        new RoleDetailsDTO{
                        permission = 5,
                        isPermitted = IsSetting
                        }
            };

            try
            {
                (bool IsS, string mes) = RoleService.Ins.EditRolePermission(SelectedRole.id, listDetail);
                if (IsS)
                {
                    RoleList = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());
                    p.IsEnabled = false;
                }

                MessageBox.Show(mes);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


        }
        public void DeleteRoleFunc()
        {
            if (SelectedRole is null) return;

            if (MessageBox.Show("Bạn có muốn xoá vai trò này không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    (bool isS, string mes) = RoleService.Ins.DeleteRole(SelectedRole.id);
                    if (isS)
                    {
                        RoleList.Remove(SelectedRole);
                    }
                    MessageBox.Show(mes);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);

                }
            }



        }
        public void AddRoleFunc(Window p)
        {
            RoleDTO role = new RoleDTO
            {
                position = TxtRole,
                roleDetaislList = new List<RoleDetailsDTO>
                {
                    new RoleDetailsDTO
                    {
                    permission = 0,
                    isPermitted = false
                    },
                     new RoleDetailsDTO
                     {
                    permission = 1,
                    isPermitted = false
                    },
                    new RoleDetailsDTO{
                    permission = 2,
                    isPermitted = false
                    },
                             new RoleDetailsDTO{
                    permission = 3,
                    isPermitted = false
                    },
                                new RoleDetailsDTO{
                    permission = 4,
                    isPermitted = false
                    },
                                   new RoleDetailsDTO{
                    permission = 5,
                    isPermitted = false
                    },

                },
            };
            try
            {
                (bool success, string message) = RoleService.Ins.CreateNewRole(role);
                if (success)
                {
                    RoleList.Add(role);
                    MessageBox.Show(message);
                    p.Close();
                    return;
                }
                MessageBox.Show(message);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

        }

    }
}
