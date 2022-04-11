using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels.StaffManagementVM
{
    public partial class StaffManagementViewModel
    {
        private ObservableCollection<EmployeeDTO> _ListStaff;
        public ObservableCollection<EmployeeDTO> ListStaff
        {
            get { return _ListStaff; }
            set { _ListStaff = value; }
        }


        public StaffManagementViewModel()
        {
            ListStaff = new ObservableCollection<EmployeeDTO>(EmployeeService.Ins.GetAllEmployees());
        }
    }
}
