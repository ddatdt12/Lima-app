using System;


namespace LibraryManagement.DTOs
{
    public class ReturnCardDTO
    {
        public string id { get; set; }
        public string borrowingCardId { get; set; }
        public DateTime returnedDate { get; set; }
        public string employeeId { get; set; }
        public int fine { get; set; }

        public BorrowingCardDTO borrowingCard { get; set; }
        public EmployeeDTO employee { get; set; }

    }
}
