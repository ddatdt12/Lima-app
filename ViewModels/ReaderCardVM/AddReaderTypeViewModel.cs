using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private ObservableCollection<ReaderTypeDTO> _ListReaderType;

        public ObservableCollection<ReaderTypeDTO> ListReaderType
        {
            get { return _ListReaderType; }
            set { _ListReaderType = value; }
        }

        private string _ReaderType;
        public string ReaderType
        {
            get { return _ReaderType; }
            set { _ReaderType = value; OnPropertyChanged(); }
        }

        public ICommand AddReaderType { get; set; }

        public System.Threading.Tasks.Task AddGenre()
        {
            ReaderTypeDTO reader = new ReaderTypeDTO();
            reader.id = 4;
            reader.name = ReaderType;
            (bool Successful, string message) = ReaderTypeService.Ins.CreateReaderType(reader);
            ListReaderType.Add(reader);
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
