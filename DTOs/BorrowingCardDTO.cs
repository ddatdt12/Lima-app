using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BorrowingCardDTO
    {
        public string id { get; set; }
        public string readerCardId { get; set; }
        public System.DateTime borrowingDate { get; set; }
        public System.DateTime dueDate { get; set; }
        public string employeeId { get; set; }
        public string bookInfoId { get; set; }
        public int numberOfDelayReturnDays{ get; set; }

        public BookInfoDTO bookInfo { get; set; }
        public ReturnCardDTO returnCard { get; set; }
        public EmployeeDTO employee { get; set; }
        public ReaderCardDTO readerCard { get; set; }

    }
}
