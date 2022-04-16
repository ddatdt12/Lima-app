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
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) return;

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 500);

            //create page
            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Height;

            PrintReaderCardWindow w = new PrintReaderCardWindow();
            w.id.Text = readercard.id;
            w.name.Text = readercard.name;
            w.gender.Text = readercard.gender;
            w.birthdate.Text = readercard.birthDate.ToString("dd/MM/yyyy");
            w.address.Text = readercard.address;
            w.email.Text = readercard.email;
            w.createdate.Text = readercard.createdAt.ToString("dd/MM/yyyy");
            w.expirydate.Text = readercard.expiryDate.ToString("dd/MM/yyyy");

            //remove element from tree
            Grid parent = w.Print.Parent as Grid;
            Grid child = w.Print as Grid;
            parent.Children.Remove(w.Print);
            page.Children.Add(child);

            // add the page to the document
            PageContent page1Content = new PageContent();
            ((IAddChild)page1Content).AddChild(page);
            document.Pages.Add(page1Content);


            // and print
            pd.PrintDocument(document.DocumentPaginator, "Reader card");
        }
    }
}
