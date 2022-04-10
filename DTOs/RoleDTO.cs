using System.Collections.Generic;

namespace LibraryManagement.DTOs
{
   public class RoleDTO
    {
        public int id { get; set; }
        public string name{ get; set; }
        public bool isDeleted { get; set; }

        public List<RoleDetailDTO> roleDetaislList { get; set; }
    }
}
