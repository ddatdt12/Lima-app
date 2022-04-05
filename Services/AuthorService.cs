using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class AuthorService
    {
        private AuthorService() { }
        private static AuthorService _ins;
        public static AuthorService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AuthorService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public List<AuthorDTO> GetAllAuthor()
        {
            try
            {
                List<AuthorDTO> authors;
                authors = (from s in DataProvider.Ins.DB.Authors
                           select new AuthorDTO { id = s.id, name = s.name }).ToList();
                return authors;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public (bool, string message) CreateNewAuthor(AuthorDTO author)
        {
            try
            {
                LibraryManagementEntities context = DataProvider.Ins.DB;
                var authorInDB = context.Authors.Where(g => g.name == author.name).FirstOrDefault();
                if (authorInDB != null)
                {
                    return (false, "Tác giả này đã tồn tại");
                }
                var newAuthor = new Author
                {
                    name = author.name,
                    birthDate = author.birthDate,
                };
                context.Authors.Add(newAuthor);
                context.SaveChanges();
                author.id = newAuthor.id;
                return (true, "Thêm tác giả thành công");
            }
            catch (DbEntityValidationException e)
            {
                return (false, e.Message);

            }
            catch (DbUpdateException e)
            {
                return (false, e?.InnerException.Message);
            }
        }

        public (bool, string message) EditAuthor(AuthorDTO updatedAuthor)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var author = context.Authors.Where(g => g.id == updatedAuthor.id).FirstOrDefault();
                if (author is null)
                {
                    return (false, "Thể loại không tồn tại");
                }
                author.name = updatedAuthor.name;
                author.birthDate = updatedAuthor.birthDate;

                context.SaveChanges();
                return (true, "");
            }
            catch (DbEntityValidationException e)
            {
                return (false, e.Message);

            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }

        }

        //public (bool, string message) DeleteAuthor(int authorId)
        //{
        //    try
        //    {
        //        var context = DataProvider.Ins.DB;
        //        var related = context.BaseBooks.Where(b => b.authorId == authorId).Any();
        //        if (related)
        //        {
        //            return (false, "Đã có sách của tác giả này. Không thể xóa!");
        //        }
        //        var genre = context.Authors.Where(g => g.id == authorId).FirstOrDefault();
        //        if (genre is null)
        //        {
        //            return (false, "Tác giả không tồn tại");
        //        }
        //        context.Authors.Remove(genre);
        //        context.SaveChanges();
        //        return (true, "Xóa thể loại thành công");
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        return (false, e.Message);

        //    }
        //    catch (DbUpdateException e)
        //    {
        //        return (false, e.Message);
        //    }
        //}

    }
}
