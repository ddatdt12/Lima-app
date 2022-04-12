using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BorrowedGenreReportDTO
    {
        public BorrowedGenreReportDTO()
        {
        }
        public int id { get; set; }
        public System.DateTime reportDate { get; set; }
        public int totalNumberOfBorrowings { get; set; }
        public List<BorrowedGenreReportDetailDTO> borrowedGenreReportDetails { get; set; }

    }

    public  class BorrowedGenreReportDetailDTO
    {
        public int borrowedGenreReportId { get; set; }
        public int genreId { get; set; }
        public int NumberOfBorrowings { get; set; }
        public double rate { get; set; }

        public GenreDTO Genre { get; set; }
    }
}
