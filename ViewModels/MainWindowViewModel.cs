﻿using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.ViewModels;
using LibraryManagement.ViewModels.SettingVM;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.Genre_AuthorManagement;
using LibraryManagement.Views.ImportBookPage;
using LibraryManagement.Views.Login;
using LibraryManagement.Views.SettingManagement;
using LibraryManagement.Views.StatisticalManagement;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand OpenBookManagementPageCM { get; set; }
        public ICommand OpenImportBookPage { get; set; }
        public ICommand OpenGenreAuthorManagementPage { get; set; }
        public ICommand OpenGenreStatisticPageCM { get; set; }
        public ICommand OpenLateStatisticPageCM { get; set; }
        public ICommand OpenSettingPageCM { get; set; }
        public ICommand SignOutCM { get; set; }


        public static AccountDTO CurrentUser { get; set; }

        public MainWindowViewModel()
        {
            OpenBookManagementPageCM = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookManagementPage();
            });
            OpenImportBookPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
             {
                 p.Content = new MainImportBookPage();
             });
            OpenGenreAuthorManagementPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainManagementPage();
            });
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
                SettingViewModel.CurrentUser = CurrentUser;
                p.Content = new MainSettingPage();
            });
            SignOutCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                LoginWindow w = new LoginWindow();
                w.Show();
                p.Close();
         
            });

            // EMPLOYEEEE
            //var newEmployee = new EmployeeDTO()
            //{
            //    name = "Lee Phong new",
            //    phoneNumber = "09875123444",
            //    birthDate = new DateTime(2002, 5, 26),
            //    gender = "Nam", //Nam or Nữ
            //    startingDate = DateTime.Now,
            //    email = "20521111@gm.uit.edu.vn",
            //    account = new AccountDTO
            //    {
            //        username = "lephong",
            //        password = "123456",
            //        roleId = 1,
            //    }
            //};

            //(bool success, string message) = EmployeeService.Ins.CreateNewEmployee(newEmployee);
            //var employees = EmployeeService.Ins.GetAllEmployees();
            //newEmployee.id = "NV0002";
            //newEmployee.accountId = 3;
            //(bool uupdateSuccess, string updateMessage) = EmployeeService.Ins.UpdateEmployee(newEmployee);
            //(bool deleteSuccess, string deleteMessage) = EmployeeService.Ins.DeleteEmployee("NV0003");

            //List<ReaderTypeDTO> readerTypeList = new List<ReaderTypeDTO>(){
            //        new ReaderTypeDTO
            //        {
            //            name = "Độc giả A"
            //        },
            //        new ReaderTypeDTO
            //        {
            //            name = "Độc giả B"
            //        },
            //        new ReaderTypeDTO
            //        {
            //            name = "Độc giả C"
            //        },
            //    };

            //readerTypeList.ForEach(r =>
            //{
            //    ReaderTypeService.Ins.CreateReaderType(r);
            //});

            //var data = ReaderTypeService.Ins.GetAllReaderTypes();


            //Reader Card
            int duration = ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD);
            var createdAt = DateTime.Now;
            var newReaderCard = new ReaderCardDTO()
            {
                name = "Khôi Trần",
                address = "Quận 1, TPHCM",
                readerTypeId = 3,
                email = "khoitran@gmail.com",
                createdAt = createdAt,
                expiryDate = createdAt.AddMonths(30),
                gender = "Nữ",
                birthDate = new DateTime(2002, 5, 26),
                employeeId = "NV0002",
            };
            (bool isSuccess, string messageCreate) = ReaderService.Ins.CreateNewReaderCard(newReaderCard);
            //(bool deleteSuccess, string deleteMessage) = ReaderService.Ins.DeleteReaderCard("READER0001");
            ////Authentication
            //(AccountDTO user, string mesasage) = AuthService.Ins.Login("datdo", "123456");
            //int duration = ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD);
            //var createdAt = DateTime.Now;
            //var newReaderCard = new ReaderCardDTO()
            //{
            //    name = "Tesst",
            //    address = "Kí túc xá khu A",
            //    readerTypeId = 3,
            //    email = "tesst123@gmail.com",
            //    createdAt = createdAt,
            //    expiryDate = createdAt.AddMonths(30),
            //    gender = "Nữ",
            //    birthDate = new DateTime(2002, 5, 26),
            //    employeeId = "NV0002",
            //};
            //(bool isSuccess, string message) = ReaderService.Ins.CreateNewReaderCard(newReaderCard);
            //(AccountDTO user, string mesasage) = AuthService.Ins.Login("READER0002", "READER0002");

            //var email = AuthService.Ins.GetAccountByUsername("datdo");
            //(bool resetSuccess, string resetMessage) = AuthService.Ins.ResetPassword("datdo", "1234567");
            //(var userLogin, string message) = AuthService.Ins.Login("datdo", "1234567");


            int minAge = ParameterService.Ins.GetRuleValue(Rules.MIN_AGE);
            //int maxAge = ParameterService.Ins.GetRuleValue(Rules.MAX_AGE);
            //int ALLOWED_BOOK_MAXIMUM = ParameterService.Ins.GetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM);
            //int YEAR_PUBLICATION_PERIOD = ParameterService.Ins.GetRuleValue(Rules.YEAR_PUBLICATION_PERIOD);
            //int MAXIMUM_NUMBER_OF_DAYS_TO_BORROW = ParameterService.Ins.GetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW);
            //(bool isUpdateSuccess, string updateMessage)  = BookService.Ins.UpdateBook(new BookDTO
            //{
            //    id="BOOK0004",
            //    name = "Cô gái đến từ hôm qua test",
            //    genreId = 1,
            //    authorId = 2,
            //    yearOfPublication = 1990,
            //    publisher = "Nhà xuất bản Trẻ",
            //});
            //var allImportReceipt = ImportService.Ins.GetAllImportReceipt();
            //var importReceiptDetails = ImportService.Ins.GetImportReceiptDetail("IPR0002");
            //var bookList = BookService.Ins.GetAllBook();
            //var availableBookList = BookService.Ins.GetAllAvailableBook();

            //var role = new RoleDTO
            //{
            //    name = "Độc giả",
            //    roleDetaislList = new List<RoleDetailsDTO>
            //    {
            //        new RoleDetailsDTO
            //        {
            //        permissionId = 1,
            //        isPermitted = true
            //        },
            //           new RoleDetailsDTO
            //        {
            //        permissionId = 4,
            //        isPermitted = true
            //        },
            //    },
            //};

            //(bool success, string message) = RoleService.Ins.CreateNewRole(role);
            //(bool success, string roleMessage) = RoleService.Ins.DeleteRole(5);

            var roleList = RoleService.Ins.GetAllRoles();
            //var permissionList = RoleService.Ins.GetAllPermissions();



            //Employeê
            //var newEmployee = new EmployeeDTO
            //{
            //    name = "Đạt ĐT",
            //    email = "20521163@gm.uit.edu.vn",
            //    phoneNumber = "0987582042",
            //    roleId = 2,
            //    birthDate = new DateTime(2002, 5, 12),
            //    gender = "Nam",
            //    password = "123456",
            //    username = "datrumqn",
            //    startingDate = DateTime.Now,
            //};
            //(bool isSuccess, string message) = EmployeeService.Ins.CreateNewEmployee(newEmployee);
            //var employees = EmployeeService.Ins.GetAllEmployees();



            // Borrowing/Return Service
            int borrowingDuration = ParameterService.Ins.GetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW);
            int max = ParameterService.Ins.GetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM);
            int fineDaily = ParameterService.Ins.GetRuleValue(Rules.FINE);
            //Check số sách tối đa có thể mượn
            //BorrowingCardDTO cardDTO = new BorrowingCardDTO()
            //{
            //    readerCardId = "READER0001",
            //    borrowingDate = DateTime.Now,
            //    dueDate = DateTime.Now.AddDays(borrowingDuration),
            //    employeeId = "NV0001",
            //};
            //var bookInfoIdList = new List<string>()
            //{
            //    "BOOK0001_001",
            //    "BOOK0002_002",
            //    "BOOK0003_002",
            //};
            //(bool success, string message) = BorrowingReturnService.Ins.CreateBorrowingCard(cardDTO, bookInfoIdList);

            //var borrowingCardDTOs = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId("READER0001");

            ////////Return card
            //var returnCardList = new List<ReturnCardDTO>();

            //borrowingCardDTOs.ForEach(b =>
            //{

            //    int days = DateTime.Now.Subtract(b.dueDate).Days;
            //    int fine = b.dueDate >= DateTime.Now ? 0 : fineDaily * days;
            //    var card = new ReturnCardDTO()
            //    {
            //        id = b.id,
            //        borrowingCardId = b.id,
            //        returnedDate = DateTime.Now,
            //        employeeId = "NV0001",
            //        fine = fine,
            //    };
            //    returnCardList.Add(card);
            //});
            ////var borrowingCardDTOs = BorrowingReturnService.Ins.GetBorrowingCardsByReaderId("READER0001");
            //(bool success, string message) = BorrowingReturnService.Ins.CreateReturnCardList(returnCardList);



            ////FINE RECEIPT
            ///

            //var fineReceipt = new FineReceiptDTO()
            //{
            //    amount = 3000,
            //    createdAt = DateTime.Now,
            //    employeeId = "NV0001",
            //    readerCardId = "READER0001",
            //};

            ////(bool isSucc, string message) = FineReceiptService.Ins.CreateFineReceipt(fineReceipt);


            //var books = BookService.Ins.GetAllAvailableBook();
            //var delayBorrowingCards = BorrowingReturnService.Ins.GetDelayBorrowingCardsByReaderId("READER0001");
            //var a = "";
        }

        const string APP_EMAIL = "";
        const string APP_PASSWORD = "";
        protected void SendEmail(string email, int randomCode)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(APP_EMAIL, APP_PASSWORD);

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(APP_EMAIL, "Squadin Cinema");
            mail.To.Add(email);
            mail.Subject = "Lấy lại mật khẩu đăng nhập";

            smtp.Send(mail);
        }
    }
}
