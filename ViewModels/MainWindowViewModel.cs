using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.ViewModels;
using LibraryManagement.Views.BookManagement;
using LibraryManagement.Views.ImportBookPage;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand OpenBookManagementPageCM { get; set; }
        public ICommand OpenImportBookPage { get; set; }

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
            //var author = new AuthorDTO { name = "Hemingway", birthDate = DateTime.Now, };
            //List<AuthorDTO> authorList = new List<AuthorDTO>()
            //{
            //    new AuthorDTO { name= "J.R.R.Tolkien", birthDate = DateTime.Now,  },
            //    new AuthorDTO { name= "Franz Kafka" , birthDate = DateTime.Now, },
            //    new AuthorDTO { name= "Harper Lee" , birthDate = DateTime.Now, },
            //    new AuthorDTO { name= "Vũ Phương Thanh" , birthDate = DateTime.Now, },
            //    new AuthorDTO { name= "William Shakespeare", birthDate = DateTime.Now,  },
            //    new AuthorDTO { name= "Nguyễn Nhật Ánh" , birthDate = DateTime.Now, },
            //    new AuthorDTO { name= "Stephenie Meyer" ,birthDate = DateTime.Now, },
            //    new AuthorDTO { name= "Robert M. Pirsig" , birthDate = DateTime.Now, },
            //    new AuthorDTO { name="Đạt ĐT", birthDate = DateTime.Now, },
            //};
            //(bool isSuccessAuthor, string messageAuthor) = AuthorService.Ins.CreateNewAuthor(author);
            //authorList.ForEach(a =>
            //{
            //    AuthorService.Ins.CreateNewAuthor(a);
            //});

            //List<GenreDTO> genres = new List<GenreDTO>()
            //{
            //    new GenreDTO { name = "Khoa học công nghệ – Kinh tế" },
            //    new GenreDTO { name = "Văn học nghệ thuật" },
            //    new GenreDTO { name = "Văn hóa xã hội – Lịch sử" },
            //    new GenreDTO { name = "Tình cảm" },
            //    new GenreDTO { name = "Giáo trình" },
            //    new GenreDTO { name = "Giả tưởng và khoa học viễn tưởng" },
            //    new GenreDTO { name = "Truyện, tiểu thuyết" },
            //    new GenreDTO { name = "Sách thiếu nhi" },
            //    new GenreDTO { name = "Tâm lý, tâm linh, tôn giáo" },
            //};
            //(bool isSuccess, string message) = GenreService.Ins.CreateNewGenre(new GenreDTO { name = "Chính trị – pháp luật" });
            //genres.ForEach(gen =>
            //{
            //    GenreService.Ins.CreateNewGenre(gen);
            //});



            // EMPLOYEEEE
            var newEmployee = new EmployeeDTO()
            {
                name = "Lee Phong new",
                phoneNumber = "09875123444",
                birthDate = new DateTime(2002, 5, 26),
                gender = "Nam", //Nam or Nữ
                startingDate = DateTime.Now,
                email = "20521111@gm.uit.edu.vn",
                account = new AccountDTO
                {
                    username = "lephong",
                    password = "123456",
                    roleId = 1,
                }
            };

            //(bool success, string message) = EmployeeService.Ins.CreateNewEmployee(newEmployee);
            //var employees = EmployeeService.Ins.GetAllEmployees();
            //newEmployee.id = "NV0002";
            //newEmployee.accountId = 3;
            //(bool uupdateSuccess, string updateMessage) = EmployeeService.Ins.UpdateEmployee(newEmployee);
            //(bool deleteSuccess, string deleteMessage) = EmployeeService.Ins.DeleteEmployee("NV0003");

            List<ReaderTypeDTO> readerTypeList = new List<ReaderTypeDTO>(){
                new ReaderTypeDTO
                {
                    name = "Độc giả A"
                },
                new ReaderTypeDTO
                {
                    name = "Độc giả B"
                },
                new ReaderTypeDTO
                {
                    name = "Độc giả C"
                },
            };

            //readerTypeList.ForEach(r =>
            //{
            //    ReaderTypeService.Ins.CreateReaderType(r);
            //});

            var data = ReaderTypeService.Ins.GetAllReaderTypes();


            //Reader Card
            int duration = ParameterService.Ins.GetRuleValue(Rules.VALIDITY_PERIOD_OF_CARD);
            var createdAt = DateTime.Now;
            var newReaderCard = new ReaderCardDTO()
            {
                name = "Tesst",
                address = "Kí túc xá khu A",
                readerTypeId = 3,
                email = "tesst123@gmail.com",
                createdAt = createdAt,
                expiryDate = createdAt.AddMonths(30),
                gender = "Nữ",
                birthDate = new DateTime(2002, 5, 26),
                employeeId = "NV0002",
            };
            //(bool isSuccess, string message) = ReaderService.Ins.CreateNewReaderCard(newReaderCard);
            (AccountDTO user, string mesasage) = AuthService.Ins.Login("datdo", "123456");

            var email = AuthService.Ins.GetAccountByUsername("datdo");
            (bool resetSuccess, string resetMessage) = AuthService.Ins.ResetPassword("datdo","1234567");
            (var userLogin, string message) = AuthService.Ins.Login("datdo","1234567");
            var a = "";

            //BOOK IMPORT
            List<BaseBookDTO> baseBookDTOs = new List<BaseBookDTO>()
            {
                //new BaseBookDTO
                //{
                //    name = "Cho tôi xin một vé đi tuổi thơ",
                //    genreId=5,
                //    authors = new List<AuthorDTO> {
                //    new AuthorDTO{ id=7},
                //    new AuthorDTO{ id=2},
                //    new AuthorDTO{ id=3}},
                //},
                //new BaseBookDTO
                //{
                //    name = "Tôi Thấy Hoa Vàng Trên Cỏ Xanh",
                //    genreId=5,
                //    authors = new List<AuthorDTO> {   
                //    new AuthorDTO{ id=7}
                //    },
                //},
                //new BaseBookDTO
                //{
                //    name = " Tâm lý học và đời sống",
                //    genreId=5,
                //    authors = new List<AuthorDTO> {
                //    new AuthorDTO{ id=7}
                //    },
                //},
                //new BaseBookDTO
                //{
                //    name = "Đắc nhân tâm",
                //    genreId=3,
                //    authors = new List<AuthorDTO> {
                //    new AuthorDTO{ id=3}
                //    },
                //},
                new BaseBookDTO
                {
                    name = "Đừng Làm Việc Chăm Chỉ Hãy Làm Việc Thông Minh",
                    genreId=3,
                    authors = new List<AuthorDTO> {
                    new AuthorDTO{ id=3},
                    new AuthorDTO{ id=2}
                    },
                },
                new BaseBookDTO
                {
                    name = "Nghệ Thuật Từ Chối – Cách Nói Không Mà Vẫn Có Được Đồng Thuận",
                    genreId=5,
                    authors = new List<AuthorDTO> {
                    new AuthorDTO{ id=3},
                    new AuthorDTO{ id=1}
                    },
                },
            };

            //foreach (var item in baseBookDTOs)
            //{
            //    (bool isSucess, string messageBase) = BaseBookService.Ins.CreateBaseBook(item);
            //}
            //(bool isSuccess, string message) = BaseBookService.Ins.UpdateBaseBook(new BaseBookDTO
            //{
            //    id = "BB0001",
            //    name = "Cho tôi xin một vé đi tuổi thơ",
            //    genreId = 4,
            //    authors = new List<AuthorDTO> {
            //        new AuthorDTO{ id=7},
            //        new AuthorDTO{ id=1},
            //        new AuthorDTO{ id=2}},
            //});



            var importReceiptDetailList = new List<ImportReceiptDetailDTO> {
                new ImportReceiptDetailDTO(){
                    unitPrice=30000,
                    quantity=10,
                    book=  new BookDTO()
                    {
                          baseBookId = "BB0001",
                          yearOfPublication = 2011,
                        publisher = "Nhà xuất bản Trẻ",
                        isNew = true,
                    },
                },
                new ImportReceiptDetailDTO(){
                    unitPrice=30000,
                    quantity=15,
                    book=  new BookDTO()
                    {
                        baseBookId = "BB0002",
                        yearOfPublication = 2014,
                        publisher = "Nhà xuất bản Trẻ",
                        isNew = true,
                    },
                },
                  new ImportReceiptDetailDTO(){
                    unitPrice=30000,
                    quantity=12,
                    book=  new BookDTO()
                    {
                        baseBookId = "BB0003",
                        yearOfPublication = 2016,
                        publisher = "Nhà xuất bản Trẻ",
                        isNew = true,
                    },
                },

            };

            var imReceipt = new ImportReceiptDTO
            {
                supplier = "Thư viện UIT",
                createdAt = DateTime.Now,
                employeeId = "NV0001",
                importReceiptDetailList = importReceiptDetailList,
            };
            //(bool isImportSuccess, string message) = ImportService.Ins.CreateNewBookImportReceipt(imReceipt);

            //var data = BaseBookService.Ins.GetAllBaseBook();


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
            var allImportReceipt = ImportService.Ins.GetAllImportReceipt();
            var importReceiptDetails = ImportService.Ins.GetImportReceiptDetail("IPR0002");
            var bookList = BookService.Ins.GetAllBook();
            var availableBookList = BookService.Ins.GetAllAvailableBook();

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
            //(bool successEdit, string messageEdit) = RoleService.Ins.EditRolePermission(2, new List<RoleDetailsDTO>
            //    {
            //        new RoleDetailsDTO
            //        {
            //        permissionId = 3,
            //        isPermitted = true
            //        },
            //         new RoleDetailsDTO
            //        {
            //        permissionId = 4,
            //        isPermitted = true
            //        },
            //    });

            //var roleList = RoleService.Ins.GetAllRoles();
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


            var reader = ReaderService.Ins.GetReaderInfo("reader0003");

            //int borrowingDuration = ParameterService.Ins.GetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW);
            //int max = ParameterService.Ins.GetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM);
            //int fineDaily = ParameterService.Ins.GetRuleValue(Rules.FINE);
            ////Check số sách tối đa có thể mượn
            //BorrowingCardDTO cardDTO = new BorrowingCardDTO()
            //{
            //    readerCardId = "reader0003",
            //    borrowingDate = DateTime.Now,
            //    dueDate = DateTime.Now.AddDays(borrowingDuration),
            //    employeeId = "NV0001",
            //};
            //var bookInfoIdList = new List<string>()
            //{
            //    "BOOK0001_003",
            //};
            //(bool success, string messageFromBorrowing) = BorrowingReturnService.Ins.CreateBorrowingCard(cardDTO, bookInfoIdList);

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

            var fineReceipt = new FineReceiptDTO()
            {
                amount = 3000,
                createdAt = DateTime.Now,
                employeeId = "NV0001",
                readerCardId = "READER0001",
            };

            //(bool isSucc, string message) = FineReceiptService.Ins.CreateFineReceipt(fineReceipt);


            var books = BookService.Ins.GetAllAvailableBook();
            var delayBorrowingCards = BorrowingReturnService.Ins.GetDelayBorrowingCardsByReaderId("READER0001");
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
            mail.IsBodyHtml = true;

            //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetResetPasswordTemplate(randomCode), null, "text/html");

            //Add view to the Email Message
            //mail.AlternateViews.Add(htmlView);

            mail.From = new MailAddress(APP_EMAIL, "Squadin Cinema");
            mail.To.Add(email);
            mail.Subject = "Lấy lại mật khẩu đăng nhập";

            smtp.Send(mail);
        }
    }
}
