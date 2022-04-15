using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LibraryManagement.DTOs;
using LibraryManagement.Views.HistoryManagement;
using LibraryManagement.Services;
using LibraryManagement.Views.ImportBook;

namespace LibraryManagement.ViewModels.HistoryManagementVM
{
    public class HistoryViewModel : BaseViewModel
    {
        private ObservableCollection<ImportReceiptDTO> importReceiptList;
        public ObservableCollection<ImportReceiptDTO> ImportReceiptList
        {
            get { return importReceiptList; }
            set { importReceiptList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BorrowingCardDTO> borrowReturnList;
        public ObservableCollection<BorrowingCardDTO> BorrowReturnList
        {
            get { return borrowReturnList; }
            set { borrowReturnList = value; OnPropertyChanged(); }
        }

        private ImportReceiptDTO selectedReceipt;
        public ImportReceiptDTO SelectedReceipt
        {
            get { return selectedReceipt; }
            set { selectedReceipt = value; OnPropertyChanged(); }
        }

        private BorrowingCardDTO selectedBorrow;
        public BorrowingCardDTO SelectedBorrow
        {
            get { return selectedBorrow; }
            set { selectedBorrow = value; OnPropertyChanged(); }
        }


        private DateTime? selectedDate;
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; OnPropertyChanged(); }
        }



        public ICommand FirstLoadCM { get; set; }
        public ICommand ReceiptPageLoadedCM { get; set; }
        public ICommand BorrowReturnPageLoadedCM { get; set; }
        public ICommand OpenImportReceiptPageCM { get; set; }
        public ICommand OpenBorrowReturnPageCM { get; set; }
        public ICommand PrintReceiptCM { get; set; }

        public HistoryViewModel()
        {

            FirstLoadCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportReceiptPage();
            });
            ReceiptPageLoadedCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                ImportReceiptList = new ObservableCollection<ImportReceiptDTO>(ImportService.Ins.GetAllImportReceipt());
            });
            BorrowReturnPageLoadedCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                BorrowReturnList = new ObservableCollection<BorrowingCardDTO>(BorrowingReturnService.Ins.GetBorrowingReturnCards());
            });
            OpenImportReceiptPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportReceiptPage();
            });
            OpenBorrowReturnPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowReturnPage();
            });
            PrintReceiptCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedReceipt is null) return;
                PrintReceiptFunc();
                SelectedReceipt = null;
            });
        }


        public void PrintReceiptFunc()
        {
            ImportReceiptDTO rc = ImportService.Ins.GetImportReceiptDetail(SelectedReceipt.id);

            if (rc is null) return;

            PrintWindow w = new PrintWindow();
            w.supplier.Text = rc.supplier;
            w.rcId.Text = rc.id;
            w.date.Text = rc.createdAt.ToString("dd/MM/yyyy");
            w.lv.ItemsSource = rc.importReceiptDetailList;

            int total = 0;
            foreach (var item in rc.importReceiptDetailList)
            {
                item.unitTotal = item.unitPrice * item.quantity;
                total += item.unitTotal;
            }

            w.totalPrice.Text = total.ToString();
            w.ShowDialog();
        }
    }
}
