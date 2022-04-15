using System.Windows.Input;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public partial class StaffManagementViewModel : BaseViewModel
    {
        private string _RePassword;

        public string RePassword
        {
            get { return _RePassword; }
            set { _RePassword = value; }
        }

        public ICommand GetRePasswordCM { get; set; }
        public ICommand ChangePassCM { get; set; }
    }
}
