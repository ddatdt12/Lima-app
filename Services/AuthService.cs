using LibraryManagement.DTOs;
using LibraryManagement.Models;
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


     

    
    }
}
