using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ReceiptService
    {
        private ReceiptService() { }
        private static ReceiptService _ins;
        public static ReceiptService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReceiptService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public List<GenreDTO> GetAllGenre()
        {
            try
            {
                List<GenreDTO> genres;
                genres = (from s in DataProvider.Ins.DB.Genres
                          select new GenreDTO { id = s.id, name = s.name }).ToList();
                return genres;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public (bool, string message) CreateBookImportReceipt(GenreDTO genre)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var genreInDB = context.Genres.Where(g => g.name == genre.name).FirstOrDefault();
                if (genreInDB != null)
                {
                    return (false, "Thể loại sách này đã tồn tại");
                }
                var newGenre = new Genre
                {
                    name = genre.name,
                };
                context.Genres.Add(newGenre);
                context.SaveChanges();

                genre.id = newGenre.id;
                return (true, "Thêm thể loại thành công");
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

        public (bool, string message) EditGenre(GenreDTO updatedGenre)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var genre = context.Genres.Where(g => g.id == updatedGenre.id).FirstOrDefault();
                if (genre is null)
                {
                    return (false, "Thể loại không tồn tại");
                }
                genre.name = updatedGenre.name;
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

        public (bool, string message) DeleteGenre(int genreId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var related = context.BaseBooks.Where(b => b.genreId == genreId).Any();
                if (related)
                {
                    return (false, "Đã có thể loại sách thuộc thể loại này không thể xóa");
                }
                var genre = context.Genres.Where(g => g.id == genreId).FirstOrDefault();
                if (genre is null)
                {
                    return (false, "Genre don't exist");
                }
                context.Genres.Remove(genre);
                context.SaveChanges();
                return (true, "Xóa thể loại thành công");
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

    }
}
