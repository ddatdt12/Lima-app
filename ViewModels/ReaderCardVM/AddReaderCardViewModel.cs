using LibraryManagement.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private DateTime? _StartDate;
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; OnPropertyChanged(); }
        }

        private DateTime? _FinishDate;
        public DateTime? FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; OnPropertyChanged(); }
        }

        private String _Name;
        public String Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }

        private DateTime? _Birthday;
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; OnPropertyChanged(); }
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }

        private string _Adress;
        public string Adress
        {
            get { return _Adress; }
            set { _Adress = value; OnPropertyChanged(); }
        }

        private string _Sex;
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; OnPropertyChanged(); }
        }

        private string _ReaderType;
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; OnPropertyChanged(); }
        }

        public ICommand CheckedSexCM { get; set; }
        public ICommand AddReaderCardCM { get; set; }
    }
}
