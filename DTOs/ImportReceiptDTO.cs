using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class ImportReceiptDTO
    {
        public string id { get; set; }
        public string supplier { get; set; }
        public int totalPrice { get; set; }
        public DateTime createdAt { get; set; }
        public string employeeId { get; set; }

        public EmployeeDTO employee { get; set; }
        public List<ImportReceiptDetailDTO> importReceiptDetailList{ get; set; }
    }
}
