using LibraryManagement.DTOs;
using LibraryManagement.ViewModels.HistoryManagementVM;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagement.Views.HistoryManagement
{
    public partial class BorrowReturnPage : Page
    {
        public BorrowReturnPage()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-Vn");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (HistoryViewModel)DataContext;
            if (viewModel.SelectedDateChangedCM.CanExecute(null))
                viewModel.SelectedDateChangedCM.Execute(null);
            ComboBoxItem str = cbb.SelectedItem as ComboBoxItem;
            if (str.Content.ToString() == "Toàn bộ")
            {
                datePicker.IsEnabled = false;
                if (btn2 != null && btn1 != null)
                {
                    btn2.Visibility = System.Windows.Visibility.Visible;
                    btn1.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            else
            {
                datePicker.IsEnabled = true;
                btn1.Visibility = System.Windows.Visibility.Visible;
                btn2.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lv.ItemsSource is null) return;
            CollectionViewSource.GetDefaultView(lv.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lv.ItemsSource);
            view.Filter = Filter;
        }
        private bool Filter(object item)
        {
            if (string.IsNullOrEmpty(searchBox.Text))
                return true;

            switch (FilterBox.SelectedIndex)
            {
                case 0:
                    return ((item as BorrowingCardDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case 1:
                    return ((item as BorrowingCardDTO).bookInfo.Book.baseBook.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case 2:
                    return ((item as BorrowingCardDTO).employee.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    return ((item as BorrowingCardDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }
    }
}
