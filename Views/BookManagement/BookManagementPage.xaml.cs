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
    /// <summary>
    /// Interaction logic for BookManagementPage.xaml
    /// </summary>
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
                    return ((item as BookDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tên sách":
                    return ((item as BookDTO).name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Thể loại":
                    return ((item as BookDTO).genre.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tác giả":
                    return ((item as BookDTO).author.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    return ((item as BookDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }
    }
}
