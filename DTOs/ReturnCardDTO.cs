using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class ReturnCardDTO
    {
        public string id { get; set; }
        public string borrowingCardId { get; set; }
        public System.DateTime returnedDate { get; set; }
        public string employeeId { get; set; }
        public int fine { get; set; }

        public BorrowingCardDTO borrowingCard { get; set; }
        public EmployeeDTO employee { get; set; }

    }
}
