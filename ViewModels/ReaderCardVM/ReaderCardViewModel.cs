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

        #region
        public ICommand SelectedDateCM { get; set; }
        public ICommand AddReaderCard { get; set; }
        #endregion

        public ReaderCardViewModel()
        {
            ListReaderCard = new ObservableCollection<ReaderCardDTO>(ReaderService.Ins.GetAllReaderCards());
            ListReaderType = new ObservableCollection<ReaderTypeDTO>(ReaderTypeService.Ins.GetAllReaderTypes());
            ListGenre = new ObservableCollection<string>();
            foreach (ReaderTypeDTO type in ReaderTypeService.Ins.GetAllReaderTypes())
            {
                ListGenre.Add(type.name);
            }
            StartDate = DateTime.Now;
            FinishDate = StartDate.AddDays(30);
            SelectedDateCM = new RelayCommand<object>((p) => { return true; },(p) =>
            {
                FinishDate = StartDate.AddDays(30);
            });

            AddReaderCard = new RelayCommand<System.Windows.Window>((p) => { return true; }, async (p) =>
            {
                AddReader(p);
            });
            AddReaderType = new RelayCommand<object>((p) => { return true; }, async (p) => 
            {
                AddGenre();
            });
;        }
    }
}
