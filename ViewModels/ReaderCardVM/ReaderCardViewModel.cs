using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public class ReaderCard
    {
        public string id { get; set; }
        public string name { get; set; }
        public string birthDay { get; set; }
        public string idCard { get; set; }
        public string type { get; set; }
        public string registrationDate { get; set; }
        public string expirationDate { get; set; }
        public ReaderCard()
        {
            id = "ABCD";
            name = "Lê Hải Phong";
            birthDay = "20/04/2002";
            idCard = "212454483";
            type = "Loại";
            registrationDate = "14/06/2004";
            expirationDate = "03/12/2021";
        }
    }

    public partial class ReaderCardViewModel : BaseViewModel
    {
        private ObservableCollection<ReaderCard> _listReaderCard;
        public ObservableCollection<ReaderCard> ListReaderCard
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    _listReaderCard.Add(new ReaderCard());
                }
                return _listReaderCard;
            }
            set { _listReaderCard = value;OnPropertyChanged(); }
        }

        #region
        public ICommand SelectedDateCM { get; set; }
        public ICommand AddReaderCard { get; set; }
        #endregion
        public ReaderCardViewModel()
        {
            ListReaderCard = new ObservableCollection<ReaderCard>();
            StartDate = DateTime.Now;
            FinishDate = StartDate.AddDays(30);
            SelectedDateCM = new RelayCommand<object>((p) => { return true; },(p) =>
            {
                FinishDate = StartDate.AddDays(30);
            });

            AddReaderCard = new RelayCommand<System.Windows.Window>((p) => { return true; }, async (p) =>
            {
                await AddReader(p);
            });

;        }
    }
}
