using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
                        List<BookDTO> listBookDTO = BookService.Ins.GetAllAvailableBook();
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
                            string temp = listBookDTO[i].baseBook.name.ToLower();
                            if (temp.Contains(NameSearchBook.ToLower()))
                            {
                                Book book = new Book(
                                0,
                                listBookDTO[i].id,
                                listBookDTO[i].baseBook.name,
                                listBookDTO[i].baseBook.genre.name,
                                author,
                                listBookDTO[i].publisher,
                                listBookDTO[i].yearOfPublication
                                );
                                BookList.Add(book);
                            }
                        }
                        for (int i = 0; i < RentBookList.Count; i++)
                        {
                            for (int j = 0; j < BookList.Count; j++)
                            {
                                if (RentBookList[i].BookID == BookList[j].BookID)
                                {
                                    BookList.Remove(BookList[j]);
                                }
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

        public RentBookViewModel()
        {
            IsReaderCardExpired = Visibility.Collapsed;
            IsHaveOutdatedBook = Visibility.Collapsed;
            CanRent = false;

            List<BookDTO> listBookDTO = BookService.Ins.GetAllAvailableBook();

            RentBookList = new ObservableCollection<Book>();
            BookList = new ObservableCollection<Book>();

            RentBookTotal = RentBookList.Count;
            RentDate = DateTime.Now;
            DateTime dateTimeSub = RentDate.Value.AddDays(ParameterService.Ins.GetRuleValue(Utils.Rules.VALIDITY_PERIOD_OF_CARD));
            ExpiredBookDate = dateTimeSub;

            CheckReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CanRent = true;
                if (ReaderID == null)
                {
                    MessageBox.Show("Mã độc giả bị trống!");
                    RentBookList.Clear();
                    BookList.Clear();
                    ReaderName = string.Empty;
                    ExpiredDate = null;
                    ExpiredBook = null;
                    RentBookQuantity = null;
                    RentDate = null;
                    ExpiredBookDate = null;
                    IsHaveOutdatedBook = Visibility.Collapsed;
                    IsReaderCardExpired = Visibility.Collapsed;
                    RentBookTotal = 0;
                    CanRent = false;
                    return;
                }
                ReaderCardDTO readerCard = ReaderService.Ins.GetReaderInfo(ReaderID);
                if (readerCard == null)
                {
                    MessageBox.Show("Mã độc giả không tồn tại!");
                    RentBookList.Clear();
                    BookList.Clear();
                    ReaderName = string.Empty;
                    ExpiredDate = null;
                    ExpiredBook = null;
                    RentBookQuantity = null;
                    RentDate = null;
                    ExpiredBookDate = null;
                    IsHaveOutdatedBook = Visibility.Collapsed;
                    IsReaderCardExpired = Visibility.Collapsed;
                    RentBookTotal = 0;
                    CanRent = false;
                    return;
                }
                ReaderName = readerCard.name;
                ExpiredDate = readerCard.expiryDate;
                if (readerCard.haveDelayBook)
                {
                    ExpiredBook = "Có";
                    IsHaveOutdatedBook = Visibility.Visible;
                    CanRent = false;
                }
                else
                {
                    ExpiredBook = "Không";
                    IsHaveOutdatedBook = Visibility.Collapsed;
                    CanRent = true;
                }
                RentBookQuantity = readerCard.numberOfBorrowingBooks;
                if (readerCard.expiryDate < DateTime.Now)
                {
                    IsReaderCardExpired = Visibility.Visible;
                    CanRent = false;
                }
                else
                {
                    IsReaderCardExpired = Visibility.Collapsed;
                    CanRent = true;
                }
                listBookDTO = BookService.Ins.GetAllAvailableBook();
                if (CanRent)
                {
                    if ((BookList.Count + RentBookList.Count) != listBookDTO.Count)
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

                            Book book = new Book(
                                0,
                                listBookDTO[i].id,
                                listBookDTO[i].baseBook.name,
                                listBookDTO[i].baseBook.genre.name,
                                author,
                                listBookDTO[i].publisher,
                                listBookDTO[i].yearOfPublication
                                );
                            BookList.Add(book);
                        }
                    }
                }



            }
               );

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
                    for (int j = 0; j < listBookDTO.Count; j++)
                    {
                        if (RentBookList[i].BookID == listBookDTO[j].id)
                        {
                            for (int k = 0; k < listBookDTO[j].bookInfoes.Count; k++)
                            {
                                if (listBookDTO[j].bookInfoes[k].status)
                                {
                                    bookInfoList.Add(listBookDTO[j].bookInfoes[k].id);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }

                try
                {
                    (bool success, string message) = BorrowingReturnService.Ins.CreateBorrowingCard(borrowingCard, bookInfoList);
                    if (success)
                    {
                        RentBookList.Clear();
                        BookList.Clear();
                        ReaderID = string.Empty;
                        ReaderName = string.Empty;
                        ExpiredDate = null;
                        ExpiredBook = null;
                        RentBookQuantity = null;
                        IsHaveOutdatedBook = Visibility.Collapsed;
                        IsReaderCardExpired = Visibility.Collapsed;
                        RentBookTotal = 0;
                        CanRent = false;
                    }
                    MessageBox.Show(message);
                }
                catch (Exception)
                {

                    throw;
                }
            }
               );

            RemoveBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedRentBook != null)
                {
                    Book book1 = SelectedRentBook;
                    RentBookList.Remove(book1);
                    BookList.Add(book1);
                    RentBookTotal = RentBookList.Count;
                    for (int i = 0; i < RentBookList.Count; i++)
                    {
                        RentBookList[i].STT = i + 1;
                    }
                }

            }
               );

            AddBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBook != null)
                {
                    SelectedBook.STT = RentBookList.Count + 1;
                    RentBookList.Add(SelectedBook);
                    BookList.Remove(SelectedBook);
                    RentBookTotal = RentBookList.Count;
                }

            }
               );

            SelectedDateCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (RentDate != null)
                {
                    DateTime dateTime = RentDate.Value.AddDays(ParameterService.Ins.GetRuleValue(Utils.Rules.VALIDITY_PERIOD_OF_CARD));
                    ExpiredBookDate = dateTime;
                }

            });
        }


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


        public Book(int STT, string BookID, string Name, string Category, string Author, string Publisher, int YearPublisher)
        {
            _STT = STT;
            _BookID = BookID;
            _Name = Name;
            _Category = Category;
            _Author = Author;
            _Publisher = Publisher;
            _YearPublisher = YearPublisher;
        }
    }
}
