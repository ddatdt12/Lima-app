using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.SettingVM
{
    public partial class SettingViewModel : BaseViewModel
    {
        private string oldPass;
        public string OldPass
        {
            get { return oldPass; }
            set { oldPass = value; OnPropertyChanged(); }
        }

        private string newPass;
        public string NewPass
        {
            get { return newPass; }
            set { newPass = value; OnPropertyChanged(); }
        }
        private string reEnterNewPass;
        public string ReEnterNewPass
        {
            get { return reEnterNewPass; }
            set { reEnterNewPass = value; OnPropertyChanged(); }
        }

        public ICommand ResetReaderPassCM { get; set; }
        public ICommand OldPasswordChangedCM { get; set; }
        public ICommand NewPasswordChangedCM { get; set; }
        public ICommand ReEnterPasswordChangedCM { get; set; }
    }
}
