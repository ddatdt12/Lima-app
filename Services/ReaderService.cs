using LibraryManagement.Utils;
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
            string newIdString = $"0000{int.Parse(maxId.Substring(6)) + 1}";
            return "READER" + newIdString.Substring(newIdString.Length - 4, 4);
        }

        public ReaderCardDTO GetReaderInfo(string readerId)
        {
            try
            {

                var context = DataProvider.Ins.DB;
                ReaderCard reader = context.ReaderCards.Find(readerId);
                if (reader is null || reader.isDeleted)
                {
                    return null;
                }
                var readerCard = new ReaderCardDTO
                {
                    id = reader.id,
                    name = reader.name,
                    birthDate = (DateTime)reader.birthDate,
                    expiryDate = reader.expiryDate,
                    employeeId = reader.employeeId,
                    gender = reader.gender,
                    readerTypeId = reader.readerTypeId,
                    readerType = new ReaderTypeDTO
                    {
                        id = reader.ReaderType.id,
                        name = reader.ReaderType.name
                    },
                    totalFine = reader.totalFine ?? 0,
                    address = reader.address,
                    email = reader.email,
                    createdAt = reader.createdAt,
                    employee = new EmployeeDTO
                    {
                        id = reader.employeeId,
                        name = reader.Employee.name,
                        phoneNumber = reader.Employee.phoneNumber,
                        startingDate = reader.Employee.startingDate,
                        email = reader.Employee.email,
                        gender = reader.gender,
                    },

                };
                readerCard.haveDelayBook = reader.Borrowing_ReturnCard.Count(b => b.returnedDate == null && b.dueDate < DateTime.Now) > 0;
                readerCard.numberOfBorrowingBooks = reader.Borrowing_ReturnCard.Count(b => b.returnedDate == null);

                return readerCard;
            }
            catch (Exception)
            {
                return null;
            }
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
                                                     readerType = new ReaderTypeDTO { id = s.ReaderType.id, name = s.ReaderType.name },
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
            LibraryManagementEntities context = DataProvider.Ins.DB;
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var emailExist = context.ReaderCards
                    .Where(g => g.expiryDate > DateTime.Now && g.email == readerCard.email).Any();

                    if (emailExist)
                    {
                        return (false, "Email đã được sử dụng");
                    }

                    string maxId = context.ReaderCards.Max(r => r.id);

                    string readerCardId = CreateReaderCardId(maxId);

                    var newAccount = new Account
                    {
                        roleId = 2, // Độc giả
                        username = readerCardId,
                        password = Helper.MD5Hash(readerCardId),
                    };
                    context.Accounts.Add(newAccount);
                    context.SaveChanges();
                    var newReaderCard = new ReaderCard
                    {
                        id = readerCardId,
                        name = readerCard.name,
                        address = readerCard.address,
                        employeeId = readerCard.employeeId,
                        readerTypeId = readerCard.readerTypeId,
                        totalFine = 0,
                        email = readerCard.email,
                        createdAt = readerCard.createdAt,
                        expiryDate = readerCard.expiryDate,
                        gender = readerCard.gender,
                        birthDate = readerCard.birthDate,
                        accountId = newAccount.id
                    };

                    context.ReaderCards.Add(newReaderCard);
                    context.SaveChanges();
                    transaction.Commit();
                    readerCard.id = newReaderCard.id;
                    readerCard.account = new AccountDTO
                    {
                        roleId = 2, // Độc giả
                        username = newAccount.username,
                        type = Utils.AccountType.READER_CARD
                    };
                    string readerTypeName = context.ReaderTypes.Find(newReaderCard.readerTypeId)?.name;
                    readerCard.readerType = new ReaderTypeDTO
                    {
                        id = newReaderCard.readerTypeId,
                        name = readerTypeName,
                    };
                    return (true, "Thêm thẻ độc giả thành công");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return (false, "Lỗi hệ thống");
                }
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

                var readerCard = context.ReaderCards.Find(updatedReaderCard.id);

                readerCard.name = updatedReaderCard.name;
                readerCard.address = updatedReaderCard.address;
                readerCard.employeeId = updatedReaderCard.employeeId;
                readerCard.readerTypeId = updatedReaderCard.readerTypeId;
                readerCard.totalFine = updatedReaderCard.totalFine;
                readerCard.email = updatedReaderCard.email;
                readerCard.createdAt = updatedReaderCard.createdAt;
                readerCard.expiryDate = updatedReaderCard.expiryDate;
                readerCard.gender = updatedReaderCard.gender;
                readerCard.birthDate = updatedReaderCard.birthDate;

                context.SaveChanges();
                string readerTypeName = context.ReaderTypes.Find(readerCard.readerTypeId)?.name;
                updatedReaderCard.readerType = new ReaderTypeDTO
                {
                    id = readerCard.readerTypeId,
                    name = readerTypeName,
                };
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

        public (bool, string message) DeleteReaderCard(string readerCardId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var reader = context.ReaderCards.Find(readerCardId);

                if (reader is null)
                {
                    return (false, "Độc giả không tồn tại");
                }
                if (reader.Borrowing_ReturnCard.Count() > 0)
                {
                    return (false, "Độc giả đã từng đặt sách. Không thể xóa!");
                }

                var account = context.Accounts.Find(reader.accountId);

                context.ReaderCards.Remove(reader);
                if (account != null)
                {
                    context.Accounts.Remove(account);
                }
                context.SaveChanges();
                return (true, "Xóa thẻ độc giả thành công");
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
