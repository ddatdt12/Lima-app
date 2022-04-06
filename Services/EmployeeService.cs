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
                                                   Role = new RoleDTO
                                                   {
                                                       id = e.Role.id,
                                                       name = e.Role.name,
                                                   },
                                                   roleId = e.Role.id,
                                                   birthDate = e.birthDate,
                                                   gender = e.gender,
                                                   password = e.password,
                                                   username = e.username,
                                                   startingDate = e.startingDate,
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
                if (phoneNumberIsExist)
                {
                    return (false, "Email đã đươc sử dụng");
                }

                var usernameIsExist = context.Employees.Where(e => e.username == employee.username).Any();
                if (usernameIsExist)
                {
                    return (false, "Username đã đươc sử dụng");
                }
                var maxId = context.Employees.Max(e => e.id);
                var newEmployee = new Employee
                {
                    id = CreateEmployeeId(maxId),
                    name = employee.name,
                    email = employee.email,
                    phoneNumber = employee.phoneNumber,
                    roleId = employee.roleId,
                    birthDate = employee.birthDate,
                    gender = employee.gender,
                    password = employee.password,
                    username = employee.username,
                    startingDate = employee.startingDate,
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
                employee.name = updatedEmployee.name;
                employee.email = updatedEmployee.email;
                employee.phoneNumber = updatedEmployee.phoneNumber;
                employee.roleId = updatedEmployee.roleId;
                employee.birthDate = updatedEmployee.birthDate;
                employee.gender = updatedEmployee.gender;
                employee.password = updatedEmployee.password;
                employee.username = updatedEmployee.username;
                employee.startingDate = updatedEmployee.startingDate;
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
