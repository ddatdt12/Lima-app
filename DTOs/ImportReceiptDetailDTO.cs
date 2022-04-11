using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class ImportReceiptDetailDTO
    {
        public string importReceiptId { get; set; }
        public string bookId { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }

        public BookDTO book { get; set; }
    }
}
