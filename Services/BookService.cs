using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
                                              quantity = s.quantity,
                                              publisher = s.publisher,
                                              yearOfPublication = s.yearOfPublication,
                                              baseBookId = s.baseBookId,
                                              baseBook = new BaseBookDTO
                                              {
                                                  id = s.id,
                                                  name = s.BaseBook.name,
                                                  genre = new GenreDTO
                                                  {
                                                      id = s.BaseBook.Genre.id,
                                                      name = s.BaseBook.Genre.name,
                                                  },
                                                  authors = s.BaseBook.Authors.Select(x => new AuthorDTO
                                                  {
                                                      id = x.id,
                                                      name = x.name,
                                                      birthDate = x.birthDate ?? DateTime.Now
                                                  }).ToList(),
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

        public void ImportBooks(List<BookDTO> bookList)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                string maxBookId = context.Books.Max(b => b.id);


                bookList.ForEach(book =>
                {
                    var isExist = context.Books
                     .Where(b => b.baseBookId == book.baseBookId
                     && b.yearOfPublication == book.yearOfPublication
                     && b.publisher == book.publisher).Any();
                    if (isExist)
                    {
                        string statusMessage = "Sách lần xuất bản này đã tồn tại!";
                        int statusCode = 400;
                        var ex = new Exception($"{statusMessage} - {statusCode}");
                        ex.Data.Add(statusCode, statusMessage);
                        throw ex;
                    }
                });
                //Create new book and list book info depend on book quantity
                var newBookList = bookList.Where(b => b.isNew).Select(b => new Book
                {
                    baseBookId = b.baseBookId,
                    publisher = b.publisher,
                    quantity = b.quantity,
                    yearOfPublication = b.yearOfPublication,
                }).ToList();

                if (newBookList.Count() > 0)
                {

                    for (int i = 0; i < newBookList.Count; i++)
                    {
                        string bookId = CreateBookId(maxBookId);
                        newBookList[i].id = bookId;
                        bookList[i].id = bookId;
                        context.Books.Add(newBookList[i]);
                        CreateBookInfoList(context, bookId, newBookList[i].quantity);
                        maxBookId = bookId;
                    }
                }

                //Update book quantity and create list book info depend on book quantity
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

            }
            catch (Exception e)
            {
                throw e;
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
                book.publisher = updatedBook.publisher;
                book.yearOfPublication = updatedBook.yearOfPublication;
                context.SaveChanges();
                return (true, "Cập nhật sách thành công!");

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
            catch (Exception)
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
            catch (Exception)
            {
                return (false, "Lỗi hệ thống!");
            }
        }
    }
}
