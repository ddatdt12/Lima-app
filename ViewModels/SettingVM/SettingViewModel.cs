using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Views.SettingManagement;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {

        public ICommand OpenRoleSettingPageCM { get; set; }
        public ICommand OpenGeneralSettingPageCM { get; set; }
        public ICommand GeneralFirstLoadCM { get; set; }
        public ICommand SaveGeneralSettingCM { get; set; }
        public ICommand DisableRoleStackCM { get; set; }
        public ICommand EnableRoleStackCM { get; set; }

        public SettingViewModel()
        {
            RoleList = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());


            GeneralFirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GeneralFirstLoadFunc();
            });
            OpenRoleSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new RoleSettingPage();
            });
            OpenGeneralSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new GeneralSettingPage();
            });
            EnableRoleStackCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                p.IsEnabled = true;
            });
            OpenAddRoleCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddRoleWindow w = new AddRoleWindow();
                w.ShowDialog();
            });
            AddRoleCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                AddRoleFunc(p);
            });
            DeleteRoleCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteRoleFunc();
            });
            SaveGeneralSettingCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveGeneralSettingFunc();
            });
            DisableRoleStackCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                if (SelectedRole is null) return;

                if (p != null)
                    p.IsEnabled = false;

                IsBookManagement = SelectedRole.roleDetaislList[0].isPermitted;
                IsImportBook = SelectedRole.roleDetaislList[1].isPermitted;
                IsReaderManagement = SelectedRole.roleDetaislList[2].isPermitted;
                IsStaffManagement = SelectedRole.roleDetaislList[3].isPermitted;
                IsStatictis = SelectedRole.roleDetaislList[4].isPermitted;
                IsSetting = SelectedRole.roleDetaislList[5].isPermitted;
            });
            ApplyChangeRoleCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                ApplyChangeRoleFunc(p);
            });
        }
    }
}
