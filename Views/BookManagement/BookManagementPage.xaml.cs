using LibraryManagement.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagement.Views.BookManagement
{

    public partial class BookManagementPage : Page
    {
        public BookManagementPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(listview.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listview.ItemsSource);
            view.Filter = Filter;
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(searchBox.Text))
                return true;

            switch (FilterBox.Text)
            {
                case "Mã sách":
                    return ((item as BaseBookDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tên sách":
                    return ((item as BaseBookDTO).name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Thể loại":
                    return ((item as BaseBookDTO).genre.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tác giả":
                    {
                        string res = "";
                        BaseBookDTO bb = item as BaseBookDTO;
                        foreach (var it in bb.authors)
                        {
                            res += it.name + " ";
                        }
                        return (res.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                default:
                    return ((item as BaseBookDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }
    }
}
