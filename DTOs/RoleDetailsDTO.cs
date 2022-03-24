using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
   public class RoleDetailsDTO
    {
        public int roleId { get; set; }
        public int permission { get; set; }
        public bool isPermitted { get; set; }
    }
}
