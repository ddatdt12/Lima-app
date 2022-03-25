using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public class Staff
    {
        public string idStaff { get; set; }
        public string nameStaff { get; set; }
        public string birthDay { get; set; }
        public string idCard { get; set; }
        public string sex { get; set; }
        public Staff()
        {
            idStaff = "ABCD";
            nameStaff = "Lê Hải Phong";
            birthDay = "20/04/2002";
            idCard = "212454483";
            sex = "Nam";
        }
    }
    public partial class StaffManagementViewModel
    {
        private ObservableCollection<Staff> _ListStaff = new ObservableCollection<Staff>();

        public ObservableCollection<Staff> ListStaff
        {
            get {
                for (int i = 0; i < 10; i++)
                {
                    _ListStaff.Add(new Staff());
                }
                return _ListStaff; 
            }
            set { _ListStaff = value; }
        }

    }
}
