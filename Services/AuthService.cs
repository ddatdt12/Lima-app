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


                if (account.Employees.Count() > 0)
                {
                    user.type = Utils.AccountType.EMPLOYEE;
                    user.employee = account.Employees.Select(e => new EmployeeDTO
                    {
                        id = e.id,
                        name = e.name,
                        email = e.email,
                        phoneNumber = e.phoneNumber,
                        birthDate = e.birthDate,
                        gender = e.gender,
                        startingDate = e.startingDate,
                        accountId = e.accountId
                    }).FirstOrDefault();
                }
                else if (account.ReaderCards.Count() > 0)
                {
                    user.type = Utils.AccountType.READER_CARD;
                    user.reader = account.ReaderCards.Select(s => new ReaderCardDTO
                    {
                        id = s.id,
                        name = s.name,
                        address = s.address,
                        expiryDate = s.expiryDate,
                        employeeId = s.employeeId,
                        readerTypeId = s.readerTypeId,
                        totalFine = s.totalFine ?? 0,
                        readerType = new ReaderTypeDTO { id = s.readerTypeId , name = s.ReaderType.name},
                        email = s.email,
                        createdAt = s.createdAt,
                        gender = s.gender,
                        birthDate = s.birthDate,
                    }).FirstOrDefault();
                }
                else
                {
                    return (null, "Tài khoản không tồn tại!");
                }
                return (user, "Đăng nhập thành công!");
            }
            catch (Exception e)
            {
                return (null, "Lỗi hệ thống");
            }

        }

        public string GetAccountByUsername(string username)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var account = context.Accounts.Where(c => c.username == username).FirstOrDefault();
                string email;

                if (account is null)
                {
                    return null;
                }

                if (account.Employees.Count >= 0)
                {
                    email = account.Employees.FirstOrDefault()?.email;
                }
                else
                {
                    email = account.ReaderCards.FirstOrDefault()?.email;
                }

                return email;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public (bool, string) ResetPassword(string username, string newPassword)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var account = context.Accounts.Where(a => a.username == username).FirstOrDefault();

                if (account == null)
                {
                    return (false, "Tài khoản không tồn tại!");
                }

                account.password = Helper.MD5Hash(newPassword);
                context.SaveChanges();
                return (true, "Cập nhật mật khẩu thành công!");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống!");
            }

        }
    }
}
