using LibraryManagement.ViewModels;
using LibraryManagement.Views.ReaderCard;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        public ICommand OpenEditReaderCardCM { get; set; }
        public ICommand UpdateReaderCardCM { get; set; }

        public void LoadEditReaderCard(EditReaderCardWindow w)
        {
            Name = SelectedItem.name;
            if(SelectedItem.gender == "Nam")
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
            //StartDate = SelectedItem.;//fixx
            //CardHistoryList = SelectedItem 
            FinishDate = SelectedItem.expiryDate;
        }
    }
}
