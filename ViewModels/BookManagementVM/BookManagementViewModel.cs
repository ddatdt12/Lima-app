using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.BookManagement;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace LibraryManagement.ViewModel.BookManagementVM
{
    public class BookManagementViewModel : BaseViewModel
    {

        private ObservableCollection<BookDTO> bookList;
        public ObservableCollection<BookDTO> BookList
        {
            get { return bookList; }
            set { bookList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInfoDTO> bookInforList;
        public ObservableCollection<BookInfoDTO> BookInforList
        {
            get { return bookInforList; }
            set { bookInforList = value; OnPropertyChanged(); }
        }


        private List<GenreDTO> listGenre;
        public List<GenreDTO> ListGenre
        {
            get { return listGenre; }
            set { listGenre = value; OnPropertyChanged(); }
        }

        private List<AuthorDTO> listAuthor;
        public List<AuthorDTO> ListAuthor
        {
            get { return listAuthor; }
            set { listAuthor = value; OnPropertyChanged(); }
        }

        private GenreDTO selectedGenre;
        public GenreDTO SelectedGenre
        {
            get { return selectedGenre; }
            set { selectedGenre = value; OnPropertyChanged(); }
        }

        private BookDTO selectedItem;
        public BookDTO SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private BookInfoDTO selectedBookInfor;
        public BookInfoDTO SelectedBookInfor
        {
            get { return selectedBookInfor; }
            set { selectedBookInfor = value; OnPropertyChanged(); }
        }


        #region BINDING
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private AuthorDTO author;
        public AuthorDTO Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged(); }
        }

        private GenreDTO genre;
        public GenreDTO Genre
        {
            get { return genre; }
            set { genre = value; OnPropertyChanged(); }
        }

        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; OnPropertyChanged(); }
        }

        private int yearPublish;
        public int YearPublish
        {
            get { return yearPublish; }
            set { yearPublish = value; OnPropertyChanged(); }
        }

        #endregion





        #region COMMAND
        public ICommand FirstLoadCM { get; set; }
        public ICommand GenreFilterCM { get; set; }
        public ICommand DeleteBookCM { get; set; }
        public ICommand DeleteBookInforCM { get; set; }
        public ICommand UpdateBookCM { get; set; }
        public ICommand OpenEditBookCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        #endregion


        public BookManagementViewModel()
        {

            BookList = new ObservableCollection<BookDTO>(BookService.Ins.GetAllBook());

            FirstLoadCM = new RelayCommand<System.Windows.Controls.ComboBox>((p) => { return true; }, (p) =>
            {
                GenreDTO allGenre = new GenreDTO();
                allGenre.name = "Toàn bộ";
                ListGenre = new List<GenreDTO>();
                ListGenre.Add(allGenre);
                ListGenre.AddRange(GenreService.Ins.GetAllGenre());
                p.ItemsSource = ListGenre;

                ListAuthor = new List<AuthorDTO>(AuthorService.Ins.GetAllAuthor());

            });
            GenreFilterCM = new RelayCommand<System.Windows.Controls.ListView>((p) => { return true; }, (p) =>
            {
                if (SelectedGenre is null) return;

                if (SelectedGenre.name == "Toàn bộ")
                {
                    p.ItemsSource = BookList;
                    return;
                }


                ObservableCollection<BookDTO> filterList = new ObservableCollection<BookDTO>();
                foreach (var item in BookList)
                {
                    if (item.genre.name == SelectedGenre.name)
                        filterList.Add(item);
                }
                p.ItemsSource = filterList;
            });
            DeleteBookCM = new RelayCommand<System.Windows.Controls.ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                if (MessageBox.Show("Bạn có chắc muốn xoá sách này không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    BookDTO selectedBook = p.SelectedItem as BookDTO;

                    try
                    {
                        (bool isSuc, string res) = BookService.Ins.DeleteBook(selectedBook.id);

                        if (isSuc)
                            BookList.Remove(selectedBook);
                        MessageBox.Show(res);

                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                }
                else
                    return;
            });
            DeleteBookInforCM = new RelayCommand<System.Windows.Controls.ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                if (MessageBox.Show("Bạn có chắc muốn xoá sách này không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    BookInfoDTO selectedBook = p.SelectedItem as BookInfoDTO;

                    try
                    {
                        (bool isSuc, string res) = BookService.Ins.DeleteBookInfo(selectedBook.id);

                        if (isSuc)
                        {
                            BookInforList.Remove(selectedBook);
                        }
                        MessageBox.Show(res);

                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                }
                else
                    return;
            });
            UpdateBookCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                BookDTO tempBook = new BookDTO
                {
                    name = Name,
                    authorId = Author.id,
                    genreId = Genre.id,
                    publisher = Publisher,
                    yearOfPublication = YearPublish,
                    id = SelectedItem.id,
                };

                try
                {
                    (bool isS, string mes) = BookService.Ins.UpdateBook(tempBook);

                    if (isS)
                    {
                        BookList = new ObservableCollection<BookDTO>(BookService.Ins.GetAllBook());
                        MessageBox.Show(mes);
                        p.Close();
                    }
                    else
                        MessageBox.Show(mes);

                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            });
            OpenEditBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem is null) return;

                BookInforWindow w = new BookInforWindow();

                BookInforList = new ObservableCollection<BookInfoDTO>(SelectedItem.bookInfoes);
                Name = SelectedItem.name;
                Author = SelectedItem.author;
                Genre = SelectedItem.genre;
                Publisher = SelectedItem.publisher;
                YearPublish = SelectedItem.yearOfPublication;

                w.ShowDialog();

            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                BookList = new ObservableCollection<BookDTO>(BookService.Ins.GetAllBook());
                p.Close();
            });

        }
    }
}
