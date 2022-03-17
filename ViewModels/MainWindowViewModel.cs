using LibraryManagement.ViewModels;
using LibraryManagement.Views.SettingManagement;
using LibraryManagement.Views.StatisticalManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        public ICommand OpenGenreStatisticPageCM { get; set; }
        public ICommand OpenLateStatisticPageCM { get; set; }
        public ICommand OpenSettingPageCM { get; set; }



        public MainWindowViewModel()
        {
            OpenGenreStatisticPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new GenreStatisticPage();
            });
            OpenLateStatisticPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new LateStatisticPage();
            });
            OpenSettingPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingPage();
            });
        }


    }
}
