using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.HomeVM
{
    public class ReaderHomeViewModel : BaseViewModel
    {

        private int totalRent;
        public int TotalRent
        {
            get { return totalRent; }
            set { totalRent = value; OnPropertyChanged(); }
        }

        private int dateLeft;
        public int DateLeft
        {
            get { return dateLeft; }
            set { dateLeft = value; OnPropertyChanged(); }
        }

        private int totalFine;
        public int TotalFine
        {
            get { return totalFine; }
            set { totalFine = value; OnPropertyChanged(); }
        }

        private string ruleFine;
        public string RuleFine
        {
            get { return ruleFine; }
            set { ruleFine = value; OnPropertyChanged(); }
        }


        private ObservableCollection<BorrowingCardDTO> rentingList;
        public ObservableCollection<BorrowingCardDTO> RentingList
        {
            get { return rentingList; }
            set { rentingList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BorrowingCardDTO> delayList;
        public ObservableCollection<BorrowingCardDTO> DelayList
        {
            get { return delayList; }
            set { delayList = value; OnPropertyChanged(); }
        }

        private ReaderCardDTO readerInfo;
        public ReaderCardDTO ReaderInfo
        {
            get { return readerInfo; }
            set { readerInfo = value; OnPropertyChanged(); }
        }


        public ICommand FirstLoadCM { get; set; }

        public ReaderHomeViewModel()
        {
            FirstLoadCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ReaderInfo = ReaderService.Ins.GetReaderInfo(MainWindowViewModel.CurrentUser.reader.id);
                RuleFine =Utils.Helper.FormatVNMoney(ParameterService.Ins.GetRuleValue(Utils.Rules.FINE)) + "/ngày";

                if (ReaderInfo is null) return;

                TotalFine = ReaderInfo.totalFine;
                DateLeft = ((ReaderInfo.expiryDate - DateTime.Now).Days);
                if (DateLeft < 0)
                    DateLeft = 0;
                TotalRent = ReaderInfo.numberOfBorrowingBooks;

                RentingList = new ObservableCollection<BorrowingCardDTO>(BorrowingReturnService.Ins.GetBorrowingCardsByReaderId(ReaderInfo.id));
                DelayList = new ObservableCollection<BorrowingCardDTO>(BorrowingReturnService.Ins.GetDelayBorrowingCardsByReaderId(ReaderInfo.id));
                foreach (var item in DelayList)
                {
                    item.currentFine = DateTime.Now.Subtract(item.dueDate).Days * ParameterService.Ins.GetRuleValue(Utils.Rules.FINE);
                }
            });
        }
    }
}
