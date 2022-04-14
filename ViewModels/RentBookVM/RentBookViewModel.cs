using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using LibraryManagement.Views.RentBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace LibraryManagement.ViewModels.RentBookVM
{
    public class RentBookViewModel : BaseViewModel
    {
        #region Command

        public ICommand CheckReaderCardCM { get; set; }
        public ICommand ConfirmCM { get; set; }
        public ICommand RemoveBookCM { get; set; }
        public ICommand AddBookCM { get; set; }
        public ICommand SelectedDateCM { get; set; }
        public ICommand FirstLoadCM { get; set; }

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

        private Nullable<System.DateTime> _ExpiredDate;
        public Nullable<System.DateTime> ExpiredDate
        {
            get { return _ExpiredDate; }
            set { _ExpiredDate = value; OnPropertyChanged(); }
        }

        private string _ExpiredBook;
        public string ExpiredBook
        {
            get { return _ExpiredBook; }
            set { _ExpiredBook = value; OnPropertyChanged(); }
        }

        private Nullable<int> _RentBookQuantity;
        public Nullable<int> RentBookQuantity
        {
            get { return _RentBookQuantity; }
            set { _RentBookQuantity = value; OnPropertyChanged(); }
        }

        private Nullable<System.DateTime> _RentDate;
        public Nullable<System.DateTime> RentDate
        {
            get { return _RentDate; }
            set { _RentDate = value; OnPropertyChanged(); }
        }

        private DateTime? todayDay;
        public DateTime? TodayDay
        {
            get { return todayDay; }
            set { todayDay = value; OnPropertyChanged(); }
        }


        private int _RentBookTotal;
        public int RentBookTotal
        {
            get { return _RentBookTotal; }
            set { _RentBookTotal = value; OnPropertyChanged(); }
        }

        private Visibility _IsReaderCardExpired;
        public Visibility IsReaderCardExpired
        {
            get { return _IsReaderCardExpired; }
            set { _IsReaderCardExpired = value; OnPropertyChanged(); }
        }

        private Visibility _IsHaveOutdatedBook;
        public Visibility IsHaveOutdatedBook
        {
            get { return _IsHaveOutdatedBook; }
            set { _IsHaveOutdatedBook = value; OnPropertyChanged(); }
        }

        private bool _CanRent;
        public bool CanRent
        {
            get { return _CanRent; }
            set { _CanRent = value; OnPropertyChanged(); }
        }

        private string _NameSearchBook;

        public string NameSearchBook
        {
            get { return _NameSearchBook; }
            set
            {
                if (_NameSearchBook != value)
                {
                    _NameSearchBook = value;
                    OnPropertyChanged();
                    if (CanRent)
                    {
                        BookList.Clear();
                        List<BookDTO> listBookDTO = FilterListBook(BookService.Ins.GetAllAvailableBook());

                        for (int i = 0; i < listBookDTO.Count; i++)
                        {
                            string temp = listBookDTO[i].baseBook.name.ToLower();
                            if (temp.Contains(NameSearchBook.ToLower()))
                            {
                                string author = "";
                                for (int j = 0; j < listBookDTO[i].baseBook.authors.Count; j++)
                                {
                                    author += listBookDTO[i].baseBook.authors[j].name;
                                    if (j != listBookDTO[i].baseBook.authors.Count - 1)
                                    {
                                        author += '\n';
                                    }
                                }
                                Book book = new Book();
                                book.ListBookInfoId = new ObservableCollection<string>();
                                book.STT = 0;
                                book.BookID = listBookDTO[i].id;
                                //Lấy danh sách bookInfoId chưa mượn
                                for (int j = 0; j < listBookDTO[i].bookInfoes.Count; j++)
                                {
                                    if (listBookDTO[i].bookInfoes[j].status)
                                    {
                                        book.ListBookInfoId.Add(listBookDTO[i].bookInfoes[j].id);
                                    }
                                }
                                book.Name = listBookDTO[i].baseBook.name;
                                book.Category = listBookDTO[i].baseBook.genre.name;
                                book.Author = author;
                                book.Publisher = listBookDTO[i].publisher;
                                book.YearPublisher = listBookDTO[i].yearOfPublication;
                                //if (book.BookInfoID != null)
                                //{
                                //    BookList.Add(book);
                                //}
                                BookList.Add(book);
                            }
                        }

                        for (int i = 0; i < BookList.Count; i++)
                        {
                            if (BookList[i].ListBookInfoId.Count == 0)
                            {
                                BookList.RemoveAt(i);
                            }
                        }
                    }

                }
            }
        }

        private Nullable<System.DateTime> _ExpiredBookDate;
        public Nullable<System.DateTime> ExpiredBookDate
        {
            get { return _ExpiredBookDate; }
            set { _ExpiredBookDate = value; OnPropertyChanged(); }
        }

        #endregion

        private ObservableCollection<Book> _RentBookList;
        public ObservableCollection<Book> RentBookList
        {

            get => _RentBookList;
            set
            {
                _RentBookList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Book> _BookList;
        public ObservableCollection<Book> BookList
        {

            get => _BookList;
            set
            {
                _BookList = value;
                OnPropertyChanged();
            }
        }

        private Book _SelecteRentdBook;
        public Book SelectedRentBook
        {
            get { return _SelecteRentdBook; }
            set { _SelecteRentdBook = value; OnPropertyChanged(); }
        }

        private Book _SelecteddBook;
        public Book SelectedBook
        {
            get { return _SelecteddBook; }
            set { _SelecteddBook = value; OnPropertyChanged(); }
        }

        public List<Book> CurrentPrint { get; set; }
        List<BookDTO> listBookDTO { get; set; }


        public RentBookViewModel()
        {
            listBookDTO = new List<BookDTO>();
            RentBookList = new ObservableCollection<Book>();
            BookList = new ObservableCollection<Book>();
            RentBookTotal = RentBookList.Count;

            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ClearData();
                ReaderID = string.Empty;
                IsReaderCardExpired = Visibility.Collapsed;
                IsHaveOutdatedBook = Visibility.Collapsed;
                CanRent = false;
                RentDate = DateTime.Now;
                DateTime dateTimeSub = RentDate.Value.AddDays(ParameterService.Ins.GetRuleValue(Utils.Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW));
                ExpiredBookDate = dateTimeSub;
                TodayDay = DateTime.Today;

            });

            CheckReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ReaderID == null)
                {
                    MessageBox.Show("Mã độc giả bị trống!");
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
                CanRent = true;
                RentBookList.Clear();
                BookList.Clear();
                ReaderName = readerCard.name;
                ExpiredDate = readerCard.expiryDate;
                if (readerCard.haveDelayBook)
                {
                    ExpiredBook = "Có";
                    IsHaveOutdatedBook = Visibility.Visible;
                }
                else
                {
                    ExpiredBook = "Không";
                    IsHaveOutdatedBook = Visibility.Collapsed;
                }
                RentBookQuantity = readerCard.numberOfBorrowingBooks;
                if (readerCard.expiryDate < DateTime.Now)
                {
                    IsReaderCardExpired = Visibility.Visible;
                }
                else
                {
                    IsReaderCardExpired = Visibility.Collapsed;
                }
                if (readerCard.expiryDate < DateTime.Now || readerCard.haveDelayBook)
                {
                    CanRent = false;
                }
                listBookDTO = BookService.Ins.GetAllAvailableBook();
                if (CanRent)
                {
                    for (int i = 0; i < listBookDTO.Count; i++)
                    {
                        string author = "";
                        for (int j = 0; j < listBookDTO[i].baseBook.authors.Count; j++)
                        {
                            author += listBookDTO[i].baseBook.authors[j].name;
                            if (j != listBookDTO[i].baseBook.authors.Count - 1)
                            {
                                author += '\n';
                            }
                        }
                        Book book = new Book();
                        book.ListBookInfoId = new ObservableCollection<string>();
                        book.STT = 0;
                        book.BookID = listBookDTO[i].id;
                        //Lấy danh sách bookInfoId chưa mượn
                        for (int j = 0; j < listBookDTO[i].bookInfoes.Count; j++)
                        {
                            if (listBookDTO[i].bookInfoes[j].status)
                            {
                                book.ListBookInfoId.Add(listBookDTO[i].bookInfoes[j].id);
                            }
                        }
                        book.Name = listBookDTO[i].baseBook.name;
                        book.Category = listBookDTO[i].baseBook.genre.name;
                        book.Author = author;
                        book.Publisher = listBookDTO[i].publisher;
                        book.YearPublisher = listBookDTO[i].yearOfPublication;
                        BookList.Add(book);
                    }
                }


            });

            ConfirmCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (RentBookList.Count == 0)
                {
                    MessageBox.Show("Danh sách mượn trống!");
                    return;
                }
                if ((RentBookQuantity + RentBookTotal) > ParameterService.Ins.GetRuleValue(Utils.Rules.ALLOWED_BOOK_MAXIMUM))
                {
                    MessageBox.Show("Mượn quá số sách quy định!");
                    return;
                }
                if (RentDate == null)
                {
                    MessageBox.Show("Ngày mượn không được để trống!");
                    return;
                }
                BorrowingCardDTO borrowingCard = new BorrowingCardDTO();
                borrowingCard.readerCardId = ReaderID;
                borrowingCard.borrowingDate = (DateTime)RentDate;
                borrowingCard.employeeId = MainWindowViewModel.CurrentUser.employee.id;
                borrowingCard.dueDate = RentDate.Value.AddDays(ParameterService.Ins.GetRuleValue(Utils.Rules.VALIDITY_PERIOD_OF_CARD));
                var bookInfoList = new List<string>();
                for (int i = 0; i < RentBookList.Count; i++)
                {
                    bookInfoList.Add(RentBookList[i].BookInfoID);
                }

                try
                {
                    (bool success, string message) = BorrowingReturnService.Ins.CreateBorrowingCard(borrowingCard, bookInfoList);
                    if (success)
                    {
                        List<BorrowingCardDTO> listBorrowingCard = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId(ReaderID);
                        for(int i=0; i < RentBookList.Count; i++)
                        {
                            RentBookList[i].DueDate = ExpiredBookDate;
                        }
                        OpenPrintWindow(RentBookList);
                        ClearData();
                    }
                    MessageBox.Show(message);
                }
                catch (Exception)
                {
                    throw;
                }
            });

            RemoveBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedRentBook != null)
                {
                    if (!IsExistInBookList(SelectedRentBook))
                    {
                        SelectedRentBook.ListBookInfoId = new ObservableCollection<string>();
                        SelectedRentBook.ListBookInfoId.Add(SelectedRentBook.BookInfoID);
                        BookList.Add(SelectedRentBook);
                    }
                    else
                    {
                        for (int i = 0; i < BookList.Count; i++)
                        {
                            if (BookList[i].BookID == SelectedRentBook.BookID)
                            {
                                BookList[i].ListBookInfoId.Add(SelectedRentBook.BookInfoID);
                            }
                        }
                    }
                    RentBookList.Remove(SelectedRentBook);
                    RentBookTotal = RentBookList.Count;
                    for (int i = 0; i < RentBookList.Count; i++)
                    {
                        RentBookList[i].STT = i + 1;
                    }
                }

            });

            AddBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var list = BookList;
                if (SelectedBook != null)
                {
                    Book temp = new Book();
                    temp.BookID = SelectedBook.BookID;
                    temp.Name = SelectedBook.Name;
                    temp.Category = SelectedBook.Category;
                    temp.Author = SelectedBook.Author;
                    temp.Publisher = SelectedBook.Publisher;
                    temp.YearPublisher = SelectedBook.YearPublisher;

                    temp.BookInfoID = SelectedBook.ListBookInfoId[0];
                    temp.STT = RentBookList.Count + 1;
                    SelectedBook.ListBookInfoId.RemoveAt(0);
                    if (SelectedBook.ListBookInfoId.Count == 0)
                    {
                        BookList.Remove(SelectedBook);
                    }

                    RentBookList.Add(temp);
                    RentBookTotal = RentBookList.Count;
                }
            });

            SelectedDateCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (RentDate != null)
                {
                    DateTime dateTime = RentDate.Value.AddDays(ParameterService.Ins.GetRuleValue(Utils.Rules.VALIDITY_PERIOD_OF_CARD));
                    ExpiredBookDate = dateTime;
                }

            });


        }

        public List<BookDTO> FilterListBook(List<BookDTO> listBook)
        {
            for (int i = 0; i < RentBookList.Count; i++)
            {
                for (int j = 0; j < listBook.Count; j++)
                {
                    if (RentBookList[i].BookID == listBook[j].id)
                    {
                        for (int k = 0; k < listBook[j].bookInfoes.Count; k++)
                        {
                            if (listBook[j].bookInfoes[k].id == RentBookList[i].BookInfoID)
                            {
                                listBook[j].bookInfoes.RemoveAt(k);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            for (int i = 0; i < listBook.Count; i++)
            {
                if (listBook[i].bookInfoes.Count == 0)
                {
                    listBook.RemoveAt(i);
                }
            }
            return listBook;
        }

        

        public void ClearData()
        {
            listBookDTO.Clear();
            NameSearchBook = string.Empty;
            RentBookList.Clear();
            BookList.Clear();
            ReaderName = string.Empty;
            ExpiredDate = null;
            ExpiredBook = null;
            RentBookQuantity = null;
            RentDate = null;
            IsHaveOutdatedBook = Visibility.Collapsed;
            IsReaderCardExpired = Visibility.Collapsed;
            RentBookTotal = 0;
            CanRent = false;
        }

        public bool IsExistInBookList(Book book)
        {
            for (int i = 0; i < BookList.Count; i++)
            {
                if (BookList[i].BookID == book.BookID)
                {
                    return true;
                }
            }
            return false;
        }

        public void OpenPrintWindow(ObservableCollection<Book> rl)
        {
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 350);



            foreach (var item in rl)
            {
                //create page
                FixedPage page = new FixedPage();
                page.Width = document.DocumentPaginator.PageSize.Width;
                page.Height = document.DocumentPaginator.PageSize.Height;

                PrintWindow w = new PrintWindow();
                w.rcId.Text = item.BookInfoID;
                w.date.Text = RentDate.Value.ToString("dd/MM/yyyy");
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

            private string _BookID;
            public string BookID
            {
                get { return _BookID; }
                set { _BookID = value; }
            }

            private string _BookInfoID;
            public string BookInfoID
            {
                get { return _BookInfoID; }
                set { _BookInfoID = value; }
            }

            private ObservableCollection<string> _ListBookInfoId;
            public ObservableCollection<string> ListBookInfoId
            {
                get { return _ListBookInfoId; }
                set { _ListBookInfoId = value; }
            }

            private string _Name;
            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }

            private string _Category;
            public string Category
            {
                get { return _Category; }
                set { _Category = value; }
            }

            private string _Author;
            public string Author
            {
                get { return _Author; }
                set { _Author = value; }
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


            private Nullable<System.DateTime> _DueDate;
            public Nullable<System.DateTime> DueDate
            {
                get { return _DueDate; }
                set { _DueDate = value; OnPropertyChanged(); }
            }

            public Book() { }
        }
    }
}