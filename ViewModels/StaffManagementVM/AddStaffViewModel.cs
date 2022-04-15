

using System;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(); }
        }

        private DateTime? _StartDate;
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; OnPropertyChanged(); }
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

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; OnPropertyChanged(); }
        }

        private string _Sex;
        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; OnPropertyChanged(); }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set { _Username = value; OnPropertyChanged(); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

        private string _Role;
        public string Role
        {
            get { return _Role; }
            set { _Role = value; OnPropertyChanged(); }
        }

        public ICommand CheckedSexCM { get; set; }
        public ICommand AddStaffCM { get; set; }
        public ICommand GetPasswordCommand { get; set; }
    }
}
