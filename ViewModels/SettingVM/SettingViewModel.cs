﻿using LibraryManagement.DTOs;
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
        public ICommand FirstLoadCM { get; set; }
        public ICommand OpenRoleSettingPageCM { get; set; }
        public ICommand OpenGeneralSettingPageCM { get; set; }
        public ICommand OpenReaderSettingPageCM { get; set; }
        public ICommand GeneralFirstLoadCM { get; set; }
        public ICommand SaveGeneralSettingCM { get; set; }
        public ICommand DisableRoleStackCM { get; set; }
        public ICommand EnableRoleStackCM { get; set; }

        bool isEditPermis = false;
        public static AccountDTO CurrentUser { get; set; }

        public SettingViewModel()
        {
            RoleList = new ObservableCollection<RoleDTO>(RoleService.Ins.GetAllRoles());

            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                if (CurrentUser.type.Name == "Employee")
                {
                    p.Content = new RoleSettingPage();
                }
                else if (CurrentUser.type.Name == "ReaderCard")
                {
                    p.Content = new ReaderSettingPage();
                }
            });
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
            OpenReaderSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ReaderSettingPage();
            });
            EnableRoleStackCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                p.IsEnabled = true;
                isEditPermis = true;
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
                {
                    PermissionList = new ObservableCollection<RoleDetailDTO>(SelectedRole.roleDetaislList);
                    p.IsEnabled = false;
                    isEditPermis = false;
                }
            });
            ApplyChangeRoleCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {

                if (isEditPermis)
                    ApplyChangeRoleFunc(p);
                isEditPermis = false;
            });
            OldPasswordChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                OldPass = p.Password;
            });
            NewPasswordChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPass = p.Password;
            });
            ReEnterPasswordChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ReEnterNewPass = p.Password;
            });
            ResetReaderPassCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(OldPass) || string.IsNullOrEmpty(NewPass) || string.IsNullOrEmpty(ReEnterNewPass))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                (AccountDTO user, string mes) = AuthService.Ins.Login(CurrentUser.username, OldPass);

                if (user is null)
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác");
                    return;
                }

                if (NewPass != ReEnterNewPass)
                {
                    MessageBox.Show("Mật khẩu mới không khớp. Vui lòng nhập lại");
                    return;
                }

                (bool isS, string mess) = AuthService.Ins.ResetPassword(CurrentUser.username, ReEnterNewPass);
                MessageBox.Show(mess);
                return;

            });
        }
    }
}
