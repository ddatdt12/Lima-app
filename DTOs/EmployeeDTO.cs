using System;

namespace LibraryManagement.DTOs
{
   public class EmployeeDTO
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public DateTime? birthDate { get; set; }
        public string gender { get; set; }
        public DateTime? startingDate { get; set; }
        public int roleId { get; set; }
        public bool isDeleted { get; set; }
        public  RoleDTO Role { get; set; }
    }
}
