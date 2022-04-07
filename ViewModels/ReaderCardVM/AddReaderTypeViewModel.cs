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

        public System.Threading.Tasks.Task OpenAddReaderWindow(System.Windows.Window p)
        {
            ReaderCardDTO reader = new ReaderCardDTO();
            reader.name = Name;
            reader.address = Adress;
            reader.employeeId = "NV0001";
            var findIdReader = ListReaderType.FirstOrDefault(s => s.name == ReaderType);
            reader.readerTypeId = ListReaderType[ListReaderType.IndexOf(findIdReader)].id;
            reader.totalFine = 100;
            reader.email = Email;
            reader.createdAt = StartDate;
            reader.expiryDate = FinishDate;
            reader.gender = Sex;
            reader.birthDate = Birthday;
            (bool Successful, string message) = ReaderService.Ins.CreateNewReaderCard(reader);
            ListReaderCard.Add(reader);
            p.Close();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
