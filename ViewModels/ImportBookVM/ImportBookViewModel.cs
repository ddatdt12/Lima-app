using LibraryManagement.ViewModels;
using LibraryManagement.Views.ImportBookPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LibraryManagement.Views.ImportBook;

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





        #region COMMAND
        public ICommand IncreaseQuantityCM { get; set; }
        public ICommand DecreaseQuantityCM { get; set; }
        public ICommand DeleteSelectedBookCM { get; set; }
        public ICommand QuantityChangedCM { get; set; }
        public ICommand OpenImportWindowCM { get; set; }
        public ICommand AddBookCM { get; set; }
        public ICommand AddBookFromSearchCM { get; set; }

        #endregion



        public ImportBookViewModel()
        {
            ImportBookList = new ObservableCollection<Books>();

            ImportBookList.Add(new Books("sach 1", 2, 0));
            ImportBookList.Add(new Books("sach 1", 2, 0));
            ImportBookList.Add(new Books("sach 1", 2, 0));
            ImportBookList.Add(new Books("sach 1", 2, 0));
            ImportBookList.Add(new Books("sach 1", 2, 0));
            ImportBookList.Add(new Books("sach 1", 2, 0));


            IncreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedItem.SL++;
                SelectedItem.CalculateTotal();
            });
            DecreaseQuantityCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem.SL != 0)
                    SelectedItem.SL--;
                SelectedItem.CalculateTotal();
            });
            QuantityChangedCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                SelectedItem.CalculateTotal();
            });
            DeleteSelectedBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem != null)
                    ImportBookList.Remove(SelectedItem);
            });
            OpenImportWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                name = null;
                price = "0";

                ImportBookWindow w = new ImportBookWindow();
                w.ShowDialog();

            });
            AddBookCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ImportBookList.Add(new Books(name, 1, 5000));
                p.Close();
            });
            AddBookFromSearchCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ImportBookList.Add(new Books(name, 1, 5000));
            });
        }



        public class Books : BaseViewModel
        {
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
                set { price = value; }
            }

            private int total;
            public int Total
            {
                get { return total; }
                set { total = value; OnPropertyChanged(); }
            }


            public Books(string name, int sl, int price)
            {
                this.name = name;
                this.sl = sl;
                this.price = price;
                this.total = sl * price;

            }

            public void CalculateTotal()
            {
                Total = price * sl;
            }

        }
    }
}
