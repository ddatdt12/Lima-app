using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private ObservableCollection<RoleDetailDTO> permissionList;
        public ObservableCollection<RoleDetailDTO> PermissionList
        {
            get { return permissionList; }
            set { permissionList = value; OnPropertyChanged(); }
        }

        private int NUMBER_OF_PERMISSION = RoleService.Ins.GetAllPermissions().Count;


        private RoleDTO selectedRole;
        public RoleDTO SelectedRole
        {
            get { return selectedRole; }
            set { selectedRole = value; OnPropertyChanged(); }
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

            try
            {
                (bool IsS, string mes) = RoleService.Ins.EditRolePermission(SelectedRole.id, new List<RoleDetailDTO>(PermissionList));
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

            if (SelectedRole.id == 1 || SelectedRole.id == 2 || SelectedRole.id == 3)
            {
                MessageBox.Show("Không thể xoá loại vai trò này");
                return;
            }

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
            RoleDTO role = new RoleDTO();

            role.name = TxtRole;
            role.roleDetaislList = new List<RoleDetailDTO>();
            for (int i = 1; i <= NUMBER_OF_PERMISSION; i++)
                role.roleDetaislList.Add(new RoleDetailDTO
                {
                    permissionId = i,
                    isPermitted = false
                });

            try
            {
                (bool success, string message) = RoleService.Ins.CreateNewRole(role);
                if (success)
                {
                    RoleList = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());
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
