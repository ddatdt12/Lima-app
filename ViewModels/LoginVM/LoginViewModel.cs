using LibraryManagement.Views.Login;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryManagement.ViewModels.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        #region BINDING
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string newPass;
        public string NewPass
        {
            get { return newPass; }
            set { newPass = value; OnPropertyChanged(); }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }
        #endregion

        public Frame MainFrame { get; set; }
        public StackPanel MainPanel { get; set; }
        public bool isLoading = false;

        #region COMMAND
        public ICommand CloseWindowCM { get; set; }
        public ICommand MinimizeWindowCM { get; set; }
        public ICommand MouseLeftButtonDownWindowCM { get; set; }
        public ICommand LoadLoginPageCM { get; set; }
        public ICommand OpenForgetPassPageCM { get; set; }
        public ICommand OpenLoginPageCM { get; set; }
        public ICommand PasswordChangedCM { get; set; }
        public ICommand LoginCM { get; set; }
        public ICommand SendCodeCM { get; set; }
        public ICommand NewPassCM { get; set; }
        public ICommand SaveNewPassCM { get; set; }
        public ICommand CodeCM { get; set; }
        public ICommand ConfirmCodeCM { get; set; }

        #endregion

        public LoginViewModel()
        {
            CloseWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Window).Close();
            });

            MinimizeWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if ((p as Window).WindowState == WindowState.Normal)
                    (p as Window).WindowState = WindowState.Minimized;
            });

            MouseLeftButtonDownWindowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                (p as Window).DragMove();
            });

            LoadLoginPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p as Frame;
                MainFrame.Content = new LoginPage();
            });

            OpenForgetPassPageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgetPassPage();
            });

            OpenLoginPageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new LoginPage();
            });

            PasswordChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });

            LoginCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CheckAccount(UserName, Password);
            });

            SendCodeCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                //Get email by username
               
            });

            NewPassCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPass = p.Password;
            });

            SaveNewPassCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
            });

            CodeCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPass = p.Password;
            });

            ConfirmCodeCM = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                //Get email by username

            });
        }

        void CheckAccount(string username, string password)
        {
            //check?
        }
    }
}
