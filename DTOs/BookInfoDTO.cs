using LibraryManagement.ViewModels;

namespace LibraryManagement.DTOs
{
    public class BookInfoDTO : BaseViewModel
    {
        public string id { get; set; }
        public string BookId { get; set; }
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }
        //1 - có thể mượn; 0 - đang mượn; -1: đã hỏng
        public BookDTO Book { get; set; }
    }
}
