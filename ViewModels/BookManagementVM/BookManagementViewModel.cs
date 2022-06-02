using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
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

        private ObservableCollection<BaseBookDTO> basebookList;
        public ObservableCollection<BaseBookDTO> BaseBookList
        {
            get { return basebookList; }
            set { basebookList = value; OnPropertyChanged(); }
        }

        private BaseBookDTO selectedItem;
        public BaseBookDTO SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private BaseBookDTO baseBookDetail;
        public BaseBookDTO BaseBookDetail
        {
            get { return baseBookDetail; }
            set { baseBookDetail = value; OnPropertyChanged(); }
        }

        private BookDTO selectedBaseBookDetail;
        public BookDTO SelectedBaseBookDetail
        {
            get { return selectedBaseBookDetail; }
            set { selectedBaseBookDetail = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInfoDTO> bookInforList;
        public ObservableCollection<BookInfoDTO> BookInforList
        {
            get { return bookInforList; }
            set { bookInforList = value; OnPropertyChanged(); }
        }

        private BookInfoDTO selectedBookInfor;
        public BookInfoDTO SelectedBookInfor
        {
            get { return selectedBookInfor; }
            set { selectedBookInfor = value; OnPropertyChanged(); }
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

        public static AccountDTO CurrentUser { get; set; }


        #region BINDING
        private string baseName;
        public string BaseName
        {
            get { return baseName; }
            set { baseName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AuthorDTO> baseauthor;
        public ObservableCollection<AuthorDTO> BaseAuthor
        {
            get { return baseauthor; }
            set { baseauthor = value; OnPropertyChanged(); }
        }

        private GenreDTO basegenre;
        public GenreDTO BaseGenre
        {
            get { return basegenre; }
            set { basegenre = value; OnPropertyChanged(); }
        }

        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set { publisher = value; OnPropertyChanged(); }
        }

        private int? yearPublish;
        public int? YearPublish
        {
            get { return yearPublish; }
            set { yearPublish = value; OnPropertyChanged(); }
        }

        private int? quantity;
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(); }
        }

        private bool _AvalibleChecked;
        public bool AvalibleChecked
        {
            get { return _AvalibleChecked; }
            set { _AvalibleChecked = value; OnPropertyChanged(); }
        }

        private bool _SpoiledChecked;
        public bool SpoiledChecked
        {
            get { return _SpoiledChecked; }
            set { _SpoiledChecked = value; OnPropertyChanged(); }
        }


        #endregion





        #region COMMAND
        public ICommand FirstLoadCM { get; set; }
        public ICommand GenreFilterCM { get; set; }
        public ICommand DeleteBaseBookCM { get; set; }
        public ICommand DeleteBaseBookDetailCM { get; set; }
        public ICommand DeleteBookInforCM { get; set; }
        public ICommand UpdateBookCM { get; set; }
        public ICommand OpenEditBookCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand IncreaseBaseAuthorCM { get; set; }
        public ICommand DecreaseBaseAuthorCM { get; set; }
        public ICommand SaveBaseBookCM { get; set; }
        public ICommand BindingBookInforCM { get; set; }
        public ICommand UpdateBookInforStatusCM { get; set; }
        public ICommand OpenBookInforStatusCM { get; set; }
        #endregion


        public BookManagementViewModel()
        {


            FirstLoadCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                GenreDTO allGenre = new GenreDTO();
                allGenre.name = "Toàn bộ";
                ListGenre = new List<GenreDTO>();
                ListGenre.Add(allGenre);
                ListGenre.AddRange(GenreService.Ins.GetAllGenre());
                p.ItemsSource = ListGenre;

                ListAuthor = new List<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
                BaseBookList = new ObservableCollection<BaseBookDTO>(BaseBookService.Ins.GetAllBaseBook());

            });
            GenreFilterCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (SelectedGenre is null) return;

                if (SelectedGenre.name == "Toàn bộ")
                {
                    p.ItemsSource = BaseBookList;
                    return;
                }


                ObservableCollection<BaseBookDTO> filterList = new ObservableCollection<BaseBookDTO>();
                foreach (var item in BaseBookList)
                {
                    if (item.genre.name == SelectedGenre.name)
                        filterList.Add(item);
                }
                p.ItemsSource = filterList;
            });
            DeleteBaseBookCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                if (MessageBox.Show("Bạn có chắc muốn xoá đầu sách này không?", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    BaseBookDTO selectedbaseBook = p.SelectedItem as BaseBookDTO;

                    try
                    {
                        (bool isSuc, string res) = BaseBookService.Ins.DeleteBaseBook(selectedbaseBook.id);

                        if (isSuc)
                        {

                            BaseBookList.Remove(selectedbaseBook);
                            MessageBox.Show(res, "Thông báo", MessageBoxButton.OK);
                            return;
                        }
                        MessageBox.Show(res, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
                else
                    return;
            });
            DeleteBaseBookDetailCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (SelectedBaseBookDetail is null) return;

                if (MessageBox.Show("Bạn có chắc muốn xoá sách này không?", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        (bool isSuc, string res) = BookService.Ins.DeleteBook(SelectedBaseBookDetail.id);

                        if (isSuc)
                        {
                            BaseBookDetail = BaseBookService.Ins.GetBookDetail(SelectedItem.id);
                            Publisher = null;
                            YearPublish = null;
                            Quantity = null;
                            BookInforList = null;
                            MessageBox.Show(res, "Thông báo", MessageBoxButton.OK);
                            return;
                        }
                        MessageBox.Show(res, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
                else
                    return;
            });
            DeleteBookInforCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (SelectedBookInfor is null) return;

                if (MessageBox.Show("Bạn có chắc muốn xoá sách này không?", "Cảnh báo", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        (bool isSuc, string res) = BookService.Ins.DeleteBookInfo(SelectedBookInfor.id);

                        if (isSuc)
                        {
                            BookInforList.Remove(SelectedBookInfor);
                            BaseBookDetail = BaseBookService.Ins.GetBookDetail(SelectedItem.id);
                            Quantity--;
                            MessageBox.Show(res, "Thông báo", MessageBoxButton.OK);
                            return;
                        }
                        MessageBox.Show(res, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);


                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }
                else
                    return;
            });
            UpdateBookCM = new RelayCommand<Window>((p) => { if (SelectedBaseBookDetail is null) return false; else return true; }, (p) =>
            {
                if (SelectedBaseBookDetail is null) return;
                if (string.IsNullOrEmpty(Publisher) || YearPublish is null)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                BookDTO tempBook = new BookDTO
                {
                    id = SelectedBaseBookDetail.id,
                    publisher = Publisher,
                    yearOfPublication = (int)YearPublish,
                };

                try
                {
                    (bool isS, string mes) = BookService.Ins.UpdateBook(tempBook);
                    if (isS)
                    {
                        string id = SelectedBaseBookDetail.id;
                        BaseBookDetail = BaseBookService.Ins.GetBookDetail(SelectedItem.id);
                        foreach (var item in BaseBookDetail.books)
                        {
                            if (item.id == id)
                                SelectedBaseBookDetail = item;
                        }
                        MessageBox.Show(mes, "Thông báo", MessageBoxButton.OK);
                        return;

                    }
                    MessageBox.Show(mes, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            OpenEditBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem is null) return;

                BookInforWindow w = new BookInforWindow();

                BaseAuthor = new ObservableCollection<AuthorDTO>(SelectedItem.authors);
                BaseName = SelectedItem.name;
                BaseGenre = SelectedItem.genre;
                BaseBookDetail = BaseBookService.Ins.GetBookDetail(SelectedItem.id);

                Publisher = "";
                YearPublish = null;
                Quantity = null;
                BookInforList = null;
                w.ShowDialog();

            });
            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                BaseBookList = new ObservableCollection<BaseBookDTO>(BaseBookService.Ins.GetAllBaseBook());
                p.Close();
            });
            IncreaseBaseAuthorCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                AuthorDTO it = p.SelectedItem as AuthorDTO;

                foreach (var item in BaseAuthor)
                {
                    if (item.name == it.name)
                    {
                        p.SelectedItem = null;
                        return;
                    }
                }

                BaseAuthor.Add(it);
                p.SelectedItem = null;

            });
            DecreaseBaseAuthorCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                AuthorDTO it = p.SelectedItem as AuthorDTO;
                BaseAuthor.Remove(it);
            });
            SaveBaseBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (BaseAuthor.Count == 0 || BaseGenre is null || string.IsNullOrEmpty(BaseName))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    List<AuthorDTO> list = new List<AuthorDTO>();
                    foreach (var item in BaseAuthor)
                    {
                        list.Add(item);
                    }

                    BaseBookDTO bb = new BaseBookDTO
                    {
                        id = SelectedItem.id,
                        name = BaseName,
                        genreId = BaseGenre.id,
                        authors = list,
                    };

                    (bool isS, string mess) = BaseBookService.Ins.UpdateBaseBook(bb);

                    if (isS)
                    {
                        MessageBox.Show(mess, "Thông báo", MessageBoxButton.OK);
                        return;
                    }
                    MessageBox.Show(mess, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            BindingBookInforCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBaseBookDetail is null) return;

                Publisher = SelectedBaseBookDetail.publisher;
                YearPublish = SelectedBaseBookDetail.yearOfPublication;
                Quantity = SelectedBaseBookDetail.quantity;
                BookInforList = new ObservableCollection<BookInfoDTO>(SelectedBaseBookDetail.bookInfoes);
            });
            UpdateBookInforStatusCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                try
                {
                    var status = (AvalibleChecked) ? BookInfoStatus.AVAILABLE : BookInfoStatus.SPOILED;
                    (bool isSuccess, string message) = BookService.Ins.UpdateBookInfoStatus(SelectedBookInfor.id, status);
                    if (isSuccess)
                    {
                        foreach (var item in BookInforList)
                        {
                            if (item.id == SelectedBookInfor.id)
                            {
                                item.status = (int)status;
                                break;
                            }
                        }
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                catch (System.Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
            OpenBookInforStatusCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedBookInfor is null) return;

                if (SelectedBookInfor.status == (int)BookInfoStatus.BORROWING)
                {
                    MessageBox.Show("Không thể sửa đổi cuốn sách này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (SelectedBookInfor.status == (int)BookInfoStatus.AVAILABLE)
                {
                    AvalibleChecked = true;
                    SpoiledChecked = false;
                }
                else
                {
                    SpoiledChecked = true;
                    AvalibleChecked = false;
                }

                BookInfoStatusWindow w = new BookInfoStatusWindow();
                w.ShowDialog();
            });
        }
    }
}
