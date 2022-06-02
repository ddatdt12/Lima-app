using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.ObjectModel;
using LibraryManagement.DTOs;
using LibraryManagement.Views.HistoryManagement;
using LibraryManagement.Services;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Markup;
using LibraryManagement.Views.PunishBook;

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

        private ObservableCollection<FineReceiptDTO> fineReceiptList;
        public ObservableCollection<FineReceiptDTO> FineReceiptList
        {
            get { return fineReceiptList; }
            set { fineReceiptList = value; OnPropertyChanged(); }
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

        private FineReceiptDTO _SelectedFine;
        public FineReceiptDTO SelectedFine
        {
            get { return _SelectedFine; }
            set { _SelectedFine = value; OnPropertyChanged(); }
        }


        private DateTime? selectedDate;
        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set { selectedDate = value; OnPropertyChanged(); }
        }

        private ComboBoxItem selectedFilter;
        public ComboBoxItem SelectedFilter
        {
            get { return selectedFilter; }
            set { selectedFilter = value; OnPropertyChanged(); }
        }

        public ICommand FirstLoadCM { get; set; }
        public ICommand ReceiptPageLoadedCM { get; set; }
        public ICommand BorrowReturnPageLoadedCM { get; set; }
        public ICommand FinePageLoadedCM { get; set; }
        public ICommand OpenImportReceiptPageCM { get; set; }
        public ICommand OpenBorrowReturnPageCM { get; set; }
        public ICommand OpenFinePageCM { get; set; }
        public ICommand PrintReceiptCM { get; set; }
        public ICommand PrintBorrowReturnCM { get; set; }
        public ICommand PrintBorrowCM { get; set; }
        public ICommand PrintReturnCM { get; set; }
        public ICommand PrintFineCM { get; set; }
        public ICommand SelectedDateChangedCM { get; set; }

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
            FinePageLoadedCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                FineReceiptList = new ObservableCollection<FineReceiptDTO>(FineReceiptService.Ins.GetFineReceipts());
            });
            OpenImportReceiptPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportReceiptPage();
            });
            OpenBorrowReturnPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowReturnPage();
            });
            OpenFinePageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new FinePage();
            });
            PrintReceiptCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedReceipt is null) return;
                PrintReceiptFunc();
                SelectedReceipt = null;
            });
            PrintBorrowReturnCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBorrow is null) return;
                PrintBorrowReturn();
                SelectedBorrow = null;
            });
            PrintBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBorrow is null) return;
                PrintBorrowFunc();
                SelectedBorrow = null;
            });
            PrintReturnCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBorrow is null) return;
                if (SelectedBorrow.returnCard.returnedDate is null)
                {
                    MessageBox.Show("Sách này chưa được trả", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                PrintReturnFunc();
                SelectedBorrow = null;
            });
            PrintFineCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedFine is null) return;
                PrintFineFunc();
                SelectedFine = null;
            });
            SelectedDateChangedCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string filter = SelectedFilter.Content.ToString();
                switch (filter)
                {
                    case "Toàn bộ":
                        {
                            BorrowReturnList = new ObservableCollection<BorrowingCardDTO>(BorrowingReturnService.Ins.GetBorrowingReturnCards());
                            ImportReceiptList = new ObservableCollection<ImportReceiptDTO>(ImportService.Ins.GetAllImportReceipt());
                            FineReceiptList = new ObservableCollection<FineReceiptDTO>(FineReceiptService.Ins.GetFineReceipts());
                            return;
                        }
                    case "Theo ngày":
                        {
                            if (SelectedDate is null)
                                return;

                            BorrowReturnList = new ObservableCollection<BorrowingCardDTO>
                            (BorrowingReturnService.Ins.GetBorrowingReturnCards(borrowingDate: new DateTime(SelectedDate.Value.Year, SelectedDate.Value.Month, SelectedDate.Value.Day)));
                            ImportReceiptList = new ObservableCollection<ImportReceiptDTO>
                           (ImportService.Ins.GetAllImportReceipt(SelectedDate));
                            FineReceiptList = new ObservableCollection<FineReceiptDTO>(FineReceiptService.Ins.GetFineReceipts(SelectedDate));
                            return;
                        }
                }
            });
        }

        private void PrintFineFunc()
        {
            PrintWindow w = new PrintWindow();
            w.punishCard.Text = SelectedFine.id;
            w.date.Text = SelectedFine.createdAt.ToString("dd/MM/yyyy");
            w.name.Text = SelectedFine.readerCard.name;
            w.totalDept.Text = Utils.Helper.FormatVNMoney((decimal)SelectedFine.readerCard.totalFine);
            w.paid.Text = Utils.Helper.FormatVNMoney((decimal)SelectedFine.amount);
            var totalLeft = (decimal)(SelectedFine.readerCard.totalFine - SelectedFine.amount);
            w.remain.Text = Utils.Helper.FormatVNMoney((totalLeft < 0) ? 0 : totalLeft);
            w.ShowDialog();
        }

        public void PrintReceiptFunc()
        {
            ImportReceiptDTO rc = ImportService.Ins.GetImportReceiptDetail(SelectedReceipt.id);

            if (rc is null) return;

            Views.ImportBook.PrintWindow w = new Views.ImportBook.PrintWindow();
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
        public void PrintBorrowReturn()
        {
            string filter = SelectedFilter.Content.ToString();
            switch (filter)
            {
                case "Mượn sách":
                    {
                        PrintBorrowFunc();
                        return;
                    }
                case "Trả sách":
                    {
                        PrintReturnFunc();
                        return;
                    }
            }
        }
        public void PrintBorrowFunc()
        {
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 350);

            //create page
            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Height;

            Views.RentBook.PrintWindow w = new Views.RentBook.PrintWindow();
            w.rcId.Text = SelectedBorrow.id;
            w.date.Text = SelectedBorrow.borrowingDate.ToString("dd/MM/yyyy");
            w.reader.Text = SelectedBorrow.readerCard.name;


            RentBookVM.RentBookViewModel.Book temp = new RentBookVM.RentBookViewModel.Book
            {
                BookInfoID = SelectedBorrow.bookInfoId,
                Name = SelectedBorrow.bookInfo.Book.baseBook.name,
                DueDate = SelectedBorrow.dueDate,
            };

            ObservableCollection<RentBookVM.RentBookViewModel.Book> templist = new ObservableCollection<RentBookVM.RentBookViewModel.Book>();
            templist.Add(temp);
            w.lv.ItemsSource = templist;

            //remove element from tree
            Grid parent = w.Print.Parent as Grid;
            Grid child = w.Print as Grid;
            parent.Children.Remove(w.Print);
            page.Children.Add(child);

            // add the page to the document
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page);
            document.Pages.Add(page1Content);


            // and print
            pd.PrintDocument(document.DocumentPaginator, "Rent bill");
            MessageBox.Show("In phiếu thành thành công", "Thông báo", MessageBoxButton.OK);
        }
        public void PrintReturnFunc()
        {
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 350);

            //create page
            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Height;

            Views.ReturnBook.PrintWindow w = new Views.ReturnBook.PrintWindow();
            w.rcId.Text = SelectedBorrow.id;
            w.date.Text = SelectedBorrow.returnCard.returnedDate.Value.ToString("dd/MM/yyyy");
            w.reader.Text = SelectedBorrow.readerCard.name;


            ReturnBookVM.ReturnBookViewModel.Book temp = new ReturnBookVM.ReturnBookViewModel.Book(
                0,
                SelectedBorrow.id,
                SelectedBorrow.bookInfoId,
                "",
                0,
                SelectedBorrow.borrowingDate,
                SelectedBorrow.returnCard.returnedDate.Value.Subtract(SelectedBorrow.dueDate).Days,
                Fine: SelectedBorrow.returnCard.returnedDate.Value.Subtract(SelectedBorrow.dueDate).Days * ParameterService.Ins.GetRuleValue(Utils.Rules.FINE),
                SelectedBorrow.dueDate
                );
            if (temp.sumDelayDate < 0)
                temp.sumDelayDate = 0;
            if (temp.Fine < 0)
                temp.Fine = 0;

            ObservableCollection<ReturnBookVM.ReturnBookViewModel.Book> templist = new ObservableCollection<ReturnBookVM.ReturnBookViewModel.Book>();
            templist.Add(temp);
            w.lv.ItemsSource = templist;

            //remove element from tree
            Grid parent = w.Print.Parent as Grid;
            Grid child = w.Print as Grid;
            parent.Children.Remove(w.Print);
            page.Children.Add(child);

            // add the page to the document
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page);
            document.Pages.Add(page1Content);


            // and print
            pd.PrintDocument(document.DocumentPaginator, "Return bill");
            MessageBox.Show("In phiếu thành thành công", "Thông báo", MessageBoxButton.OK);
        }
    }
}
