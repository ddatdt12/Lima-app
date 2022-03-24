using LibraryManagement.DTOs;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class RoleService
    {
        private RoleService() { }
        private static RoleService _ins;
        public static RoleService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new RoleService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public List<RoleDTO> GetAllRoles()
        {
            try
            {
                List<RoleDTO> roles =
                    (from s in DataProvider.Ins.DB.Roles
                     select new RoleDTO
                     {
                         id = s.id,
                         position = s.position,
                         roleDetaislList = s.RoleDetails.Select(rD =>
                          new RoleDetailsDTO
                          {
                              isPermitted = rD.isPermitted,
                              permission = rD.permission,
                              roleId = rD.permission
                          }).ToList()
                     }).ToList();
                return roles;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public (bool, string message) CreateNewRole(RoleDTO role)
        {
            var context = DataProvider.Ins.DB;

            try
            {
                var isExist = context.Roles.Where(g => g.position == role.position).Any();
                if (isExist)
                {
                    return (false, "Vai trò này đã tồn tại");
                }
                var newRole = new Role
                {
                    position = role.position,
                };

                context.Roles.Add(newRole);
                context.SaveChanges();

                role.id = newRole.id;
                return (true, "Thêm vai trò mới thành công");
            }
            catch (Exception e)
            {
                return (false, "Lỗi hệ thống");
            }

        }
        //public (bool, string message) CreateNewRole(RoleDTO role)
        //{
        //    var context = DataProvider.Ins.DB;
        //    using (DbContextTransaction transaction = context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            var isExist = context.Roles.Where(g => g.position == role.position).Any();
        //            if (isExist)
        //            {
        //                return (false, "Vai trò này đã tồn tại");
        //            }
        //            var newRole = new Role
        //            {
        //                position = role.position,
        //            };

        //            context.Roles.Add(newRole);
        //            context.SaveChanges();

        //            role.roleDetaislList.ForEach((per) =>
        //            {
        //                context.RoleDetails.Add(new RoleDetail
        //                { roleId = newRole.id, isPermitted = per.isPermitted, permission = per.permission }
        //                );
        //            });


        //            context.SaveChanges();
        //            transaction.Commit();

        //            role.id = newRole.id;
        //            return (true, "Thêm vai trò mới thành công");
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();
        //            return (false, "Lỗi hệ thống");
        //        }
        //    }

        //}
        public (bool, string message) EditRole(RoleDTO role)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var roleInDB = context.Roles.Where(g => g.id == role.id).FirstOrDefault();
                if (roleInDB is null)
                {
                    return (false, "Vai trò không tồn tại");
                }

                var isExist = context.Roles.Where(g => g.position == role.position).Any();
                if (isExist)
                {
                    return (false, "Tên vai trò này đã tồn tại");
                }

                roleInDB.position = role.position;
                context.SaveChanges();
                return (true, "Cập nhật thành công");
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
        public (bool, string message) EditRolePermission(int roleId, List<RoleDetailsDTO> roleDetaislList)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var isExist = context.Roles.Where(g => g.id == roleId).Any();
                if (!isExist)
                {
                    return (false, "Vai trò không tồn tại");
                }

                var RoleDetails = context.RoleDetails.Where(rD => rD.roleId == roleId).ToList();
                var permissionList = roleDetaislList.ToDictionary(r => r.permission, r => r.isPermitted);
                RoleDetails.ForEach(rD =>
                {
                    if (permissionList.ContainsKey(rD.permission))
                    {
                        rD.isPermitted = permissionList[rD.permission];
                    }
                });

                context.SaveChanges();
                return (true, "Cập nhật thành công");
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

        public (bool, string message) DeleteRole(int roleId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var related = context.Employees.Where(b => b.roleId == roleId).Any();
                if (related)
                {
                    return (false, "Đã có nhân viên thuộc vai trò này, không thể xóa!");
                }
                var role = context.Roles.Find(roleId);
                var roleDetails = context.RoleDetails.Where(r => r.roleId == role.id).ToList();

                context.RoleDetails.RemoveRange(roleDetails);
                context.Roles.Remove(role);
                context.SaveChanges();
                return (true, "Xóa vai trò thành công");
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
