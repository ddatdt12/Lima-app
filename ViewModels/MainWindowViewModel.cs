using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using LibraryManagement.ViewModels.SettingVM;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.Genre_AuthorManagement;
using LibraryManagement.Views.ImportBookPage;
using LibraryManagement.View.ReaderCard;
using LibraryManagement.Views.StaffManagement;
using LibraryManagement.Views.Login;
using LibraryManagement.Views.PunishBook;
using LibraryManagement.Views.RentBook;
using LibraryManagement.Views.ReturnBook;
using LibraryManagement.Views.SettingManagement;
using LibraryManagement.Views.StatisticalManagement;
using System.Windows;
using LibraryManagement.Views.Home;
using LibraryManagement.Views.HistoryManagement;
namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region
        public ICommand OpenReaderCardPageCM { get; set; }
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
        public ICommand OpenStaffManagementPageCM { get; set; }
        public ICommand OpenReaderCardPageCM { get; set; }
        public ICommand OpenBookManagementPageCM { get; set; }
        public ICommand OpenImportBookPage { get; set; }
        public ICommand FirstLoadCM { get; set; }
        public ICommand OpenHomePageCM { get; set; }
        public ICommand OpenHistoryPageCM { get; set; }
        #endregion

        public static AccountDTO CurrentUser { get; set; }

        public MainWindowViewModel()
        {
            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                if (CurrentUser.employee is null)
                    p.Content = new ReaderHome();
            });
            OpenHomePageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                if (CurrentUser.employee is null)
                    p.Content = new ReaderHome();
            });
            OpenReaderCardPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ReaderCardPage();
            });
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
                punishBookWindow.ShowDialog();
            });
            OpenImportBookPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainImportBookPage();
            });
            OpenStaffManagementPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new StaffManagementPage();
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
            try
            {
                var allData = BorrowingReturnService.Ins.GetBorrowingReturnCards();
                var allCardsByReturnDate = BorrowingReturnService.Ins.GetBorrowingReturnCards(returnDate: new DateTime(2022, 4, 9));
                var allCardsByBorrowingDate = BorrowingReturnService.Ins.GetBorrowingReturnCards(borrowingDate: new DateTime(2022, 4, 9));
            }
            catch (Exception e)
            {

            }
            OpenHistoryPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainHistoryPage();
            });
        }
    }
}
