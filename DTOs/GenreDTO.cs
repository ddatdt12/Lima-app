using System.Collections.Generic;

namespace LibraryManagement.DTOs
{
   public class GenreDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<BookDTO> books { get; set; }
    }
}
