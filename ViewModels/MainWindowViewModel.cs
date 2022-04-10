using LibraryManagement.DTOs;
using LibraryManagement.Services;
using LibraryManagement.Utils;
using LibraryManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;


namespace LibraryManagement.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public MainWindowViewModel()
        {
            LoadedWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
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

                //List<BookDTO> bookDTOs = new List<BookDTO>()
                //{
                //    new BookDTO()
                //    {
                //        name = "Cô gái đến từ hôm qua",
                //        genreId = 1,
                //        authorId = 7,
                //        yearOfPublication = 1990,
                //        publisher = "Nhà xuất bản Trẻ",
                //        quantity = 5,
                //        isNew = true,
                //     },
                //    new BookDTO()
                //    {
                //        name = "Mắt Biếc",
                //        genreId = 1,
                //        authorId = 3,
                //        yearOfPublication = 1990,
                //        publisher = "Nhà xuất bản Trẻ",
                //        quantity = 10,
                //        isNew = true,
                //     },
                //    new BookDTO()
                //    {
                //        name = "Cho tôi xin một vé đi tuổi thơ",
                //        genreId = 1,
                //        authorId = 7,
                //        yearOfPublication = 2008,
                //        publisher = "Nhà xuất bản Trẻ",
                //        quantity = 5,
                //        isNew = true,
                //     },
                //    //new BookDTO()
                //    //{
                //    //    id = "BOOK0001",
                //    //    quantity = 4,
                //    // },
                //    //new BookDTO()
                //    //{
                //    //    id = "BOOK0002",
                //    //    quantity = 3,
                //    //},
                // };
                //(bool isImportSuccess, string importMessage) = BookService.Ins.ImportBooks(bookDTOs);
                //int minAge = ParameterService.Ins.GetRuleValue(Rules.MIN_AGE);
                //int maxAge = ParameterService.Ins.GetRuleValue(Rules.MAX_AGE);
                //int ALLOWED_BOOK_MAXIMUM = ParameterService.Ins.GetRuleValue(Rules.ALLOWED_BOOK_MAXIMUM);
                //int YEAR_PUBLICATION_PERIOD = ParameterService.Ins.GetRuleValue(Rules.YEAR_PUBLICATION_PERIOD);
                //int MAXIMUM_NUMBER_OF_DAYS_TO_BORROW = ParameterService.Ins.GetRuleValue(Rules.MAXIMUM_NUMBER_OF_DAYS_TO_BORROW);
                ////(bool isUpdateSuccess, string updateMessage)  = BookService.Ins.UpdateBook(new BookDTO
                ////{
                ////    id="BOOK0004",
                ////    name = "Cô gái đến từ hôm qua test",
                ////    genreId = 1,
                ////    authorId = 2,
                ////    yearOfPublication = 1990,
                ////    publisher = "Nhà xuất bản Trẻ",
                ////});
                //var bookList = BookService.Ins.GetAllBook();


                //var role = new RoleDTO
                //{
                //    id = 1,
                //    position = "Thủ thư",
                //    roleDetaislList = new List<RoleDetailsDTO>
                //    {
                //        new RoleDetailsDTO
                //        {
                //        permission = 0,
                //        isPermitted = true
                //        },
                //         new RoleDetailsDTO
                //         {
                //        permission = 1,
                //        isPermitted = true
                //        },
                //        new RoleDetailsDTO{
                //        permission = 2,
                //        isPermitted = false
                //        },
                //                 new RoleDetailsDTO{
                //        permission = 3,
                //        isPermitted = true
                //        },
                //                    new RoleDetailsDTO{
                //        permission = 4,
                //        isPermitted = true
                //        },
                //                       new RoleDetailsDTO{
                //        permission = 5,
                //        isPermitted = false
                //        },
                //                          new RoleDetailsDTO{
                //        permission = 6,
                //        isPermitted = false
                //        },

                //    },
                //};

                //(bool success, string message) = RoleService.Ins.CreateNewRole(role);
                //var roleList = RoleService.Ins.GetAllRoles();
            }


    }
}
