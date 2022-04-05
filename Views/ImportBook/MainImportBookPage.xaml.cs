using LibraryManagement.DTOs;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LibraryManagement.Views.ImportBookPage
{
    public partial class MainImportBookPage : Page
    {

        private static ObservableCollection<BookDTO> allBookList;
        public static ObservableCollection<BookDTO> AllBookList
        {
            get { return allBookList; }
            set { allBookList = value; }
        }

        public MainImportBookPage()
        {
            InitializeComponent();

            AllBookList = new ObservableCollection<BookDTO>(BookService.Ins.GetAllBook());
        }


        private void quantitybox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sd = sender as TextBox;

            if (sd.Text.Length <= 0)
                sd.Text = "1";

        }

        private static readonly Regex _regex = new Regex("[^0-9]"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void quantitybox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchList.ItemsSource is null)
                searchList.ItemsSource = AllBookList;
            CollectionViewSource.GetDefaultView(searchList.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(AllBookList);
            view.Filter = Filter;

            if (searchBox.Text.Length == 0)
            {
                searchList.ItemsSource = null;
            }
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(searchBox.Text))
                return true;

            BookDTO it = item as BookDTO;
            BaseBookDTO bb = BaseBookService.Ins.GetBookDetail(it.baseBookId);

            return (bb.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}