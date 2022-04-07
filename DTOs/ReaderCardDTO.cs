using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class ReaderCardDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public DateTime birthDate { get; set; }
        public string gender { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime expiryDate { get; set; }
        public bool isDeleted { get; set; }
        public int totalFine { get; set; }
        public int readerTypeId { get; set; }
        public string employeeId { get; set; }

        public EmployeeDTO employee { get; set; }
        public ReaderTypeDTO readerType { get; set; }
        public AccountDTO account { get; set; }


        public bool haveDelayBook { get; set; }
        public int numberOfBorrowingBooks { get; set; }
        
    }
}
