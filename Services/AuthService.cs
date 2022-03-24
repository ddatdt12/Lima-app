using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class AuthService
    {
        private AuthService() { }
        private static AuthService _ins;
        public static AuthService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AuthService();
                }
                return _ins;
            }
            private set => _ins = value;
        }


        //public List<AuthorDTO> GetAllAuthor()
        //{
        //    try
        //    {
        //        List<AuthorDTO> authors;
        //        authors = (from s in DataProvider.Ins.DB.Authors
        //                   select new AuthorDTO { id = s.id, name = s.name }).ToList();
        //        return authors;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //}
    }
}
