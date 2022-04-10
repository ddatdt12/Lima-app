using LibraryManagement.DTOs;
using LibraryManagement.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private ObservableCollection<ReaderTypeDTO> _ListReaderType;

        public ObservableCollection<ReaderTypeDTO> ListReaderType
        {
            get { return _ListReaderType; }
            set { _ListReaderType = value; OnPropertyChanged(); }
        }

        private string _ReaderType;
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; OnPropertyChanged(); }
        }

        private TextBox readerTypeTxb;
        public TextBox ReaderTypeTxb
        {
            get { return readerTypeTxb; }
            set { readerTypeTxb = value; OnPropertyChanged(); }
        }


        public ICommand AddReaderTypeCM { get; set; }
        public ICommand SelectedReaderTypeChangedCM { get; set; }
        public ICommand SaveReaderTypeTbxCM { get; set; }

    }
}
