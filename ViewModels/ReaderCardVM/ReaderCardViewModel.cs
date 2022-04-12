using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.View.ReaderCard;
using LibraryManagement.ViewModels;
using LibraryManagement.Views;
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

        private ReaderCardDTO _SelectedItem;
        public ReaderCardDTO SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }


        private ReaderTypeDTO SelectedReaderType;


        #region
        public ICommand SelectedDateCM { get; set; }
        public ICommand OpenReaderTypeWindowCM { get; set; }
        public ICommand OpenAddReaderCardWindowCM { get; set; }
        public ICommand DeleteEditReaderCardCM { get; set; }
        #endregion


        public void ResetData()
        {
            SelectedItem = null;
            Name = null;
            Birthday = null;
            Email = null;
            Adress = null;
            ReaderType = null;
            StartDate = null;
            FinishDate = null;
        }


        public ReaderCardViewModel()
        {
            ListReaderCard = new ObservableCollection<ReaderCardDTO>(ReaderService.Ins.GetAllReaderCards());
            ListReaderType = new ObservableCollection<ReaderTypeDTO>(ReaderTypeService.Ins.GetAllReaderTypes());
            StartDate = DateTime.Now;
            FinishDate = StartDate?.AddMonths(ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD));
            SelectedDateCM = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 FinishDate = StartDate?.AddMonths(ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD));
             });

            AddReaderCardCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
             {
                 (bool isvalid, string error) = IsValidData();
                 if (isvalid)
                 {
                     ReaderCardDTO reader = new ReaderCardDTO()
                     {
                         name = Name,
                         address = Adress,
                         employeeId = "NV0001",
                         readerTypeId = (ListReaderType.FirstOrDefault(s => s.name == ReaderType)).id,
                         email = Email,
                         createdAt = (DateTime)StartDate,
                         expiryDate = (DateTime)FinishDate,
                         gender = Sex,
                         birthDate = (DateTime)Birthday,
                     };

                     (bool Successful, string message) = ReaderService.Ins.CreateNewReaderCard(reader);

                     if (Successful)
                     {
                         ListReaderCard.Add(reader);
                         p.Close();
                         MessageBoxCustom mb = new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);
                         mb.ShowDialog();
                     }
                     else
                     {
                         MessageBoxCustom mb = new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);
                         mb.ShowDialog();
                     }
                 }
                 else
                 {
                     MessageBoxCustom mb = new MessageBoxCustom("Lỗi", error, MessageType.Error, MessageButtons.OK);
                     mb.ShowDialog();
                 }
             });
            SelectedReaderTypeChangedCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (p.SelectedItem != null)
                {
                    SelectedReaderType = p.SelectedItem as ReaderTypeDTO;
                    tempReaderType = SelectedReaderType.name;
                }
            });
            RemoveStatusListViewCM = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                p.SelectedItem = null;
                tempReaderType = null;
            });
            OpenReaderTypeWindowCM = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddReaderTypeWindow addReaderTypeWindow = new AddReaderTypeWindow();
                ResetDataAddReaderTypeWindow();
                addReaderTypeWindow.ShowDialog();
            });
            AddReaderTypeCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedReaderType != null)
                {
                    ReaderTypeDTO readerTypeDTO = new ReaderTypeDTO()
                    {
                        id = SelectedReaderType.id,
                        name = tempReaderType
                    };
                    (bool isS, string mes) = ReaderTypeService.Ins.EditReaderType(readerTypeDTO);

                    if (isS)
                    {
                        var readerTypeFound = ListReaderType.FirstOrDefault(s => s.id == readerTypeDTO.id);
                        ListReaderType[ListReaderType.IndexOf(readerTypeFound)] = readerTypeDTO;
                        MessageBoxCustom mb = new MessageBoxCustom("Thông báo", mes, MessageType.Success, MessageButtons.OK);
                        mb.ShowDialog();
                        ResetDataAddReaderTypeWindow();
                        return;
                    }
                    else
                    {
                        MessageBoxCustom mb = new MessageBoxCustom("Lỗi", mes, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                        ResetDataAddReaderTypeWindow();
                        return;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(tempReaderType))
                    {
                        MessageBoxCustom mb = new MessageBoxCustom("Cảnh báo", "Vui lòng chọn loại độc giả bạn muốn thay đổi hoặc thêm loại độc giả mới", MessageType.Warning, MessageButtons.OK);
                        mb.ShowDialog();
                        return;
                    }
                    else
                    {
                        ReaderTypeDTO reader = new ReaderTypeDTO();
                        reader.name = tempReaderType;
                        (bool Successful, string message) = ReaderTypeService.Ins.CreateReaderType(reader);
                        if (Successful)
                        {
                            ListReaderType.Add(reader);
                            MessageBoxCustom mb = new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);
                            mb.ShowDialog();
                            ResetDataAddReaderTypeWindow();
                            return;
                        }
                        else
                        {
                            MessageBoxCustom mb = new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            ResetDataAddReaderTypeWindow();
                            return;
                        }
                    }
                }
            });
            CheckedSexCM = new RelayCommand<RadioButton>((p) => { return true; }, (p) =>
             {
                 Sex = p.Content.ToString();
             });
            OpenAddReaderCardWindowCM = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                AddReaderCardWindow addReaderCardWindow = new AddReaderCardWindow();
                ResetData();
                addReaderCardWindow.ShowDialog();
            });
            OpenEditReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditReaderCardWindow editReaderCardWindow = new EditReaderCardWindow();
                LoadEditReaderCard(editReaderCardWindow);
                editReaderCardWindow.ShowDialog();
                ResetData();
            });
            OpenPrintReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PrintReaderCardWindow printReaderCardWindow = new PrintReaderCardWindow();
                LoadPrintReaderCard(printReaderCardWindow);
                printReaderCardWindow.ShowDialog();
            });
            UpdateReaderCardCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                (bool isvalid, string error) = IsValidData();
                if (isvalid)
                {
                    ReaderCardDTO reader = new ReaderCardDTO()
                    {
                        id = SelectedItem.id,
                        name = Name,
                        address = Adress,
                        employeeId = "NV0001",
                        readerTypeId = (ListReaderType.FirstOrDefault(s => s.name == ReaderType)).id,
                        totalFine = SelectedItem.totalFine,
                        email = Email,
                        createdAt = (DateTime)StartDate,
                        expiryDate = (DateTime)FinishDate,
                        gender = Sex,
                        birthDate = (DateTime)Birthday,
                    };

                    (bool Successful, string message) = ReaderService.Ins.UpdateReaderCard(reader);

                    if (Successful)
                    {
                        var readerCardFound = ListReaderCard.FirstOrDefault(s => s.id == reader.id);
                        ListReaderCard[ListReaderCard.IndexOf(readerCardFound)] = reader;
                        MessageBoxCustom mb = new MessageBoxCustom("Thông báo", message, MessageType.Success, MessageButtons.OK);
                        mb.ShowDialog();
                        p.Close();
                    }
                    else
                    {
                        MessageBoxCustom mb = new MessageBoxCustom("Lỗi", message, MessageType.Error, MessageButtons.OK);
                        mb.ShowDialog();
                    }
                }
                else
                {
                    MessageBoxCustom mb = new MessageBoxCustom("Lỗi", error, MessageType.Error, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });
            DeleteEditReaderCardCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedItem is null) return;

                try
                {
                    (bool isS, string mes) = ReaderService.Ins.DeleteReaderCard(SelectedItem.id);

                    if (isS)
                    {
                        ListReaderCard = new ObservableCollection<ReaderCardDTO>(ReaderService.Ins.GetAllReaderCards());
                    }
                    MessageBox.Show(mes);
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
        }
        private (bool valid, string error) IsValidData()
        {

            if (string.IsNullOrEmpty(Name) || Sex is null || Birthday is null || string.IsNullOrEmpty(Adress) || string.IsNullOrEmpty(ReaderType))
            {
                return (false, "Thông tin độc giả thiếu! Vui lòng bổ sung");
            }

            if (!Helper.IsValidEmail(Email))
            {
                return (false, "Email không hợp lệ");
            }

            return (true, null);
        }
    }
}
