using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.ViewModel;
using LibraryManagement.Views;
using LibraryManagement.Views.Login;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
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

        private string account;
        public string Account
        {
            get { return account; }
            set { account = value; OnPropertyChanged(); }
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

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        #endregion

        #region COMMAND
        public ICommand SaveLoginWindowCM { get; set; }
        public ICommand CloseWindowCM { get; set; }
        public ICommand MinimizeWindowCM { get; set; }
        public ICommand MouseLeftButtonDownWindowCM { get; set; }
        public ICommand LoadLoginPageCM { get; set; }
        public ICommand LoadConfirmPageCM { get; set; }
        public ICommand OpenForgetPassPageCM { get; set; }
        public ICommand OpenLoginPageCM { get; set; }
        public ICommand PasswordChangedCM { get; set; }
        public ICommand CodeChangedCM { get; set; }
        public ICommand NewPassChangedCM { get; set; }
        public ICommand LoginCM { get; set; }
        public ICommand SendCodeCM { get; set; }
        public ICommand SaveNewPassCM { get; set; }
        public ICommand ConfirmCodeCM { get; set; }

        #endregion

        public Frame MainFrame { get; set; }
        public Window LoginWindow { get; set; }
        public StackPanel MainPanel { get; set; }
        public int SecurityCode;

        public LoginViewModel()
        {
            SaveLoginWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p is null) return;

                LoginWindow = p;
                UserName = null;
                Password = null;
                Email = null;
                NewPass = null;
                Account = null;
            });

            CloseWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            MinimizeWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (p.WindowState == WindowState.Normal)
                    p.WindowState = WindowState.Minimized;
            });

            MouseLeftButtonDownWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.DragMove();
            });

            LoadLoginPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                MainFrame.Content = new LoginPage();
            });

            LoadConfirmPageCM = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                string email = Email;
                string[] parts = email.Split('@');
                string maskedEmail = string.Empty;
                string host = string.Empty;

                for (int i = 0; i <= 2; i++)
                    maskedEmail += parts[0][i];
                for (int i = 3; i <= parts[0].Length - 1; i++)
                    maskedEmail += "*";

                for (int i = 0; i <= parts[1].Length - 1; i++)
                {
                    if (parts[1][i] != '.')
                        host += "*";
                    else
                    {
                        i++;
                        host += "." + parts[1][i].ToString();
                    }
                }

                maskedEmail += "@" + host;

                p.Text = "Mã bảo mật 6 chữ số đã được gửi đến email: " + maskedEmail;
            });


            OpenForgetPassPageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Account = "";
                MainFrame.Content = new ForgetPassPage();
            });

            OpenLoginPageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UserName = "";
                MainFrame.Content = new LoginPage();
            });

            PasswordChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });

            CodeChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Code = p.Password;
            });

            NewPassChangedCM = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPass = p.Password;
            });

            LoginCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                else
                    try
                    {
                        (AccountDTO user, string mes) = AuthService.Ins.Login(UserName, Password);

                        if (user != null)
                        {
                            LoginWindow.Hide();
                            MainWindowViewModel.CurrentUser = user;
                            MainWindow wd = new MainWindow();
                            if (!user.role.roleDetaislList[10].isPermitted && !user.role.roleDetaislList[11].isPermitted && !user.role.roleDetaislList[12].isPermitted)
                            {
                                wd.BillTreeView.Visibility = Visibility.Collapsed;
                            }
                            if (!user.role.roleDetaislList[1].isPermitted && !user.role.roleDetaislList[2].isPermitted && !user.role.roleDetaislList[3].isPermitted)
                            {
                                wd.bookTreeview.Visibility = Visibility.Collapsed;
                            }
                            if (!user.role.roleDetaislList[15].isPermitted && !user.role.roleDetaislList[16].isPermitted)
                            {
                                wd.settingBtn.Visibility = Visibility.Collapsed;
                            }
                            if (user.reader != null)
                            {
                                wd.bookTreeview.Visibility = Visibility.Collapsed;
                                wd.BookManageBtnreader.Visibility = Visibility.Visible;
                            }
                            wd.Show();
                            LoginWindow.Close();
                        }
                        else
                            MessageBox.Show(mes);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
            });

            SendCodeCM = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(Account))
                {
                    p.Text = "Vui lòng nhập đầy đủ thông tin!";
                    return;
                }

                Email = AuthService.Ins.GetAccountByUsername(Account);

                if (string.IsNullOrEmpty(Email))
                {
                    p.Text = "Tài khoản không hợp lệ!";
                    return;
                }

                var appSettings = ConfigurationManager.AppSettings;
                string sender = appSettings["APP_EMAIL"];
                string passwword = appSettings["APP_PASSWORD"];
                string recipient = Email;
                SendEmail(sender, passwword, recipient);
                MainFrame.Content = new ConfirmCodePage();

            });

            ConfirmCodeCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(Code))
                    MessageBox.Show("Vui lòng nhập mã bảo mật!");
                else if (Code != SecurityCode.ToString())
                    MessageBox.Show("Mã bảo mật không hợp lệ!");
                else
                    MainFrame.Content = new ChangePassPage();
            });

            SaveNewPassCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (string.IsNullOrEmpty(NewPass))
                    MessageBox.Show("Vui lòng nhập mật khẩu mới");
                try
                {
                    (bool isS, string mess) = AuthService.Ins.ResetPassword(Account, NewPass);

                    if (isS)
                    {
                        MessageBox.Show(mess);
                        MainFrame.Content = new LoginPage();
                    }
                    else
                        MessageBox.Show(mess);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            });
        }

        public int RandomCode()
        {
            Random rd = new Random();

            int c = rd.Next(000000, 999999);
            return c;
        }

        protected Task SendEmail(string sndr, string pass, string rcpt)
        {
            SecurityCode = RandomCode();

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(sndr, pass);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sndr, "Lima-App");
            mail.To.Add(rcpt);
            mail.Subject = "[LIMA APP] Lấy lại mật khẩu";
            mail.Body = "Xin chào, chúng tôi gửi cho bạn mã bảo mật để có thể giúp bạn lấy lại mật khẩu tài khoản LIMA.\nMã bảo mật: " + SecurityCode;

            return smtp.SendMailAsync(mail);
        }
    }
}
