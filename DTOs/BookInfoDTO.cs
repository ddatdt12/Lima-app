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
        public int status { get; set; }
        //1 - có thể mượn; 0 - đang mượn; -1: đã hỏng
        public BookDTO Book { get; set; }
    }
}
