using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Views.Genre_AuthorManagement;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.Genre_AuthorManagementVM
{
    public partial class Genre_AuthorManagementViewModel : BaseViewModel
    {

        private ObservableCollection<GenreDTO> genreList;
        public ObservableCollection<GenreDTO> GenreList
        {
            get { return genreList; }
            set { genreList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<AuthorDTO> authorList;
        public ObservableCollection<AuthorDTO> AuthorList
        {
            get { return authorList; }
            set { authorList = value; OnPropertyChanged(); }
        }

        private GenreDTO selectedGenre;
        public GenreDTO SelectedGenre
        {
            get { return selectedGenre; }
            set { selectedGenre = value; OnPropertyChanged(); }
        }

        private AuthorDTO selectedAuthor;
        public AuthorDTO SelectedAuthor
        {
            get { return selectedAuthor; }
            set { selectedAuthor = value; OnPropertyChanged(); }
        }




        private string txtGenre;
        public string TxtGenre
        {
            get { return txtGenre; }
            set { txtGenre = value; OnPropertyChanged(); }
        }

        private string txtAuthor;
        public string TxtAuthor
        {
            get { return txtAuthor; }
            set { txtAuthor = value; OnPropertyChanged(); }
        }

        private DateTime? birthDate;
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; OnPropertyChanged(); }
        }

        public static AccountDTO CurrentUser { get; set; }

        public ICommand OpenAddGenreWindowCM { get; set; }
        public ICommand AddGenreCM { get; set; }
        public ICommand DeleteGenreCM { get; set; }
        public ICommand OpenEditGenreWindowCM { get; set; }
        public ICommand EditGenreCM { get; set; }
        public ICommand OpenAddAuthorWindowCM { get; set; }
        public ICommand AddAuthorCM { get; set; }
        public ICommand DeleteAuthorCM { get; set; }
        public ICommand OpenEditAuthorWindowCM { get; set; }
        public ICommand EditAuthorCM { get; set; }


        public Genre_AuthorManagementViewModel()
        {

            Firstload();
            OpenAddGenreWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddGenreWindow w = new AddGenreWindow();
                TxtGenre = null;
                w.ShowDialog();
            });
            OpenEditGenreWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditGenreWindow w = new EditGenreWindow();
                TxtGenre = SelectedGenre.name;
                w.ShowDialog();
            });
            AddGenreCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(TxtGenre))
                    {
                        MessageBox.Show("Vui lòng điền đủ thông tin");
                        return;
                    }


                    if (MessageBox.Show("Bạn có muốn thêm thể loại này không?", "Thêm thể loại", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        GenreDTO newGenre = new GenreDTO { name = TxtGenre };
                        (bool isS, string mes) = GenreService.Ins.CreateNewGenre(newGenre);
                        if (isS)
                        {
                            GenreList.Add(newGenre);
                            p.Close();
                        }

                        MessageBox.Show(mes);
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
            EditGenreCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(TxtGenre))
                    {
                        MessageBox.Show("Vui lòng điền đủ thông tin");
                        return;
                    }

                    if (MessageBox.Show("Bạn có muốn sửa thể loại này không?", "Sửa thể loại", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        GenreDTO newGenre = SelectedGenre;
                        newGenre.name = TxtGenre;
                        (bool isS, string mes) = GenreService.Ins.EditGenre(newGenre);
                        if (isS)
                        {
                            GenreList = new ObservableCollection<GenreDTO>(GenreService.Ins.GetAllGenre());
                            p.Close();
                        }

                        MessageBox.Show(mes);
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
            DeleteGenreCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedGenre != null)
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn xoá thể loại này không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            (bool IsS, string mes) = GenreService.Ins.DeleteGenre(SelectedGenre.id);

                            if (IsS)
                                GenreList.Remove(SelectedGenre);
                            MessageBox.Show(mes);
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
            });
            OpenAddAuthorWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddAuthorWindow w = new AddAuthorWindow();
                TxtAuthor = null;
                w.ShowDialog();
            });
            AddAuthorCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(TxtAuthor) || BirthDate is null)
                    {
                        MessageBox.Show("Vui lòng điền đủ thông tin");
                        return;
                    }

                    if (MessageBox.Show("Bạn có muốn thêm tác giả này không?", "Thêm tác giả", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        AuthorDTO newAu = new AuthorDTO { name = TxtAuthor, birthDate = (DateTime)BirthDate };
                        (bool isS, string mes) = AuthorService.Ins.CreateNewAuthor(newAu);
                        if (isS)
                        {
                            AuthorList.Add(newAu);
                            p.Close();
                        }
                        MessageBox.Show(mes);
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
            DeleteAuthorCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (SelectedAuthor != null)
                    try
                    {
                        if (MessageBox.Show("Bạn có muốn xoá tác giả này không?", "Cảnh báo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            (bool IsS, string mes) = AuthorService.Ins.DeleteAuthor(SelectedAuthor.id);

                            if (IsS)
                                AuthorList.Remove(SelectedAuthor);
                            MessageBox.Show(mes);
                        }
                        else
                        {
                            return;
                        }

                    }
                    catch (Exception e)
                    {

                        MessageBox.Show(e.Message);
                    }
            });
            OpenEditAuthorWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                EditAuthorWindow w = new EditAuthorWindow();
                TxtAuthor = SelectedAuthor.name;
                BirthDate = SelectedAuthor.birthDate;
                w.ShowDialog();
            });
            EditAuthorCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(TxtAuthor) || BirthDate is null)
                    {
                        MessageBox.Show("Vui lòng điền đủ thông tin");
                        return;
                    }

                    if (MessageBox.Show("Bạn có muốn sửa tác giả này không?", "Sửa tác giả", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        AuthorDTO newAu = SelectedAuthor;
                        newAu.name = TxtAuthor;
                        newAu.birthDate = (DateTime)BirthDate;
                        (bool isS, string mes) = AuthorService.Ins.EditAuthor(newAu);
                        if (isS)
                        {
                            AuthorList = new ObservableCollection<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
                            p.Close();
                        }

                        MessageBox.Show(mes);
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            });
        }





        public void Firstload()
        {
            GenreList = new ObservableCollection<GenreDTO>(GenreService.Ins.GetAllGenre());
            AuthorList = new ObservableCollection<AuthorDTO>(AuthorService.Ins.GetAllAuthor());
        }
    }
}
