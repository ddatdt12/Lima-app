using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using LibraryManagement.Views.ReturnBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace LibraryManagement.ViewModels.ReturnBookVM
{
    public class ReturnBookViewModel : BaseViewModel
    {
        #region Command
        public ICommand FirstLoadCM { get; set; }
        public ICommand CheckReaderCardCM { get; set; }
        public ICommand ConfirmCM { get; set; }
        public ICommand RemoveBookCM { get; set; }
        public ICommand AddBookCM { get; set; }

        #endregion

        #region Biến binding

        private string _ReaderID;
        public string ReaderID
        {
            get { return _ReaderID; }
            set { _ReaderID = value; OnPropertyChanged(); }
        }

        private string _ReaderName;
        public string ReaderName
        {
            get { return _ReaderName; }
            set { _ReaderName = value; OnPropertyChanged(); }
        }

        private Decimal? _TotalDept;
        public Decimal? TotalDept
        {
            get { return _TotalDept; }
            set { _TotalDept = value; OnPropertyChanged(); }
        }

        private Nullable<decimal> _TotalPunish;
        public Nullable<decimal> TotalPunish
        {
            get { return _TotalPunish; }
            set { _TotalPunish = value; OnPropertyChanged(); }
        }

        private int _ReturnBookTotal;
        public int ReturnBookTotal
        {
            get { return _ReturnBookTotal; }
            set { _ReturnBookTotal = value; OnPropertyChanged(); }
        }

        private bool _CanReturn;
        public bool CanReturn
        {
            get { return _CanReturn; }
            set { _CanReturn = value; OnPropertyChanged(); }
        }

        private string _IdSearchBook;

        public string IdSearchBook
        {
            get { return _IdSearchBook; }
            set
            {
                if (_IdSearchBook != value)
                {
                    _IdSearchBook = value;
                    OnPropertyChanged();
                    if (CanReturn)
                    {
                        RentingBookList.Clear();
                        List<BorrowingCardDTO> listBorrowingCard = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId(ReaderID);
                        for (int i = 0; i < listBorrowingCard.Count; i++)
                        {
                            if (listBorrowingCard[i].bookInfo.id.Contains(IdSearchBook))
                            {
                                int sumDelayDate;
                                sumDelayDate = DateTime.Now.Subtract(listBorrowingCard[i].dueDate).Days;
                                decimal fine = sumDelayDate * ParameterService.Ins.GetRuleValue(Utils.Rules.FINE);
                                Book book = new Book
                                (
                                    0,
                                    listBorrowingCard[i].id,
                                    listBorrowingCard[i].bookInfo.id,
                                    listBorrowingCard[i].bookInfo.Book.publisher,
                                    listBorrowingCard[i].bookInfo.Book.yearOfPublication,
                                    listBorrowingCard[i].borrowingDate,
                                    sumDelayDate,
                                    fine,
                                     listBorrowingCard[i].dueDate
                                    );
                                RentingBookList.Add(book);
                            }
                        }
                        for (int i = 0; i < ReturnBookList.Count; i++)
                        {
                            for (int j = 0; j < RentingBookList.Count; j++)
                            {
                                if (ReturnBookList[i].BookId == RentingBookList[j].BookId)
                                {
                                    RentingBookList.Remove(RentingBookList[j]);
                                }
                            }
                        }
                    }

                }
            }
        }

        private List<Book> currentPrint;
        public List<Book> CurrentPrint
        {
            get { return currentPrint; }
            set { currentPrint = value; OnPropertyChanged(); }
        }


        #endregion

        private ObservableCollection<Book> _ReturnBookList;
        public ObservableCollection<Book> ReturnBookList
        {

            get => _ReturnBookList;
            set
            {
                _ReturnBookList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _RentingBookList;
        public ObservableCollection<Book> RentingBookList
        {

            get => _RentingBookList;
            set
            {
                _RentingBookList = value;
                OnPropertyChanged();
            }
        }

        private Book _ReturnSelectedBook;
        public Book ReturnSelectedBook
        {
            get { return _ReturnSelectedBook; }
            set { _ReturnSelectedBook = value; OnPropertyChanged(); }
        }

        private Book _RentingSelectedBook;
        public Book RentingSelectedBook
        {
            get { return _RentingSelectedBook; }
            set { _RentingSelectedBook = value; OnPropertyChanged(); }
        }

        public ReturnBookViewModel()
        {
            ReturnBookList = new ObservableCollection<Book>();
            RentingBookList = new ObservableCollection<Book>();

            List<BorrowingCardDTO> listBorrowingCard = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId(ReaderID);

            ReturnBookTotal = ReturnBookList.Count;
            CanReturn = false;

            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ClearData();
                ReaderID = string.Empty;
            });

            CheckReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(ReaderID))
                {
                    MessageBox.Show("Vui lòng nhập mã độc giả!");
                    ClearData();
                    return;
                }
                ReaderCardDTO readerCard = ReaderService.Ins.GetReaderInfo(ReaderID);
                if (readerCard == null)
                {
                    MessageBox.Show("Mã độc giả không tồn tại!");
                    ClearData();
                    return;
                }
                ReaderName = readerCard.name;
                TotalDept = readerCard.totalFine;
                TotalPunish = 0;
                ReturnBookList.Clear();
                RentingBookList.Clear();
                listBorrowingCard = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId(ReaderID);
                if (ReturnBookList.Count + RentingBookList.Count != listBorrowingCard.Count)
                {
                    for (int i = 0; i < listBorrowingCard.Count; i++)
                    {
                        int sumDelayDate;
                        sumDelayDate = DateTime.Now.Subtract(listBorrowingCard[i].dueDate).Days;
                        decimal fine = sumDelayDate * ParameterService.Ins.GetRuleValue(Utils.Rules.FINE);
                        if (fine < 0) fine = 0;
                        if (sumDelayDate < 0) sumDelayDate = 0;

                        Book book = new Book
                        (
                            0,
                            listBorrowingCard[i].id,
                            listBorrowingCard[i].bookInfo.id,
                            listBorrowingCard[i].bookInfo.Book.publisher,
                            listBorrowingCard[i].bookInfo.Book.yearOfPublication,
                            listBorrowingCard[i].borrowingDate,
                            sumDelayDate,
                            fine,
                            listBorrowingCard[i].dueDate
                            );
                        RentingBookList.Add(book);
                    }
                }
                CanReturn = true;

            });

            ConfirmCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ReturnBookTotal == 0)
                {
                    MessageBox.Show("Danh sách sách trả đang trống!");
                    return;
                }
                var returnCardList = new List<ReturnCardDTO>();
                for (int i = 0; i < ReturnBookList.Count; i++)
                {
                    ReturnCardDTO returnCard = new ReturnCardDTO();
                    returnCard.id = ReturnBookList[i].RentCardId;
                    returnCard.borrowingCardId = ReturnBookList[i].RentCardId;
                    returnCard.returnedDate = DateTime.Now;
                    returnCard.employeeId = MainWindowViewModel.CurrentUser.employee.id;
                    returnCard.fine = (int)ReturnBookList[i].Fine;
                    returnCardList.Add(returnCard);
                }
                (bool success, string message) = BorrowingReturnService.Ins.CreateReturnCardList(returnCardList);
                if (success)
                {
                    OpenPrintWindow(ReturnBookList);
                    ClearData();
                }
                MessageBox.Show(message);
            });

            RemoveBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                if (ReturnSelectedBook != null)
                {
                    Book bookReturn = ReturnSelectedBook;
                    ReturnBookList.Remove(bookReturn);
                    RentingBookList.Add(bookReturn);
                    ReturnBookTotal = ReturnBookList.Count;
                    decimal totalPunish = 0;
                    for (int i = 0; i < ReturnBookList.Count; i++)
                    {
                        ReturnBookList[i].STT = i + 1;
                        totalPunish += ReturnBookList[i].Fine;
                    }
                    TotalPunish = totalPunish;
                }


            });

            AddBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (RentingSelectedBook != null)
                {
                    RentingSelectedBook.STT = ReturnBookList.Count + 1;
                    ReturnBookList.Add(RentingSelectedBook);
                    RentingBookList.Remove(RentingSelectedBook);
                    ReturnBookTotal = ReturnBookList.Count;
                    decimal totalPunish = 0;
                    for (int i = 0; i < ReturnBookList.Count; i++)
                    {
                        ReturnBookList[i].STT = i + 1;
                        totalPunish += ReturnBookList[i].Fine;
                    }
                    TotalPunish = totalPunish;
                }

            });

        }

        public void ClearData()
        {
            ReturnBookList.Clear();
            RentingBookList.Clear();
            ReaderName = null;
            TotalDept = null;
            TotalPunish = null;
            ReturnBookTotal = 0;
            CanReturn = false;
        }

        public void OpenPrintWindow(ObservableCollection<Book> rt)
        {
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 350);



            foreach (var item in rt)
            {
                //create page
                FixedPage page = new FixedPage();
                page.Width = document.DocumentPaginator.PageSize.Width;
                page.Height = document.DocumentPaginator.PageSize.Height;

                PrintWindow w = new PrintWindow();
                w.rcId.Text = item.RentCardId.ToString();
                w.date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                w.reader.Text = ReaderName;
                CurrentPrint = new List<Book>();
                CurrentPrint.Add(item);
                w.lv.ItemsSource = CurrentPrint;

                //remove element from tree
                Grid parent = w.Print.Parent as Grid;
                Grid child = w.Print as Grid;
                parent.Children.Remove(w.Print);
                page.Children.Add(child);

                // add the page to the document
                PageContent page1Content = new PageContent();
                ((IAddChild)page1Content).AddChild(page);
                document.Pages.Add(page1Content);
            }

            // and print
            pd.PrintDocument(document.DocumentPaginator, "Return bill");

        }

        public class Book : BaseViewModel
        {
            private int _STT;
            public int STT
            {
                get { return _STT; }
                set { _STT = value; OnPropertyChanged(); }
            }

            private string _RentCardId;
            public string RentCardId
            {
                get { return _RentCardId; }
                set { _RentCardId = value; }
            }

            private string _BookId;
            public string BookId
            {
                get { return _BookId; }
                set { _BookId = value; }
            }

            private string _Publisher;
            public string Publisher
            {
                get { return _Publisher; }
                set { _Publisher = value; }
            }

            private int _YearPublisher;
            public int YearPublisher
            {
                get { return _YearPublisher; }
                set { _YearPublisher = value; }
            }

            private Nullable<System.DateTime> _DateRent;
            public Nullable<System.DateTime> DateRent
            {
                get { return _DateRent; }
                set { _DateRent = value; }
            }

            private int _sumDelayDate;
            public int sumDelayDate
            {
                get { return _sumDelayDate; }
                set { _sumDelayDate = value; }
            }

            private decimal _Fine;
            public decimal Fine
            {
                get { return _Fine; }
                set { _Fine = value; }
            }

            private DateTime _dueDate;
            public DateTime DueDate
            {
                get { return _dueDate; }
                set { _dueDate = value; }
            }


            public Book(
            int STT,
            string RentCardId,
            string BookId,
            string Publisher,
            int YearPublisher,
            Nullable<System.DateTime> DateRent,
            int sumDelayDate,
            decimal Fine, DateTime due)
            {
                _STT = STT;
                _RentCardId = RentCardId;
                _BookId = BookId;
                _Publisher = Publisher;
                _YearPublisher = YearPublisher;
                _DateRent = DateRent;
                _sumDelayDate = sumDelayDate;
                _Fine = Fine;
                _dueDate = due;
            }
        }
    }
}
