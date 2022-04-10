using LibraryManagement.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private DateTime _StartDate;
        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; OnPropertyChanged(); }
        }

        private DateTime _FinishDate;
        public DateTime FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; OnPropertyChanged(); }
        }
        private DateTime _GetCurrentDate;
        public DateTime GetCurrentDate
        {
            get { return _GetCurrentDate; }
            set { _GetCurrentDate = value; OnPropertyChanged(); }
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

        private String _ReaderCode;
        public String ReaderCode
        {
            get { return _ReaderCode; }
            set { _ReaderCode = value; OnPropertyChanged(); }
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

        private ObservableCollection<String> _ListGenre;

        public ObservableCollection<String> ListGenre
        {
            get { return _ListGenre; }
            set { _ListGenre = value; OnPropertyChanged(); }
        }

        public ICommand CheckedSexCM { get; set; }
    }
}
