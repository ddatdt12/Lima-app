
using LibraryManagement.ViewModels;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.ImportBookPage;
using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.SettingManagement;
using LibraryManagement.Views.StatisticalManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using LibraryManagement.Views.Genre_AuthorManagement;

namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand OpenGenreStatisticPageCM { get; set; }
        public ICommand OpenLateStatisticPageCM { get; set; }
        public ICommand OpenSettingPageCM { get; set; }



        public ICommand OpenImportBookPage { get; set; }
        public ICommand OpenBookManagementPageCM { get; set; }
        public ICommand OpenGenreAuthorManagementPage { get; set; }


        public MainWindowViewModel()
        {


            OpenImportBookPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainImportBookPage();
            });
            OpenBookManagementPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookManagementPage();
            });
            OpenGenreAuthorManagementPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainManagementPage();
            });
            OpenGenreStatisticPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new GenreStatisticPage();
            });
            OpenLateStatisticPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new LateStatisticPage();
            });
            OpenSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingPage();
            });


            //var role = new RoleDTO
            //{
            //    id = 2,
            //    position = "Thủ thư",
            //    roleDetaislList = new List<RoleDetailsDTO>
            //    {
            //        new RoleDetailsDTO
            //        {
            //        permission = 0,
            //        isPermitted = true
            //        },
            //         new RoleDetailsDTO
            //         {
            //        permission = 1,
            //        isPermitted = true
            //        },
            //        new RoleDetailsDTO{
            //        permission = 2,
            //        isPermitted = false
            //        },
            //                 new RoleDetailsDTO{
            //        permission = 3,
            //        isPermitted = true
            //        },
            //                    new RoleDetailsDTO{
            //        permission = 4,
            //        isPermitted = true
            //        },
            //                       new RoleDetailsDTO{
            //        permission = 5,
            //        isPermitted = false
            //        },

            //    },
            //};

            //(bool success, string message) = RoleService.Ins.CreateNewRole(role);
            //var roleList = RoleService.Ins.GetAllRoles();
        }


    }
}
