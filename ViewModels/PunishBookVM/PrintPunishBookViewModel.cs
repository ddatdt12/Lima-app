using LibraryManagement.DTOs;
using LibraryManagement.Views.PunishBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace LibraryManagement.ViewModels.PunishBookVM
{
    public partial class PunishBookViewModel : BaseViewModel
    {
        public ICommand PrintCM { get; set; }

       

        public void OpenDemoPrintWindow(FineReceiptDTO fineReceipt)
        {
            PrintWindow w = new PrintWindow();
            printWindow = w;
            w.punishCard.Text = fineReceipt.id;
            w.date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            w.name.Text = ReaderName;
            w.totalDept.Text = Utils.Helper.FormatVNMoney((decimal)TotalDept);
            w.paid.Text = Utils.Helper.FormatVNMoney((decimal)TotalPaid);
            w.remain.Text = Utils.Helper.FormatVNMoney((decimal)TotalLeft);
            w.ShowDialog();
            
        }


        public void OpenPrintWindow(FineReceiptDTO fineReceipt, PrintWindow printWindow)
        {
            //create printer
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() != true) { printWindow.Close(); return; }

            //create document
            FixedDocument document = new FixedDocument();
            document.DocumentPaginator.PageSize = new Size(600, 420);

            //create page
            FixedPage page = new FixedPage();
            page.Width = document.DocumentPaginator.PageSize.Width;
            page.Height = document.DocumentPaginator.PageSize.Height;

            PrintWindow w = new PrintWindow();
            w.punishCard.Text = fineReceipt.id;
            w.date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            w.name.Text = ReaderName;
            w.totalDept.Text = Utils.Helper.FormatVNMoney((decimal)TotalDept);
            w.paid.Text = Utils.Helper.FormatVNMoney((decimal)TotalPaid);
            w.remain.Text = Utils.Helper.FormatVNMoney((decimal)TotalLeft);

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
            pd.PrintDocument(document.DocumentPaginator, "Return bill");
            printWindow.Close();
        }
    }
}
