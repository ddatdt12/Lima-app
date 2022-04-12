using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public  class DelayReturnBookReportDTO
    {
        public int id { get; set; }
        public System.DateTime reportDate { get; set; }
        public List<DelayReturnBookReportDetailDTO> delayReturnBookReportDetails { get; set; }
    }

    public  class DelayReturnBookReportDetailDTO
    {
        public int delayReturnBookReportId { get; set; }
        public string borrowingReturnCardId { get; set; }
        public int numberOfDelayReturn { get; set; }
        public BorrowingCardDTO BorrowingReturnCard { get; set; }
        public DelayReturnBookReportDTO DelayReturnBookReport { get; set; }
    }
}
