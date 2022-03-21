using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BookInfoDTO
    {
        public string id { get; set; }
        public string BookId { get; set; }
        public bool status { get; set; }

        public BookDTO Book { get; set; }
    }
}
