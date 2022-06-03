using LibraryManagement.DTOs;
using LibraryManagement.ViewModels.HistoryManagementVM;
using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryManagement.Views.HistoryManagement
{
    public partial class ImportReceiptPage : Page
    {
        public ImportReceiptPage()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-Vn");
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
                    return ((item as ImportReceiptDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case 1:
                    return ((item as ImportReceiptDTO).supplier.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                case 2:
                    return ((item as ImportReceiptDTO).employee.name.IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
                default:
                    return ((item as ImportReceiptDTO).id.ToString().IndexOf(searchBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (HistoryViewModel)DataContext;
            if (viewModel.SelectedDateChangedCM.CanExecute(null))
                viewModel.SelectedDateChangedCM.Execute(null);
            ComboBoxItem str = cbb.SelectedItem as ComboBoxItem;
            if (str.Content.ToString() == "Toàn bộ")
            {
                datePickerBd.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                datePickerBd.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
