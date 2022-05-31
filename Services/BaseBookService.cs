using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Services
{
    public class BaseBookService
    {
        private BaseBookService() { }
        private static BaseBookService _ins;
        public static BaseBookService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BaseBookService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private string CreateBaseBookId(string maxId)
        {
            if (maxId is null)
            {
                return "BB0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "BB" + newIdString.Substring(newIdString.Length - 4, 4);
        }
        public List<BaseBookDTO> GetAllBaseBook()
        {
            try
            {
                List<BaseBookDTO> baseBookList = (from s in DataProvider.Ins.DB.BaseBooks
                                                  where !s.isDeleted
                                                  select new BaseBookDTO
                                                  {
                                                      id = s.id,
                                                      name = s.name,
                                                      genreId = s.genreId,
                                                      genre = new GenreDTO
                                                      {
                                                          id = s.Genre.id,
                                                          name = s.Genre.name
                                                      },
                                                      authors = s.Authors.Select(a =>
                                                    new AuthorDTO
                                                    {
                                                        id = a.id,
                                                        name = a.name,
                                                        birthDate = a.birthDate ?? DateTime.Now,
                                                    }).ToList(),
                                                  }).ToList();


                return baseBookList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BaseBookDTO GetBookDetail(string baseBookId)
        {
            try
            {
                BaseBookDTO baseBook = (from s in DataProvider.Ins.DB.BaseBooks
                                        where !s.isDeleted && s.id == baseBookId
                                        select new BaseBookDTO
                                        {
                                            id = s.id,
                                            name = s.name,
                                            authors = s.Authors.Select(a =>
                                          new AuthorDTO
                                          {
                                              id = a.id,
                                              name = a.name,
                                              birthDate = a.birthDate ?? DateTime.Now,
                                          }).ToList(),
                                            books = s.Books.Where(b => !b.isDeleted).Select(b => new BookDTO
                                            {
                                                id = b.id,
                                                quantity = b.quantity,
                                                publisher = b.publisher,
                                                yearOfPublication = b.yearOfPublication,
                                                baseBookId = b.baseBookId,
                                                price = b.Price,
                                                bookInfoes = b.BookInfoes.Where(bI => !bI.isDeleted).Select(bI => new BookInfoDTO
                                                {
                                                    id = bI.id,
                                                    BookId = bI.bookId,
                                                    status = bI.status
                                                }
                                               ).ToList(),
                                            }).ToList(),
                                        }).FirstOrDefault();


                return baseBook;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public (bool success, string message) CreateBaseBook(BaseBookDTO baseBook)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                string maxId = context.BaseBooks.Max(b => b.id);

                BaseBook newBaseBook = new BaseBook()
                {
                    id = CreateBaseBookId(maxId),
                    name = baseBook.name,
                    genreId = baseBook.genreId,
                };
                foreach (var a in baseBook.authors)
                {
                    Author g = context.Authors.Find(a.id);
                    newBaseBook.Authors.Add(g);
                }

                context.BaseBooks.Add(newBaseBook);
                context.SaveChanges();
                baseBook.id = newBaseBook.id;
                return (true, "Thêm đầu sách thành công!");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống!");
            }
        }
        public (bool success, string message) UpdateBaseBook(BaseBookDTO updatedBaseBook)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var baseBook = context.BaseBooks.Find(updatedBaseBook.id);

                if (baseBook == null)
                {
                    return (false, "Đầu sách không tồn tại!");
                }


                baseBook.name = updatedBaseBook.name;
                baseBook.genreId = updatedBaseBook.genreId;
                baseBook.Authors.Clear();
                foreach (var a in updatedBaseBook.authors)
                {
                    Author g = context.Authors.Find(a.id);
                    baseBook.Authors.Add(g);
                }

                context.SaveChanges();
                return (true, "Cập nhật đầu sách thành công!");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống!");
            }
        }

        public (bool success, string message) DeleteBaseBook(string baseBookId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var baseBook = context.BaseBooks.Find(baseBookId);

                if (baseBook == null)
                {
                    return (false, "Sách không tồn tại!");
                }
                baseBook.isDeleted = true;
                context.SaveChanges();
                return (true, "Xoá đầu sách thành công!");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống!");
            }
        }
    }
}
