using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BookDTO
    {
        public BookDTO()
        {
            isNew = false;
        }

        public string id { get; set; }
        public string name { get; set; }
        public int genreId { get; set; }
        public int authorId { get; set; }
        public string yearOfPublication { get; set; }
        public string publisher { get; set; }
        public int quantity { get; set; }

        public List<BookInfoDTO> bookInfoes { get; set; }
        public virtual AuthorDTO author { get; set; }
        public GenreDTO genre { get; set; }

        //
        public bool isNew { get; set; }

    }
}
