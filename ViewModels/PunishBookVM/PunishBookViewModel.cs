using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using LibraryManagement.Views.PunishBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.PunishBookVM
{
    public class PunishBookViewModel : BaseViewModel
    {

        #region Command
        public ICommand CheckReaderCardCM { get; set; }
        public ICommand ConfirmCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand RemoveBookCM { get; set; }

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

        private Nullable<decimal> _TotalDept;
        public Nullable<decimal> TotalDept
        {
            get { return _TotalDept; }
            set { _TotalDept = value; OnPropertyChanged(); }
        }

        private Nullable<decimal> _TotalPaid;
        public Nullable<decimal> TotalPaid
        {
            get { return _TotalPaid; }
            set
            {
                if (_TotalPaid != value)
                {
                    _TotalPaid = value;
                    OnPropertyChanged();
                    TotalLeft = TotalDept - TotalPaid;
                }

            }
        }

        private Nullable<decimal> _TotalLeft;
        public Nullable<decimal> TotalLeft
        {
            get { return _TotalLeft; }
            set { _TotalLeft = value; OnPropertyChanged(); }
        }

        private int _ExpiredBookTotal;
        public int ExpiredBookTotal
        {
            get { return _ExpiredBookTotal; }
            set { _ExpiredBookTotal = value; OnPropertyChanged(); }
        }

        private bool _CanPaidFine;
        public bool CanPaidFine
        {
            get { return _CanPaidFine; }
            set { _CanPaidFine = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Book> _ExpiredBookList;
        public ObservableCollection<Book> ExpiredBookList
        {

            get => _ExpiredBookList;
            set
            {
                _ExpiredBookList = value;
                OnPropertyChanged();
            }
        }

        private Book _SelectedExpiredBook;
        public Book SelectedExpiredBook
        {
            get { return _SelectedExpiredBook; }
            set { _SelectedExpiredBook = value; OnPropertyChanged(); }
        }

        #endregion

        public PunishBookViewModel()
        {

            ExpiredBookList = new ObservableCollection<Book>();

            ExpiredBookTotal = ExpiredBookList.Count;
            CanPaidFine = false;

            RemoveBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedExpiredBook != null)
                {
                    ExpiredBookList.Remove(SelectedExpiredBook);
                    ExpiredBookTotal = ExpiredBookList.Count;
                }
            });

            CheckReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (IsReaderCardValid() == false)
                {
                    ClearData();
                    CanPaidFine = false;
                    return;
                }
                ReaderCardDTO readerCard = ReaderService.Ins.GetReaderInfo(ReaderID);
                ReaderName = readerCard.name;
                TotalDept = readerCard.totalFine;
                TotalLeft = 0;
                TotalPaid = null;
                ExpiredBookList.Clear();
                CanPaidFine = true;
            });

            ConfirmCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (TotalDept < TotalPaid)
                    {
                        MessageBox.Show("Không được trả quá số nợ!");
                        return;
                    }
                    decimal temp;
                    bool isMoney = decimal.TryParse(TotalPaid.ToString(), out temp);
                    if (!isMoney)
                    {
                        MessageBox.Show("Số tiền trả không hợp lệ!");
                        return;
                    }
                    FineReceiptDTO fineReceipt = new FineReceiptDTO();
                    fineReceipt.amount = (int)TotalPaid;
                    fineReceipt.createdAt = DateTime.Now;
                    fineReceipt.employeeId = MainWindowViewModel.CurrentUser.employee.id;
                    fineReceipt.readerCardId = ReaderID;

                    (bool success, string message) = FineReceiptService.Ins.CreateFineReceipt(fineReceipt);
                    if (success)
                    {
                        ClearData();
                        ReaderID = null;
                        CanPaidFine = false;

                    }
                    MessageBox.Show(message);
                }
                catch (Exception e)
                {
                    throw e;
                }

            });

            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Window window = GetWindowParent(p);
                var w = window as Window;

                if (w != null)
                {
                    if (w is PunishBookWindow)
                    {
                        w.Close();
                        return;
                    }
                }
            });


        }

        public bool IsReaderCardValid()
        {
            if (string.IsNullOrEmpty(ReaderID))
            {
                MessageBox.Show("Mã độc giả bị trống!");
                return false;
            }
            ReaderCardDTO readerCard = ReaderService.Ins.GetReaderInfo(ReaderID);
            if (readerCard == null)
            {
                MessageBox.Show("Mã độc giả không hợp lệ!");
                return false;
            }
            return true;
        }

        public void ClearData()
        {
            ReaderName = null;
            TotalDept = null;
            TotalPaid = null;
            TotalLeft = null;
            ExpiredBookTotal = 0;
            ExpiredBookList.Clear();

        }

        Window GetWindowParent(Window p)
        {
            Window parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as Window;
            }

            return parent;
        }

        public class Book : BaseViewModel
        {
            private int _STT;
            public int STT
            {
                get { return _STT; }
                set { _STT = value; OnPropertyChanged(); }
            }

            private string _BookId;
            public string BookId
            {
                get { return _BookId; }
                set { _BookId = value; }
            }

            private Nullable<System.DateTime> _DateRent;
            public Nullable<System.DateTime> DateRent
            {
                get { return _DateRent; }
                set { _DateRent = value; }
            }

            private int _SumDateExpired;
            public int SumDateExpired
            {
                get { return _SumDateExpired; }
                set { _SumDateExpired = value; }
            }

            private int _Fine;
            public int Fine
            {
                get { return _Fine; }
                set { _Fine = value; }
            }

            public Book(
            int STT,
            string BookId,
            Nullable<System.DateTime> DateRent,
            int SumDateExpired,
            int Fine)
            {
                _STT = STT;
                _BookId = BookId;
                _DateRent = DateRent;
                _SumDateExpired = SumDateExpired;
                _Fine = Fine;
            }
        }

    }
}
