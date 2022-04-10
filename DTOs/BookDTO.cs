using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DTOs
{
    public class BookDTO : BaseViewModel
    {
        public BookDTO()
        {
            isNew = false;
        }

        public string id { get; set; }

        private int _yearOfPublication;
        public int yearOfPublication
        {
            get { return _yearOfPublication; }
            set { _yearOfPublication = value; OnPropertyChanged(); }
        }

        private string _publisher;
        public string publisher
        {
            get { return _publisher; }
            set { _publisher = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int quantity
        {
            get { return _quantity; }
            set { _quantity = value; OnPropertyChanged(); }
        }

        public List<BookInfoDTO> bookInfoes { get; set; }
        public string baseBookId { get; set; }

        private BaseBookDTO _baseBook;
        public BaseBookDTO baseBook
        {
            get { return _baseBook; }
            set { _baseBook = value; OnPropertyChanged(); }
        }

        public bool isNew { get; set; }
    }
}
