using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class AccountDTO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int roleId { get; set; }
        public AccountType type { get; set; } // "ReaderCard" or "Employee"
        public RoleDTO role { get; set; }

    }
}
