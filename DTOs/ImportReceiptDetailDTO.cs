using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class ImportReceiptDetailDTO : BaseViewModel
    {
        public string importReceiptId { get; set; }
        public string bookId { get; set; }

        private int _unitPrice;
        public int unitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        public BookDTO book { get; set; }

        //this is for the UI only, do not include it in BE
        private int _unitTotal;
        public int unitTotal
        {
            get { return _unitTotal; }
            set { _unitTotal = value; OnPropertyChanged(); }
        }

    }
}
