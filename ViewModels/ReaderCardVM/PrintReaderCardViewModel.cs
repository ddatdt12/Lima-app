﻿using LibraryManagement.ViewModels;
using LibraryManagement.Views.ReaderCard;
using System.Windows.Input;

namespace LibraryManagement.ViewModel.ReaderCardVM
{
    public partial class ReaderCardViewModel : BaseViewModel
    {
        public ICommand OpenPrintReaderCardCM { get; set; }
        public void LoadPrintReaderCard(PrintReaderCardWindow w)
        {
            if (SelectedItem is null) 
                return;
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
            StartDate = SelectedItem.createdAt;
            FinishDate = SelectedItem.expiryDate;
            w.ShowDialog();
        }
    }
}