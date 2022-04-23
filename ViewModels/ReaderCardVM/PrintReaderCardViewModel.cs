using LibraryManagement.DTOs;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.ReaderCard;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        public ICommand OpenPrintReaderCardCM { get; set; }
        public void OpenPrintWindow(ReaderCardDTO readercard)
        {
            PrintReaderCardWindow w = new PrintReaderCardWindow();
            w.id.Text = readercard.id;
            w.name.Text = readercard.name;
            w.gender.Text = readercard.gender;
            w.birthdate.Text = readercard.birthDate.ToString("dd/MM/yyyy");
            w.address.Text = readercard.address;
            w.email.Text = readercard.email;
            w.createdate.Text = readercard.createdAt.ToString("dd/MM/yyyy");
            w.expirydate.Text = readercard.expiryDate.ToString("dd/MM/yyyy");
            w.ShowDialog();
        }
    }
}
