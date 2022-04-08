using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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

        private DateTime _Birthday;
        public DateTime Birthday
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


        public System.Threading.Tasks.Task AddReader(System.Windows.Window p)
        {
            ReaderCardDTO reader = new ReaderCardDTO();
            reader.name = Name; 
            reader.address = Adress;
            reader.employeeId = "NV0001";
            reader.readerTypeId = (ListReaderType.FirstOrDefault(s => s.name == ReaderType)).id;
            reader.totalFine = 100;
            reader.email = Email;
            reader.createdAt = StartDate;
            reader.expiryDate = FinishDate;
            reader.gender = Sex;
            reader.birthDate = Birthday;
            (bool Successful, string message) = ReaderService.Ins.CreateNewReaderCard(reader);
            ListReaderCard.Add(reader);
            p.Close();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
