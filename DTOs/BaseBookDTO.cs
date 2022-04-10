using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BaseBookDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public int genreId { get; set; }
        public bool isDeleted { get; set; }

        public GenreDTO genre { get; set; }
        public List<AuthorDTO> authors { get; set; }
        public List<BookDTO> books { get; set; }

    }
}
