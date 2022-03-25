using LibraryManagement.ViewModels;
using System;
using System.Collections.ObjectModel;

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

        private String _IdCard;
        public String IdCard
        {
            get { return _IdCard; }
            set { _IdCard = value; OnPropertyChanged(); }
        }

        private String _Birthday;
        public String Birthday
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

        private string _ReaderType;
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; OnPropertyChanged(); }
        }

        private ObservableCollection<String> _GenreList = new ObservableCollection<string>();

        public ObservableCollection<String> GenreList
        {
            get 
            {

                _GenreList.Add("Lê Hải Phong");
                _GenreList.Add("Trần Đình Khôi");
                _GenreList.Add("Đồ Thành Đạt");
                _GenreList.Add("Kiều Bá Dương");
                _GenreList.Add("Nguyễn Ngọc Trinh");
                return _GenreList; 
            }
            set { _GenreList = value; OnPropertyChanged(); }
        }

        public void CreatGenreList()
        {
            GenreList = new ObservableCollection<String>();
        }

        public System.Threading.Tasks.Task OpenAddReaderWindow(System.Windows.Window p)
        {
            ReaderCard reader = new ReaderCard();
            reader.id = ReaderCode;
            reader.name = Name;
            reader.idCard = IdCard;
            reader.type = ReaderType;
            reader.registrationDate = StartDate.ToString();
            reader.expirationDate = FinishDate.ToString();

            ListReaderCard.Add(reader);
            p.Close();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
