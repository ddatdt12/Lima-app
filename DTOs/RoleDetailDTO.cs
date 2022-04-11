using LibraryManagement.ViewModels;

namespace LibraryManagement.DTOs
{
   public class RoleDetailDTO : BaseViewModel
    {
        public int roleId { get; set; }
        public int permissionId { get; set; }
        public string permissionName { get; set; }
        private bool _isPermitted;
        public bool isPermitted
        {
            get { return _isPermitted; }
            set { _isPermitted = value; OnPropertyChanged(); }
        }
    }
}
