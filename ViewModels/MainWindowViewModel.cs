using LibraryManagement.View.ReaderCard;
using LibraryManagement.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region
        public ICommand OpenReaderCardPageCM { get; set; }
        #endregion
        public MainWindowViewModel()
        {
            OpenReaderCardPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ReaderCardPage();
            });
        }

    }
}
