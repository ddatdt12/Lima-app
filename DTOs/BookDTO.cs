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
        public int yearOfPublication { get; set; }
        public string publisher { get; set; }
        public int quantity { get; set; }

        public List<BookInfoDTO> bookInfoes { get; set; }
        public string baseBookId { get; set; }
        public BaseBookDTO BaseBook { get; set; }

        public bool isNew { get; set; }
    }
}
