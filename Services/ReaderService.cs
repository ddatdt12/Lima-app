using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ReaderService
    {
        private ReaderService() { }
        private static ReaderService _ins;
        public static ReaderService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReaderService();
                }
                return _ins;
            }
            private set => _ins = value;
        }
        private string CreateReaderCardId(string maxId)
        {
            if (maxId is null)
            {
                return "READER0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "READER" + newIdString.Substring(newIdString.Length - 4, 4);
        }
        public List<ReaderCardDTO> GetAllReaderCards()
        {
            try
            {
                List<ReaderCardDTO> employees = (from s in DataProvider.Ins.DB.ReaderCards
                                                 select new ReaderCardDTO
                                                 {
                                                     id = s.id,
                                                     name = s.name,
                                                     address = s.address,
                                                     expiryDate = s.expiryDate,
                                                     employeeId = s.employeeId,
                                                     readerTypeId = s.readerTypeId,
                                                     totalFine = s.totalFine ?? 0,
                                                     readerType = new ReaderTypeDTO { id = s.readerTypeId },
                                                     identityCard = s.identityCard,
                                                     email = s.email,
                                                     createdAt = s.createdAt,
                                                     gender = s.gender,
                                                     birthDate = s.birthDate,
                                                     employee = new EmployeeDTO
                                                     {
                                                         id = s.Employee.id,
                                                         email = s.Employee.email,
                                                         name = s.Employee.name,
                                                         birthDate = s.Employee.birthDate,
                                                         gender = s.Employee.gender,
                                                         phoneNumber = s.Employee.phoneNumber,
                                                     },
                                                 }).ToList();
                return employees;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public (bool, string message) CreateNewReaderCard(ReaderCardDTO readerCard)
        {
            try
            {
                LibraryManagementEntities context = DataProvider.Ins.DB;
                var emailExist = context.ReaderCards
                    .Where(g => g.expiryDate > DateTime.Now && g.email == readerCard.email).Any();

                if (emailExist)
                {
                    return (false, "Email đã được sử dụng");
                }
                var identityCardExist = context.ReaderCards
                    .Where(g => g.expiryDate > DateTime.Now && g.identityCard == readerCard.identityCard).Any();

                if (identityCardExist)
                {
                    return (false, "CMND/CCCD đã được sử dụng");
                }
                string maxId = context.ReaderCards.Max(r => r.id);

                var newReaderCard = new ReaderCard
                {
                    id = CreateReaderCardId(maxId),
                    name = readerCard.name,
                    address = readerCard.address,
                    employeeId = readerCard.employeeId,
                    readerTypeId = readerCard.readerTypeId,
                    totalFine = readerCard.totalFine,
                    identityCard = readerCard.identityCard,
                    email = readerCard.email,
                    createdAt = readerCard.createdAt,
                    expiryDate = readerCard.expiryDate,
                    gender = readerCard.gender,
                    birthDate = readerCard.birthDate,
                };

                context.ReaderCards.Add(newReaderCard);
                context.SaveChanges();
                readerCard.id = newReaderCard.id;
                return (true, "Thêm tác giả thành công");
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

        public (bool, string message) UpdateReaderCard(ReaderCardDTO updatedReaderCard)
        {
            try
            {
                LibraryManagementEntities context = DataProvider.Ins.DB;
                var emailExist = context.ReaderCards
                    .Where(g => g.expiryDate > DateTime.Now && g.id != updatedReaderCard.id && g.email == updatedReaderCard.email).Any();

                if (emailExist)
                {
                    return (false, "Email đã được sử dụng");
                }
                var identityCardExist = context.ReaderCards
                    .Where(g => g.expiryDate > DateTime.Now && g.id != updatedReaderCard.id && g.identityCard == updatedReaderCard.identityCard).Any();

                if (identityCardExist)
                {
                    return (false, "CMND/CCCD đã được sử dụng");
                }

                var readerCard = context.ReaderCards.Find(updatedReaderCard.id);

                readerCard.name = updatedReaderCard.name;
                readerCard.address = updatedReaderCard.address;
                readerCard.employeeId = updatedReaderCard.employeeId;
                readerCard.readerTypeId = updatedReaderCard.readerTypeId;
                readerCard.totalFine = updatedReaderCard.totalFine;
                readerCard.identityCard = updatedReaderCard.identityCard;
                readerCard.email = updatedReaderCard.email;
                readerCard.createdAt = updatedReaderCard.createdAt;
                readerCard.expiryDate = updatedReaderCard.expiryDate;
                readerCard.gender = updatedReaderCard.gender;
                readerCard.birthDate = updatedReaderCard.birthDate;

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

        //public (bool, string message) DeleteAuthor(int authorId)
        //{
        //    try
        //    {
        //        var context = DataProvider.Ins.DB;
        //        var related = context.BaseBooks.Where(b => b.authorId == authorId).Any();
        //        if (related)
        //        {
        //            return (false, "Đã có sách của tác giả này. Không thể xóa!");
        //        }
        //        var genre = context.Authors.Where(g => g.id == authorId).FirstOrDefault();
        //        if (genre is null)
        //        {
        //            return (false, "Tác giả không tồn tại");
        //        }
        //        context.Authors.Remove(genre);
        //        context.SaveChanges();
        //        return (true, "Xóa thể loại thành công");
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        return (false, e.Message);

        //    }
        //    catch (DbUpdateException e)
        //    {
        //        return (false, e.Message);
        //    }
        //}

    }
}
