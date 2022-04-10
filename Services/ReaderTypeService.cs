using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ReaderTypeService
    {
        private ReaderTypeService() { }
        private static ReaderTypeService _ins;
        public static ReaderTypeService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReaderTypeService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public List<ReaderTypeDTO> GetAllReaderTypes()
        {
            try
            {
                var readerTypes = (from s in DataProvider.Ins.DB.ReaderTypes
                               where !s.isDeleted
                               select new ReaderTypeDTO { id = s.id, name = s.name}).ToList();
                return readerTypes;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public (bool, string message) CreateReaderType(ReaderTypeDTO readerType)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                var isExist = context.ReaderTypes.Any(x => x.name == readerType.name);

                if (isExist)
                {
                    return (false, "Loại độc giả đã tồn tại!");
                }
                var newReaderType = new ReaderType { name = readerType.name };
                context.ReaderTypes.Add(newReaderType);
                context.SaveChanges();
                readerType.id = newReaderType.id;
                return (true, "Thêm loại độc giả thành công");
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

        public (bool, string message) EditReaderType(ReaderTypeDTO updatedReaderType)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                var isExist = context.ReaderTypes.Any(x => x.name == updatedReaderType.name && x.id != updatedReaderType.id);

                if (isExist)
                {
                    return (false, "Tên loại độc giả đã tồn tại!");
                }

                var readerType = context.ReaderTypes.Find(updatedReaderType.id);
                readerType.name = updatedReaderType.name;
                context.SaveChanges();
                return (true, "Cập nhật loại độc giả thành công");
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
        public (bool, string message) DeleteReaderType(int readerTypeId)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                var readerType = context.ReaderTypes.Find(readerTypeId);

                if (readerType is null)
                {
                    return (false, "Loại độc giả không tồn tại!");
                }

               var isRef =  context.ReaderCards.Any(r => r.readerTypeId == readerTypeId);
                if (!isRef)
                {
                    context.ReaderTypes.Remove(readerType);
                }
                else
                {
                    readerType.isDeleted = true;
                }
                context.SaveChanges();
                return (true, "Xóa loại độc giả thành công");
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

      

    }
}
