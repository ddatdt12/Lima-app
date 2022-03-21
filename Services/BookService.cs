using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BookService
    {
        private BookService() { }
        private static BookService _ins;
        public static BookService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private string CreateBookId(string maxId)
        {
            if (maxId is null)
            {
                return "BOOK0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "BOOK" + newIdString.Substring(newIdString.Length - 4, 4);
        }
        private string CreateBookInfoId(string bookId, int index)
        {
            if (bookId is null)
            {
                return "BOOK0001_001";
            }
            string newIdString = $"0000{index}";
            return bookId + "_" + newIdString.Substring(newIdString.Length - 3, 3);
        }

        private int GetBookInfoIndex(string bookInfoId)
        {
            if (bookInfoId is null)
            {
                return 0;
            }
            int index = int.Parse(bookInfoId.Substring(bookInfoId.Length - 3, 3));

            return index;
        }
        public List<BookDTO> GetAllBook()
        {
            try
            {
                List<BookDTO> bookList = (from s in DataProvider.Ins.DB.Books
                                          where !s.isDeleted
                                          select new BookDTO
                                          {
                                              id = s.id,
                                              name = s.name,
                                              author = new AuthorDTO
                                              {
                                                  id = s.Author.id,
                                                  name = s.Author.name,
                                              },
                                              authorId = s.Author.id,
                                              genreId = s.genreId,
                                              quantity = s.quantity,
                                              publisher = s.publisher,
                                              yearOfPublication = s.yearOfPublication,
                                              genre = new GenreDTO
                                              {
                                                  id = s.Genre.id,
                                                  name = s.Genre.name,
                                              },
                                              bookInfoes = s.BookInfoes
                                              .Where(bI => !bI.isDeleted)
                                              .Select(bI =>
                                              new BookInfoDTO
                                              {
                                                  id = bI.id,
                                                  BookId = bI.bookId,
                                                  status = bI.status
                                              }).ToList()
                                          }).ToList();
                return bookList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CreateBookInfoList(LibraryManagementEntities context, string bookId, int quantity)
        {
            List<BookInfo> listBookInfoes = new List<BookInfo>();
            string maxBookInfoId = context.BookInfoes.Where(bI => bI.bookId == bookId).Max(bI => bI.id);
            int cIndex = GetBookInfoIndex(maxBookInfoId);
            for (int i = 0; i < quantity; i++)
            {
                cIndex++;
                listBookInfoes.Add(new BookInfo
                {
                    id = CreateBookInfoId(bookId, cIndex),
                    bookId = bookId,
                    status = true,
                }
                );
            }
            context.BookInfoes.AddRange(listBookInfoes);
        }

        public (bool isSuccess, string message) ImportBooks(List<BookDTO> bookList)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                string maxBookId = context.Books.Max(b => b.id);


                //Create new book and list book info belong to book quantity
                var newBookList = bookList.Where(b => b.isNew).Select(b => new Book
                {
                    name = b.name,
                    publisher = b.publisher,
                    quantity = b.quantity,
                    authorId = b.authorId,
                    yearOfPublication = b.yearOfPublication,
                    genreId = b.genreId,
                }).ToList();

                if (newBookList.Count() > 0)
                {
                    newBookList.ForEach((b) =>
                    {
                        string bookId = CreateBookId(maxBookId);
                        b.id = bookId;
                        context.Books.Add(b);
                        CreateBookInfoList(context, bookId, b.quantity);
                        maxBookId = bookId;
                    });
                }

                //Update book quantity and create list book info belong to book quantity
                IEnumerable<BookDTO> bookListIENum = bookList.Where(b => b.id != null && !b.isNew);
                if (bookListIENum.Count() > 0)
                {
                    //Hash Map
                    Dictionary<string, int> bookQuanityMap = bookListIENum.ToDictionary(b => b.id, b => b.quantity);
                    List<string> bookIdList = bookListIENum.Select(b => b.id).ToList();

                    List<Book> bookListInDB = context.Books.Where(b => bookIdList.Contains(b.id)).ToList();
                    foreach (var book in bookListInDB)
                    {
                        if (bookQuanityMap.ContainsKey(book.id))
                        {
                            int quantity = bookQuanityMap[book.id];
                            book.quantity = book.quantity + quantity;
                            CreateBookInfoList(context, book.id, quantity);
                        }
                    }
                }


                context.SaveChanges();
                return (true, "Nhập sách thành công!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (false, "Lỗi hệ thống!");
            }
        }
        public (bool isSuccess, string message) UpdateBook(BookDTO updatedBook)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var book = context.Books.Find(updatedBook.id);
                if (book is null || book.isDeleted)
                {
                    return (false, "Sách không tồn tại!");
                }
                book.name = updatedBook.name;
                book.authorId = updatedBook.authorId;
                book.genreId = updatedBook.genreId;
                book.publisher = updatedBook.publisher;
                book.yearOfPublication = updatedBook.yearOfPublication;

                context.SaveChanges();
                return (true, "Cập nhật sách!");

            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống!");
            }
        }

        public (bool isSuccess, string message) DeleteBook(string bookId)
        {
            try
            {
                var book = DataProvider.Ins.DB.Books.Find(bookId);
                if (book is null || book.isDeleted)
                {
                    return (false, "Sách không tồn tại!");
                }

                book.isDeleted = true;
                DataProvider.Ins.DB.SaveChanges();
                return (true, "Xóa sách thành công!");

            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống!");
            }
        }

        public (bool isSuccess, string message) DeleteBookInfo(string bookInfoId)
        {
            try
            {
                var bookInfo = DataProvider.Ins.DB.BookInfoes.Find(bookInfoId);


                var book = DataProvider.Ins.DB.Books.Find(bookInfo.bookId);
                if (bookInfo is null || bookInfo.isDeleted)
                {
                    return (false, "Sách không tồn tại!");
                }
                book.quantity--;
                bookInfo.isDeleted = true;
                DataProvider.Ins.DB.SaveChanges();
                return (true, "Xóa sách thành công!");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống!");
            }
        }
    }
}
