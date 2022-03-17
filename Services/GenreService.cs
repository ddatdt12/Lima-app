using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class GenreService
    {
        private GenreService() { }
        private static GenreService _ins;
        public static GenreService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new GenreService();
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

        public (bool, string message) AddGenre(GenreDTO genre)
        {
            try
            {
                LibraryManagementEntities context = DataProvider.Ins.DB;
                var genreInDB = context.Genres.Where(g => g.name == genre.name).FirstOrDefault();
                if (genreInDB != null)
                {
                    return (false, "Thể loại sách này đã tồn tại");
                }
                context.Genres.Add(new Genre
                {
                    name = genre.name,
                });
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                return (false, e.Message);

            }
            catch (DbUpdateException e)
            {
                return (false, e?.InnerException.Message);
            }
            return (true, "");
        }

        public (bool, string message) EditGenre(int GenreId, string newDisplayName)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var genre = context.Genres.Where(g => g.id == GenreId).FirstOrDefault();
                if (genre == null)
                {
                    return (false, "Genre don't exist");
                }
                genre.name = newDisplayName;
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                return (false, e.Message);

            }
            catch (DbUpdateException e)
            {
                return (false, e.Message);
            }
            return (true, "");

        }


    }
}
