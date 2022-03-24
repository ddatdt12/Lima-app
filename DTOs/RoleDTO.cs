using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
   public class RoleDTO
    {
        public int id { get; set; }
        public string position{ get; set; }
        public bool isDeleted { get; set; }

        public List<RoleDetailsDTO> roleDetaislList { get; set; }
    }
}
