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

        private ImportReceiptDetailDTO selectedItem;
        public ImportReceiptDetailDTO SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }

        ImportBookWindow importWindow;



        private ObservableCollection<ImportReceiptDetailDTO> importBookList;
        public ObservableCollection<ImportReceiptDetailDTO> ImportBookList
        {
            get { return importBookList; }
            set { importBookList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BaseBookDTO> baseBookList;
        public ObservableCollection<BaseBookDTO> BaseBookList
        {
            get { return baseBookList; }
            set { baseBookList = value; }
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



        private string basename;
        public string BaseName
        {
            get { return basename; }
            set { basename = value; OnPropertyChanged(); }
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




        private BaseBookDTO selectedBaseBook;
        public BaseBookDTO SelectedBaseBook
        {
            get { return selectedBaseBook; }
            set { selectedBaseBook = value; OnPropertyChanged(); }
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

        private int? unitprice;
        public int? UnitPrice
        {
            get { return unitprice; }
            set { unitprice = value; OnPropertyChanged(); }
        }

        private int? price;
        public int? Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        private int? quantity;
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(); }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; OnPropertyChanged(); }
        }

        private DateTime? createAt;
        public DateTime? CreateAt
        {
            get { return createAt; }
            set { createAt = value; OnPropertyChanged(); }
        }



        private ObservableCollection<AuthorDTO> baseAuthor;
        public ObservableCollection<AuthorDTO> BaseAuthor
        {
            get { return baseAuthor; }
            set { baseAuthor = value; OnPropertyChanged(); }
        }





        private int totalQuantity;
        public int TotalQuantity
        {
            get { return totalQuantity; }
            set { totalQuantity = value; OnPropertyChanged(); }
        }

        private int totalReceiptPrice;
        public int TotalReceiptPrice
        {
            get { return totalReceiptPrice; }
            set { totalReceiptPrice = value; OnPropertyChanged(); }
        }




        #region COMMAND
        public ICommand IncreaseQuantityCM { get; set; }
        public ICommand DecreaseQuantityCM { get; set; }
        public ICommand DeleteSelectedBookCM { get; set; }
        public ICommand QuantityChangedCM { get; set; }
        public ICommand OpenImportWindowCM2 { get; set; }
        public ICommand AddBookFromSearchCM { get; set; }
        public ICommand ImportBookCM { get; set; }
        public ICommand OpenImportWindowCM { get; set; }
        public ICommand AddBookCM { get; set; }
        public ICommand AddNewGenreCM { get; set; }
        public ICommand OpenAddNewAuthorCM { get; set; }
        public ICommand OpenAddBaseBookPageCM { get; set; }
        public ICommand OpenAddBookPageCM { get; set; }
        public ICommand AddNewBaseBookCM { get; set; }
        public ICommand IncreaseBaseAuthorCM { get; set; }
        public ICommand DecreaseBaseAuthorCM { get; set; }
        public ICommand PriceChangedCM { get; set; }
        public ICommand CloseCM { get; set; }
        public ICommand FirstLoadCM { get; set; }

        #endregion



        public ImportBookViewModel()
        {
            ImportBookList = new ObservableCollection<ImportReceiptDetailDTO>();
            ListGenre = new ObservableCollection<GenreDTO>(GenreService.Ins.GetAllGenre());
            ListAuthor = new ObservableCollection<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
            TotalQuantity = 0;
            TotalReceiptPrice = 0;

            FirstLoadCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p != null)
                    importWindow = p as ImportBookWindow;
            });
            IncreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItem.quantity++;
                CalculateTotal(SelectedItem);
            });
            DecreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem.quantity != 0)
                {
                    SelectedItem.quantity--;
                    CalculateTotal(SelectedItem);
                }
            });
            QuantityChangedCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                CalculateTotal(SelectedItem);
                if (SelectedItem != null)
                {
                    TotalQuantity = 0;
                    foreach (var item in ImportBookList)
                    {
                        TotalQuantity += item.quantity;
                    }
                }
            });
            DeleteSelectedBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                {
                    TotalQuantity -= SelectedItem.quantity;
                    ImportBookList.Remove(SelectedItem);
                }
            });
            OpenAddBaseBookPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new AddBaseBookPage();
            });
            OpenAddBookPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                BaseBookList = new ObservableCollection<BaseBookDTO>(BaseBookService.Ins.GetAllBaseBook());
                p.Content = new AddBookPage();
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

                ImportReceiptDetailDTO newRCD = new ImportReceiptDetailDTO
                {
                    unitPrice = (int)UnitPrice,
                    quantity = (int)Quantity,
                    book = new BookDTO
                    {
                        baseBookId = SelectedBaseBook.id,
                        baseBook = SelectedBaseBook,
                        yearOfPublication = (int)YearPublish,
                        publisher = Publisher,
                        isNew = true,
                    },
                    unitTotal = (int)(UnitPrice * Quantity),
                };

                ImportBookList.Add(newRCD);
                TotalQuantity += newRCD.quantity;
                TotalReceiptPrice += newRCD.unitTotal;

                importWindow.Close();
            });
            AddBookFromSearchCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                if (ImportBookList.Count > 0)
                    foreach (var item in ImportBookList)
                        if (item.book.id == ((BookDTO)p.SelectedItem).id) return;

                BookDTO slt = p.SelectedItem as BookDTO;

                ImportReceiptDetailDTO newRCD = new ImportReceiptDetailDTO
                {
                    unitPrice = 0,
                    quantity = 1,
                    book = new BookDTO
                    {
                        id = slt.id,
                        baseBook = slt.baseBook,
                        yearOfPublication = slt.yearOfPublication,
                        isNew = false,
                    }
                };
                ImportBookList.Add(newRCD);
                TotalQuantity++;
            });
            OpenImportWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BaseName = null;
                Genre = null;
                BaseAuthor = null;
                SelectedBaseBook = null;
                UnitPrice = null;
                Price = null;
                Quantity = null;
                Publisher = null;
                YearPublish = DateTime.Now.Year;
                BaseAuthor = new ObservableCollection<AuthorDTO>();

                ImportBookWindow.PreEnterBaseBook = "";
                AddBaseBookPage.PreEnterBaseBook = "";
                ImportBookWindow w = new ImportBookWindow();
                w.ShowDialog();

            });
            OpenImportWindowCM2 = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                ImportBookWindow.PreEnterBaseBook = p.Text;
                Genre = null;
                BaseAuthor = null;
                SelectedBaseBook = null;
                UnitPrice = null;
                Price = null;
                Quantity = null;
                Publisher = null;
                YearPublish = DateTime.Now.Year;
                BaseAuthor = new ObservableCollection<AuthorDTO>();
                ImportBookWindow w = new ImportBookWindow();

                w.ShowDialog();
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
            OpenAddNewAuthorCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                AddAuthorWindow w = new AddAuthorWindow();
                w.authorname.Text = p.Text;
                w.ShowDialog();
                ListAuthor = new ObservableCollection<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
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

                var importRC = new ImportReceiptDTO
                {
                    supplier = Supplier,
                    createdAt = (DateTime)CreateAt,
                    employeeId = "NV0001",
                    importReceiptDetailList = new List<ImportReceiptDetailDTO>(ImportBookList),
                };

                try
                {
                    (bool isS, string mes) = ImportService.Ins.CreateNewBookImportReceipt(importRC);

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
            CloseCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                importWindow.Close();
            });
            IncreaseBaseAuthorCM = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                AuthorDTO at = (AuthorDTO)p.SelectedItem;
                foreach (var item in BaseAuthor)
                {
                    if (item.id == at.id)
                    {
                        p.SelectedItem = null;
                        return;
                    }
                }
                BaseAuthor.Add(at);
                p.SelectedItem = null;

            });
            DecreaseBaseAuthorCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem is null) return;

                AuthorDTO at = (AuthorDTO)p.SelectedItem;
                BaseAuthor.Remove(at);

            });
            AddNewBaseBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (BaseAuthor.Count == 0 || Genre is null || string.IsNullOrEmpty(BaseName))
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin");
                    return;
                }

                try
                {

                    BaseBookDTO newbb = new BaseBookDTO();
                    newbb.authors = new List<AuthorDTO>();
                    foreach (var item in BaseAuthor)
                    {
                        newbb.authors.Add(item);
                    }
                    newbb.name = BaseName;
                    newbb.genreId = Genre.id;

                    (bool isS, string mess) = BaseBookService.Ins.CreateBaseBook(newbb);

                    if (isS)
                    {
                        BaseName = "";
                        Genre = null;
                        TxtGenre = null;
                        BaseAuthor = null;
                    }

                    MessageBox.Show(mess);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
            PriceChangedCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CalculateTotal(SelectedItem);
            });
        }


        public bool IsValidData()
        {
            return !(SelectedBaseBook is null) &&
                !string.IsNullOrEmpty(Publisher) &&
                !(YearPublish is null) &&
                !(UnitPrice is null) &&
                !(Price is null) &&
                !(Quantity is null);
        }
        public void PrintImportReiceipt()
        {

        }
        public void CalculateTotal(ImportReceiptDetailDTO item)
        {
            if (item is null) return;
            item.unitTotal = item.quantity * item.unitPrice;

            TotalReceiptPrice = 0;
            foreach (var it in ImportBookList)
            {
                TotalReceiptPrice += it.unitTotal;
            }
        }

    }
}
