using CinemaManagement.Utils;
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

        public (AccountDTO, string message) Login(string username, string password)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                string hashPass = Helper.MD5Hash(password);
                var account = context.Accounts.Where(c => c.username == username).FirstOrDefault();


                if (account == null || account.password != hashPass)
                {
                    return (null, "Tài khoản hoặc mật khẩu không đúng!");
                }

                var user = new AccountDTO
                {
                    id = account.id,
                    username = username,
                    role = new RoleDTO
                    {
                        id = account.Role.id,
                        name = account.Role.name,
                        roleDetaislList = account.Role.RoleDetails.Select(rD => new RoleDetailDTO
                        {
                            permissionId = rD.permissionId,
                            roleId = rD.roleId,
                            isPermitted = rD.isPermitted,
                        }).ToList(),
                    },
                };


                if(account.Employees.Count() > 0)
                {
                    user.type = Utils.AccountType.EMPLOYEE;
                }
                else if (account.ReaderCards.Count() > 0)
                {
                    user.type = Utils.AccountType.READER_CARD;
                }
                else
                {
                    return (null, "Tài khoản không tồn tại!");
                }
                return (user, "Đăng nhập thành công!");
            }
            catch (Exception)
            {
                return (null, "Lỗi hệ thống");
            }

        }
    }
}
