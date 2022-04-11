using System;

namespace LibraryManagement.DTOs
{
   public class EmployeeDTO
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? birthDate { get; set; }
        public string gender { get; set; }
        public DateTime? startingDate { get; set; }
        public bool isDeleted { get; set; }

        public int accountId { get; set; }

        public AccountDTO account { get; set; }
    }
}
