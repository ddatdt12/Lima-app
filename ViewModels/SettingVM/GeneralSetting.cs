using LibraryManagement.Services;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {
        private int minAge;
        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; OnPropertyChanged(); }
        }

        private int maxAge;
        public int MaxAge
        {
            get { return maxAge; }
            set { maxAge = value; OnPropertyChanged(); }
        }

        private int expiredDay;
        public int ExpiredDay
        {
            get { return expiredDay; }
            set { expiredDay = value; OnPropertyChanged(); }
        }

        private int maxRent;
        public int MaxRent
        {
            get { return maxRent; }
            set { maxRent = value; OnPropertyChanged(); }
        }

        private int returnDate;
        public int ReturnDate
        {
            get { return returnDate; }
            set { returnDate = value; OnPropertyChanged(); }
        }

        private int finesAmount;
        public int FinesAmount
        {
            get { return finesAmount; }
            set { finesAmount = value; OnPropertyChanged(); }
        }

        private int publishYear;
        public int PublishYear
        {
            get { return publishYear; }
            set { publishYear = value; OnPropertyChanged(); }
        }


        public void GeneralFirstLoadFunc()
        {
            MinAge = ParameterService.Ins.GetRuleValue(Rules.MIN_AGE);
            MaxAge = ParameterService.Ins.GetRuleValue(Rules.MAX_AGE);
            ExpiredDay = ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD);
            MaxRent = ParameterService.Ins.GetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM);
            ReturnDate = ParameterService.Ins.GetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW);
            FinesAmount = ParameterService.Ins.GetRuleValue(Rules.FINE);
            PublishYear = ParameterService.Ins.GetRuleValue(Rules.YEAR_PUBLICATION_PERIOD);
        }

        public void SaveGeneralSettingFunc()
        {
            ParameterService.Ins.SetRuleValue(Rules.MIN_AGE, MinAge);
            ParameterService.Ins.SetRuleValue(Rules.MAX_AGE, MaxAge);
            ParameterService.Ins.SetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD, ExpiredDay);
            ParameterService.Ins.SetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM, MaxRent);
            ParameterService.Ins.SetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW, ReturnDate);
            ParameterService.Ins.SetRuleValue(Rules.FINE, FinesAmount);
            ParameterService.Ins.SetRuleValue(Rules.YEAR_PUBLICATION_PERIOD, PublishYear);

            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK);
        }

    }
}
