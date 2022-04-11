using System;

namespace LibraryManagement.DTOs
{
   public class FineReceiptDTO
    {
        public string id { get; set; }
        public int amount { get; set; }
        public System.DateTime createdAt { get; set; }
        public string employeeId { get; set; }
        public string readerCardId { get; set; }
        public EmployeeDTO employee { get; set; }
        public ReaderCardDTO readerCard { get; set; }
    }
}
