using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {
        private ObservableCollection<string> roleList;
        public ObservableCollection<string> RoleList
        {
            get { return roleList; }
            set { roleList = value; OnPropertyChanged(); }
        }

        private string selectedRole;
        public string SelectedRole
        {
            get { return selectedRole; }
            set { selectedRole = value; OnPropertyChanged(); }
        }

        private bool isBookManagement;
        public bool IsBookManagement
        {
            get { return isBookManagement; }
            set { isBookManagement = value; OnPropertyChanged(); }
        }

        private bool isImportBook;
        public bool IsImportBook
        {
            get { return isImportBook; }
            set { isImportBook = value; OnPropertyChanged(); }
        }

        private bool isReaderManagement;
        public bool IsReaderManagement
        {
            get { return isReaderManagement; }
            set { isReaderManagement = value; OnPropertyChanged(); }
        }

        private bool isStaffManagement;
        public bool IsStaffManagement
        {
            get { return isStaffManagement; }
            set { isStaffManagement = value; OnPropertyChanged(); }
        }

        private bool isStatictis;
        public bool IsStatictis
        {
            get { return isStatictis; }
            set { isStatictis = value; OnPropertyChanged(); }
        }

        private bool isSetting;
        public bool IsSetting
        {
            get { return isSetting; }
            set { isSetting = value; OnPropertyChanged(); }
        }


        public ICommand EditRoleCM { get; set; }
        public ICommand DeleteRoleCM { get; set; }
        public ICommand OpenAddRoleCM { get; set; }
        public ICommand AddRoleCM { get; set; }
        public ICommand ApplyChangeRoleCM { get; set; }


    }
}
