using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.ReaderCard;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        public ICommand OpenEditReaderCardCM { get; set; }
        public ICommand UpdateReaderCardCM { get; set; }
        public ICommand OpenRenewalHistoryWindowCM { get; set; }
        public ICommand UpdateRenewalCM { get; set; }

        public void LoadEditReaderCard(EditReaderCardWindow w)
        {
            Name = SelectedItem.name;
            if (SelectedItem.gender == "Nam")
            {
                w.Man.IsChecked = true;
            }
            else
            {
                w.Woman.IsChecked = true;
            }
            Sex = SelectedItem.gender;
            Birthday = SelectedItem.birthDate;
            Email = SelectedItem.email;
            Adress = SelectedItem.address;
            ReaderType = SelectedItem.readerType.name;
            CreateAt = SelectedItem.createdAt;
            FinishDate = SelectedItem.renewalHistories[SelectedItem.renewalHistories.Count - 1].endDate;
            StartDate = SelectedItem.renewalHistories[SelectedItem.renewalHistories.Count - 1].renewalDate;
            CardHistoryList = new ObservableCollection<RenewalHistoryDTO>(SelectedItem.renewalHistories);
            for (int i = 1; i <= CardHistoryList.Count; i++)
            {
                CardHistoryList[i - 1].Index = i;
            }
        }

        public void OpenRenewalFunc()
        {
            RenewalWindow w = new RenewalWindow();
            var rule = ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD);
            w.ruleCardExpired.Text = $"Quy định: Gia hạn thêm {rule} tháng";
            w.renewDay.Text = DateTime.Now.ToString("dd/MM/yyyy");

            calculateReaderCardExpiredDate(w);

            w.ShowDialog();
        }

        private void calculateReaderCardExpiredDate(RenewalWindow w)
        {
            if (SelectedItem.expiryDate > DateTime.Now)
            {
                w.NewDay.Text = SelectedItem.expiryDate.AddMonths(ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD)).ToString("dd/MM/yyyy");
            }
            else
                w.NewDay.Text = DateTime.Now.AddMonths(ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD)).ToString("dd/MM/yyyy");
        }
    }
}
