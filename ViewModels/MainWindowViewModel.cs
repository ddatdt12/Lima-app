using LibraryManagement.DTOs;
using LibraryManagement.ViewModels;
using LibraryManagement.ViewModels.SettingVM;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.Genre_AuthorManagement;
using LibraryManagement.Views.ImportBookPage;
using LibraryManagement.Views.Login;
using LibraryManagement.Views.PunishBook;
using LibraryManagement.Views.RentBook;
using LibraryManagement.Views.ReturnBook;
using LibraryManagement.Views.SettingManagement;
using LibraryManagement.Views.StatisticalManagement;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand OpenBookManagementPageCM { get; set; }
        public ICommand OpenImportBookPage { get; set; }
        public ICommand OpenGenreAuthorManagementPage { get; set; }
        public ICommand OpenGenreStatisticPageCM { get; set; }
        public ICommand OpenLateStatisticPageCM { get; set; }
        public ICommand OpenSettingPageCM { get; set; }
        public ICommand SignOutCM { get; set; }
        public ICommand OpenRentBookPageCM { get; set; }
        public ICommand OpenReturnBookPageCM { get; set; }
        public ICommand OpenPunishBookWindowCM { get; set; }

        public static AccountDTO CurrentUser { get; set; }

        public MainWindowViewModel()
        {
            OpenBookManagementPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookManagementPage();
            });
            OpenRentBookPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new RentBookPage();
            });
            OpenReturnBookPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ReturnBookPage();
            });
            OpenPunishBookWindowCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                var punishBookWindow = new PunishBookWindow();
                punishBookWindow.Show();
            });
            OpenImportBookPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainImportBookPage();
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
                SettingViewModel.CurrentUser = CurrentUser;
                p.Content = new MainSettingPage();
            });
            SignOutCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                LoginWindow w = new LoginWindow();
                w.Show();
                p.Close();

            });
        }

    }
}
