using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private double finesAmount;
        public double FinesAmount
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
            MinAge = 5;
            MaxAge = 65;
            ExpiredDay = 6;
            MaxRent = 5;
            ReturnDate = 7;
            FinesAmount = 1000;
            PublishYear = 8;
        }

    }
}
