using LibraryManagement.ViewModels;
using LibraryManagement.Views.ImportBookPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LibraryManagement.Views.ImportBook;
using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Views.Genre_AuthorManagement;

namespace LibraryManagement.ViewModel.ImportBookVM
{
    public class ImportBookViewModel : BaseViewModel
    {

        private Books selectedItem;
        public Books SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }



        private ObservableCollection<Books> importBookList;
        public ObservableCollection<Books> ImportBookList
        {
            get { return importBookList; }
            set { importBookList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<GenreDTO> listGenre;
        public ObservableCollection<GenreDTO> ListGenre
        {
            get { return listGenre; }
            set { listGenre = value; }
        }
        private ObservableCollection<AuthorDTO> listAuthor;
        public ObservableCollection<AuthorDTO> ListAuthor
        {
            get { return listAuthor; }
            set { listAuthor = value; OnPropertyChanged(); }
        }



        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        private string price;
        public string Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }
        private string txtGenre;
        public string TxtGenre
        {
            get { return txtGenre; }
            set { txtGenre = value; OnPropertyChanged(); }
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
        private int quantity;

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; OnPropertyChanged(); }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(); }
        }
        private string txtAuthor;
        public string TxtAuthor
        {
            get { return txtAuthor; }
            set { txtAuthor = value; OnPropertyChanged(); }
        }
        private AuthorDTO author;
        public AuthorDTO Author
        {
            get { return author; }
            set { author = value; OnPropertyChanged(); }
        }





        private int totalQuantity;
        public int TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; OnPropertyChanged(); }
        }





        #region COMMAND
        public ICommand IncreaseQuantityCM { get; set; }
        public ICommand DecreaseQuantityCM { get; set; }
        public ICommand DeleteSelectedBookCM { get; set; }
        public ICommand QuantityChangedCM { get; set; }
        public ICommand OpenImportWindowCM { get; set; }
        public ICommand OpenImportWindowCM2 { get; set; }
        public ICommand AddBookCM { get; set; }
        public ICommand AddBookFromSearchCM { get; set; }
        public ICommand AddNewGenreCM { get; set; }
        public ICommand AddNewAuthorCM { get; set; }
        public ICommand ImportBookCM { get; set; }

        #endregion



        public ImportBookViewModel()
        {
            ImportBookList = new ObservableCollection<Books>();
            ListGenre = new ObservableCollection<GenreDTO>(GenreService.Ins.GetAllGenre());
            ListAuthor = new ObservableCollection<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
            TotalQuantity = 0;


            IncreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItem.SL++;
                TotalQuantity++;
                SelectedItem.CalculateTotal();
            });
            DecreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem.SL != 0)
                    SelectedItem.SL--;
                TotalQuantity--;
                SelectedItem.CalculateTotal();
            });
            QuantityChangedCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                    SelectedItem.CalculateTotal();
            });
            DeleteSelectedBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    TotalQuantity -= SelectedItem.SL;
                    ImportBookList.Remove(SelectedItem);
                }
            });
            OpenImportWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Name = null;
                Genre = null;
                Author = null;
                Price = "1";
                Quantity = 1;
                Publisher = null;
                YearPublish = DateTime.Now.Year;

                ImportBookWindow w = new ImportBookWindow();
                w.ShowDialog();

            });
            OpenImportWindowCM2 = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Name = p.Text;
                Genre = null;
                Author = null;
                Price = "1";
                Quantity = 1;
                Publisher = null;
                YearPublish = DateTime.Now.Year;

                ImportBookWindow w = new ImportBookWindow();
                w.ShowDialog();

            });
            AddBookCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (!IsValidData())
                {
                    MessageBox.Show("Vui lòng điền đủ thông tin");
                    return;
                }
                if ((DateTime.Now.Year - YearPublish) > ParameterService.Ins.GetRuleValue(Utils.Rules.YEAR_PUBLICATION_PERIOD))
                {
                    MessageBox.Show("Năm xuất bản sách không thoả quy định");
                    return;
                }
                Books newBook = new Books
                {
                    Name = Name,
                    Publisher = Publisher,
                    SL = Quantity,
                    Author = Author,
                    YearPublish = YearPublish,
                    GenreId = Genre.id,
                    Price = int.Parse(Price),
                    IsNew = true,
                };

                ImportBookList.Add(newBook);
                TotalQuantity += newBook.SL;

                p.Close();
            });
            AddBookFromSearchCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                BookDTO SelectedBook = p.SelectedItem as BookDTO;

                foreach (var item in ImportBookList)
                    if (item.Id == SelectedBook.id)
                        return;

                Books importselectedBook = new Books
                {
                    Id = SelectedBook.id,
                    Name = SelectedBook.name,
                    Author = SelectedBook.author,
                    SL = 1,
                    IsNew = false,
                };
                ImportBookList.Add(importselectedBook);
                TotalQuantity++;

            });
            AddNewGenreCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(TxtGenre)) return;

                    if (MessageBox.Show("Bạn có muốn thêm thể loại này không?", "Thêm thể loại", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        GenreDTO newGenre = new GenreDTO { name = TxtGenre };
                        (bool isS, string mes) = GenreService.Ins.CreateNewGenre(newGenre);
                        if (isS)
                        {
                            ListGenre.Add(newGenre);
                            Genre = newGenre;
                        }

                        MessageBox.Show(mes);
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
            AddNewAuthorCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddAuthorWindow w = new AddAuthorWindow();
                w.authorname.Text = TxtAuthor;
                w.ShowDialog();
            });
            ImportBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (ImportBookList.Count == 0)
                {
                    MessageBox.Show("Danh sách nhập trống!");
                    return;
                }
                if (string.IsNullOrEmpty(Supplier))
                {
                    MessageBox.Show("Vui lòng nhập thông tin nhà cung cấp");
                    return;
                }

                List<BookDTO> importList = new List<BookDTO>();

                foreach (var item in ImportBookList)
                {
                    BookDTO tempBook = new BookDTO
                    {
                        name = item.Name,
                        publisher = item.Publisher,
                        quantity = item.SL,
                        authorId = item.Author.id,
                        yearOfPublication = item.YearPublish,
                        genreId = item.GenreId,
                        id = item.Id,
                        isNew = item.IsNew,
                        //UnitPrice = item.Price,
                    };

                    importList.Add(tempBook);
                }

                try
                {
                    (bool isS, string mes) = BookService.Ins.ImportBooks(importList);

                    if (isS)
                    {
                        ImportBookList.Clear();
                        MainImportBookPage.AllBookList = new ObservableCollection<BookDTO>(BookService.Ins.GetAllBook());
                        PrintImportReiceipt();
                    }
                    MessageBox.Show(mes);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }

            });
        }


        public bool IsValidData()
        {
            return !string.IsNullOrEmpty(Name) &&
                !(Author is null) &&
                !(Genre is null) &&
                !string.IsNullOrEmpty(Publisher) &&
                !string.IsNullOrEmpty(Price.Trim()) &&
                !string.IsNullOrEmpty(Quantity.ToString().Trim()) &&
                !string.IsNullOrEmpty(YearPublish.ToString().Trim());
        }
        public void PrintImportReiceipt()
        {

        }
        public class Books : BaseViewModel
        {
            private string id;
            public string Id
            {
                get { return id; }
                set { id = value; OnPropertyChanged(); }
            }


            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            private int sl;
            public int SL
            {
                get { return sl; }
                set { sl = value; OnPropertyChanged(); }
            }

            private int price;
            public int Price
            {
                get { return price; }
                set { price = value; OnPropertyChanged(); CalculateTotal(); }
            }

            private AuthorDTO author;
            public AuthorDTO Author
            {
                get { return author; }
                set { author = value; OnPropertyChanged(); }
            }

            private int genreId;
            public int GenreId
            {
                get { return genreId; }
                set { genreId = value; OnPropertyChanged(); }
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

            private bool isnew;
            public bool IsNew
            {
                get { return isnew; }
                set { isnew = value; OnPropertyChanged(); }
            }


            private int total;
            public int Total
            {
                get { return total; }
                set { total = value; OnPropertyChanged(); }
            }

            public Books() { }

            public void CalculateTotal()
            {
                Total = price * sl;
            }

        }
    }
}
