using LibraryManagement.DTOs;
using LibraryManagement.Views.PunishBook;
using System;
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
            w.Height = 500;
            w.punishCard.Text = fineReceipt.id;
            w.date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            w.name.Text = ReaderName;
            w.paid.Text = Utils.Helper.FormatVNMoney((decimal)TotalPaid);
            w.ShowDialog();
            
        }
    }
}
