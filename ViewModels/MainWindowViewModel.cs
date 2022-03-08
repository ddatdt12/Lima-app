using LibraryManagement.ViewModels;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.ImportBookPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand OpenImportBookPage { get; set; }
        public ICommand OpenBookManagementPageCM { get; set; }


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
        }


    }
}
