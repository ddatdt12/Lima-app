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
    public class FineReceiptService
    {
        private FineReceiptService() { }
        private static FineReceiptService _ins;
        public static FineReceiptService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new FineReceiptService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        private string CreateBorrowingCardId(string maxId)
        {
            if (maxId is null)
            {
                return "BORC0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "BORC" + newIdString.Substring(newIdString.Length - 4, 4);
        }

        private string CreateFineReceiptId(string maxId)
        {
            if (maxId is null)
            {
                return "FIRE0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "FIRE" + newIdString.Substring(newIdString.Length - 4, 4);
        }

        public List<FineReceiptDTO> GetFineReceipts(DateTime? date = null)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                IQueryable<FineReceipt> fineReceiptQuery = context.FineReceipts;
                if (date != null)
                {
                    var compareDate = date?.Date ?? new DateTime();
                    fineReceiptQuery = fineReceiptQuery.Where(f => (DbFunctions.TruncateTime(f.createdAt) == compareDate));
                }

                var fineReceipts = fineReceiptQuery.Select(f => new FineReceiptDTO
                {
                    id = f.id,
                    readerCard = new ReaderCardDTO
                    {
                        name = f.ReaderCard.name,
                        address = f.ReaderCard.address,
                        employeeId = f.ReaderCard.employeeId,
                        readerTypeId = f.ReaderCard.readerTypeId,
                        totalFine = f.ReaderCard.totalFine ?? 0,
                        email = f.ReaderCard.email,
                        createdAt = f.ReaderCard.createdAt,
                        expiryDate = f.ReaderCard.expiryDate,
                        gender = f.ReaderCard.gender,
                        birthDate = f.ReaderCard.birthDate,
                    },
                    createdAt = f.createdAt,
                    amount = f.amount,
                }).ToList();


                return fineReceipts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public (bool, string message) CreateFineReceipt(FineReceiptDTO fineReceipt)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                var readerCard = context.ReaderCards.Find(fineReceipt.readerCardId);

                if (readerCard is null)
                {
                    return (false, "Thẻ độc giả không tồn tại!");
                }

                if (readerCard.totalFine < fineReceipt.amount)
                {
                    return (false, "Tiền thu không được lớn hơn số tiền nợ");
                }

                string maxId = context.FineReceipts.Max(f => f.id);
                var newFineReceipt = new FineReceipt
                {
                    id = CreateFineReceiptId(maxId),
                    amount = fineReceipt.amount,
                    createdAt = fineReceipt.createdAt,
                    employeeId = fineReceipt.employeeId,
                    readerCardId = fineReceipt.readerCardId,
                };

                readerCard.totalFine -= newFineReceipt.amount;

                context.FineReceipts.Add(newFineReceipt);
                context.SaveChanges();
                return (true, "Trả tiền phạt thành công");
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
