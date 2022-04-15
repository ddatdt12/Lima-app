using LibraryManagement.DTOs;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagement.View.ReaderCard
{
    public partial class ReaderCardPage : Page
    {
        public ReaderCardPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvReader.ItemsSource);
            view.Filter = Filter;
            CollectionViewSource.GetDefaultView(lvReader.ItemsSource).Refresh();
        }
        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(SearchTbx.Text))
                return true;

            switch (FilterCbb.SelectedValue)
            {
                case "Mã độc giả":
                    return ((item as ReaderCardDTO).id.ToString().IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case "Tên độc giả":
                    return ((item as ReaderCardDTO).name.IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    return ((item as ReaderCardDTO).id.ToString().IndexOf(SearchTbx.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }
    }
}
