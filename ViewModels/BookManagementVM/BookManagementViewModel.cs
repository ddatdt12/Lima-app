using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LibraryManagement.ViewModel.BookManagementVM
{
    public class BookManagementViewModel : BaseViewModel
    {

        private ObservableCollection<string> bookList;
        public ObservableCollection<string> BookList
        {
            get { return bookList; }
            set { bookList = value; OnPropertyChanged(); }
        }


        public BookManagementViewModel()
        {

            BookList = new ObservableCollection<string>();
            BookList.Add("");
            BookList.Add("");
            BookList.Add("");
            BookList.Add("");

        }

    }
}
