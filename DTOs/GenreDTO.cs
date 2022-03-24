using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
   public class GenreDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
