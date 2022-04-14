using LibraryManagement.Utils;
using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class EmployeeService
    {
        private EmployeeService() { }
        private static EmployeeService _ins;
        public static EmployeeService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new EmployeeService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private string CreateEmployeeId(string maxId)
        {
            if (maxId is null)
            {
                return "NV0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "NV" + newIdString.Substring(newIdString.Length - 4, 4);
        }
        public List<EmployeeDTO> GetAllEmployees()
        {
            try
            {
                List<EmployeeDTO> employees = (from e in DataProvider.Ins.DB.Employees
                                               where !e.isDeleted
                                               select new EmployeeDTO
                                               {
                                                   id = e.id,
                                                   name = e.name,
                                                   email = e.email,
                                                   phoneNumber = e.phoneNumber,
                                                   birthDate = e.birthDate,
                                                   gender = e.gender,
                                                   startingDate = e.startingDate,
                                                   accountId = e.accountId,
                                                   account = new AccountDTO
                                                   {
                                                       roleId = e.Account.roleId,
                                                       password = e.Account.password,
                                                       username = e.Account.username,
                                                       role = new RoleDTO { id = e.Account.roleId, name = e.Account.Role.name }
                                                   },
                                               })
                                               .ToList();
                return employees;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public (bool, string message) CreateNewEmployee(EmployeeDTO employee)
        {
            try
            {
                LibraryManagementEntities context = DataProvider.Ins.DB;
                var phoneNumberIsExist = context.Employees.Where(e => e.phoneNumber == employee.phoneNumber).Any();
                if (phoneNumberIsExist)
                {
                    return (false, "Số điện thoại đã đươc sử dụng");
                }

                var emailIsExist = context.Employees.Where(e => e.email == employee.email).Any();
                if (emailIsExist)
                {
                    return (false, "Email đã đươc sử dụng");
                }

                var usernameIsExist = context.Accounts.Where(e => e.username == employee.account.username).Any();
                if (usernameIsExist)
                {
                    return (false, "Username đã đươc sử dụng");
                }
                var maxEmployeeId = context.Employees.Max(e => e.id);

                var newAccount = new Account
                {
                    roleId = employee.account.roleId,
                    password = Helper.MD5Hash(employee.account.password),
                    username = employee.account.username,
                };

                context.Accounts.Add(newAccount);
                context.SaveChanges();

                var newEmployee = new Employee
                {
                    id = CreateEmployeeId(maxEmployeeId),
                    name = employee.name,
                    email = employee.email,
                    phoneNumber = employee.phoneNumber,
                    birthDate = employee.birthDate,
                    gender = employee.gender,
                    startingDate = employee.startingDate,
                    accountId = newAccount.id,
                };

                context.Employees.Add(newEmployee);
                context.SaveChanges();

                employee.id = newEmployee.id;
                return (true, "Thêm nhân viên thành công thành công");
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

        public (bool, string message) UpdateEmployee(EmployeeDTO updatedEmployee)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var employee = context.Employees.Find(updatedEmployee.id);
                if (employee is null)
                {
                    return (false, "Nhân viên không tồn tại");
                }
                var acc = context.Accounts.Find(employee.accountId);


                var emailIsExist = context.Employees.Where(e => e.id != updatedEmployee.id && e.email == employee.email).Any();
                if (emailIsExist)
                {
                    return (false, "Email đã đươc sử dụng");
                }

                var usernameIsExist = context.Accounts.Where(a => a.id != updatedEmployee.accountId && a.username == updatedEmployee.account.username).Any();
                if (usernameIsExist)
                {
                    return (false, "Username đã đươc sử dụng");
                }

                employee.name = updatedEmployee.name;
                employee.email = updatedEmployee.email;
                employee.phoneNumber = updatedEmployee.phoneNumber;
                employee.birthDate = updatedEmployee.birthDate;
                employee.gender = updatedEmployee.gender;
                employee.startingDate = updatedEmployee.startingDate;

                acc.username = updatedEmployee.account.username;
                acc.roleId = updatedEmployee.account.roleId;

                context.SaveChanges();
                return (true, "Cập nhật thành công!");
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
        public (bool, string message) UpdatePasswordOfEmployee(int accountId, string newPassword)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var acc = context.Accounts.Find(accountId);
                if (acc is null)
                {
                    return (false, "Tài khoản không tồn tại");
                }
                acc.password = Helper.MD5Hash(newPassword);

                context.SaveChanges();
                return (true, "Cập nhật thành công!");
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

        public (bool, string message) DeleteEmployee(string employeeId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var employee = context.Employees.Find(employeeId);
                if (employee is null)
                {
                    return (false, "Nhân viên không tồn tại");
                }
                employee.isDeleted = true;
                context.SaveChanges();
                return (true, "Xóa nhân viên thành công");
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
