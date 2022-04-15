using LibraryManagement.DTOs;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagement.Views.Genre_AuthorManagement
{
    public partial class MainManagementPage : Page
    {
        public MainManagementPage()
        {
            InitializeComponent();
        }
        private void searchBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(genrelistview.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(genrelistview.ItemsSource);
            view.Filter = Filter;
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(searchBox1.Text))
                return true;
            return ((item as GenreDTO).name.IndexOf(searchBox1.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void searchBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(authorlistview.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(authorlistview.ItemsSource);
            view.Filter = Filter2;
        }
        private bool Filter2(object item)
        {
            if (String.IsNullOrEmpty(searchBox2.Text))
                return true;
            return ((item as AuthorDTO).name.IndexOf(searchBox2.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

    }
}
