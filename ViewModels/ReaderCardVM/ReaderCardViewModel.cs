using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.View.ReaderCard;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.ReaderCard;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        private ObservableCollection<ReaderCardDTO> _listReaderCard;
        public ObservableCollection<ReaderCardDTO> ListReaderCard
        {
            get { return _listReaderCard; }
            set { _listReaderCard = value; OnPropertyChanged(); }
        }

        private ReaderTypeDTO SelectedReaderType;
        #region
        public ICommand SelectedDateCM { get; set; }
        public ICommand AddReaderCardCM { get; set; }
        public ICommand OpenReaderTypeWindowCM { get; set; }
        public ICommand OpenAddReaderCardWindowCM { get; set; }
        public ICommand RemoveStatusTextBoxCM { get; set; }
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
            SelectedDateCM = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 FinishDate = StartDate.AddDays(30);
             });

            AddReaderCardCM = new RelayCommand<System.Windows.Window>((p) => { return true; }, (p) =>
             {
                 ReaderCardDTO reader = new ReaderCardDTO()
                 {
                     name = Name,
                     address = Adress,
                     employeeId = "NV0001",
                     readerTypeId = (ListReaderType.FirstOrDefault(s => s.name == ReaderType)).id,
                     email = Email,
                     createdAt = StartDate,
                     expiryDate = FinishDate,
                     gender = Sex,
                     birthDate = (DateTime)Birthday,
                 };

                 (bool Successful, string message) = ReaderService.Ins.CreateNewReaderCard(reader);
                 ListReaderCard.Add(reader);
                 p.Close();
             });
            AddReaderTypeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedReaderType != null)
                {
                    ReaderTypeDTO readerTypeDTO = new ReaderTypeDTO()
                    {
                        id = SelectedReaderType.id,
                        name = ReaderType
                    };
                    (bool isS, string mes) = ReaderTypeService.Ins.EditReaderType(readerTypeDTO);

                    if (isS)
                    {
                        MessageBox.Show(mes);
                    }
                    var readerTypeFound = ListReaderType.FirstOrDefault(s => s.id == readerTypeDTO.id);
                    ListReaderType[ListReaderType.IndexOf(readerTypeFound)] = readerTypeDTO;
                    return;
                }

                ReaderTypeDTO reader = new ReaderTypeDTO();
                reader.name = ReaderType;
                (bool Successful, string message) = ReaderTypeService.Ins.CreateReaderType(reader);
                ListReaderType.Add(reader);
            });
            CheckedSexCM = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
             {
                 Sex = p.Content.ToString();
             });
            SelectedReaderTypeChangedCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {

                if (p.SelectedItem != null)
                {
                    SelectedReaderType = p.SelectedItem as ReaderTypeDTO;
                    ReaderType = SelectedReaderType.name;
                }
            });
            SaveReaderTypeTbxCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                if (p != null)
                    ReaderTypeTxb = p;
            });
            OpenReaderTypeWindowCM = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddReaderTypeWindow addReaderTypeWindow = new AddReaderTypeWindow();
                addReaderTypeWindow.readertypeTbx.Text = null;
                addReaderTypeWindow.ShowDialog();
            });
            OpenAddReaderCardWindowCM = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddReaderCardWindow addReaderCardWindow = new AddReaderCardWindow();
                addReaderCardWindow.ShowDialog();
            });
            RemoveStatusTextBoxCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = null;
            });
        }
    }
}
