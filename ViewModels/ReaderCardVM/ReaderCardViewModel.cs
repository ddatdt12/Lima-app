using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private ObservableCollection<ReaderCardDTO> _listReaderCard;
        public ObservableCollection<ReaderCardDTO> ListReaderCard
        {
            get { return _listReaderCard;}
            set { _listReaderCard = value;OnPropertyChanged(); }
        }

        private ObservableCollection<String> _ListReaderType;

        public ObservableCollection<String> ListReaderType
        {
            get { return _ListReaderType; }
            set { _ListReaderType = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ReaderTypeDTO> ListGenre;

        #region
        public ICommand SelectedDateCM { get; set; }
        public ICommand AddReaderCard { get; set; }
        #endregion

        public ReaderCardViewModel()
        {
            ListReaderCard = new ObservableCollection<ReaderCardDTO>(ReaderService.Ins.GetAllReaderCards());
            ListGenre = new ObservableCollection<ReaderTypeDTO>(ReaderTypeService.Ins.GetAllReaderTypes());
            ListReaderType = new ObservableCollection<String>();
            foreach (ReaderTypeDTO readerType in ListGenre)
            {
                ListReaderType.Add(readerType.name);
            }
            StartDate = DateTime.Now;
            FinishDate = StartDate.AddDays(30);
            SelectedDateCM = new RelayCommand<object>((p) => { return true; },(p) =>
            {
                FinishDate = StartDate.AddDays(30);
            });

            AddReaderCard = new RelayCommand<System.Windows.Window>((p) => { return true; }, async (p) =>
            {
                await OpenAddReaderWindow(p);
            });

;        }
    }
}
