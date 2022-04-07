using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class BorrowingReturnService
    {
        private BorrowingReturnService() { }
        private static BorrowingReturnService _ins;
        public static BorrowingReturnService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BorrowingReturnService();
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
        private int GetBookInfoIndex(string bookInfoId)
        {
            if (bookInfoId is null)
            {
                return 0;
            }
            int index = int.Parse(bookInfoId.Substring(bookInfoId.Length - 4, 4));

            return index;
        }
        public ReaderCardDTO GetReaderInfo(string readerId)
        {
            try
            {

                var context = DataProvider.Ins.DB;
                ReaderCard reader = context.ReaderCards.Find(readerId);
                if (reader is null)
                {
                    return null;
                }
                var readerCard = new ReaderCardDTO
                {
                    id = reader.id,
                    name = reader.name,
                    birthDate = (DateTime)reader.birthDate,
                    expiryDate = reader.expiryDate,
                };
                readerCard.haveDelayBook = reader.BorrowingCards.Count(b => b.BookReturnCards.Count() == 0 && b.dueDate < DateTime.Now) > 0;
                readerCard.numberOfBorrowingBooks = reader.BorrowingCards.Count(b => b.BookReturnCards.Count() == 0);

                return readerCard;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<BorrowingCardDTO> GetBorrowingCardsByReaderId(string readerId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var borrowingCards = context.BorrowingCards.Where(b => b.readerCardId == readerId && b.BookReturnCards.Count() == 0)
                    .Select(b => new BorrowingCardDTO
                {
                    id = b.id,
                    bookInfoId = b.bookInfoId,
                    borrowingDate = b.borrowingDate,
                    dueDate = b.dueDate,
                    employeeId = b.employeeId,
                    readerCardId = b.readerCardId,
                    bookInfo = new BookInfoDTO
                    {
                        id = b.bookInfoId,
                        Book = new BookDTO
                        {
                            id = b.BookInfo.Book.id,
                            publisher = b.BookInfo.Book.publisher,
                            yearOfPublication = b.BookInfo.Book.yearOfPublication,
                            baseBook = new BaseBookDTO
                            {
                                id = b.BookInfo.Book.BaseBook.id,
                                name = b.BookInfo.Book.BaseBook.name,
                            }
                        }
                    }
                }).ToList();

                return borrowingCards;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public (bool, string message) CreateBorrowingCard(BorrowingCardDTO cardInfo, List<string> bookInfoIdList)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var readerCard = GetReaderInfo(cardInfo.readerCardId);
                if (readerCard is null)
                {
                    return (false, "Độc giả không tồn tại");
                }

                int max = ParameterService.Ins.GetRuleValue(Utils.Rules.ALLOWED_BOOK_MAXIMUM);
                if (readerCard.haveDelayBook || readerCard.numberOfBorrowingBooks + bookInfoIdList.Count > max)
                {
                    return (false, "Độc giả không đủ điều kiện để mượn sách");
                }

                var bookInfoList = context.BookInfoes.Where(b => bookInfoIdList.Contains(b.id)).ToList();

                List<BorrowingCard> borrowingCards = new List<BorrowingCard>();
                string maxId = context.BorrowingCards.Max(b => b.id);
                string nextId = CreateBorrowingCardId(maxId);

                foreach (var b in bookInfoList)
                {
                    if (!b.status)
                    {
                        return (false, $"{b.Book.BaseBook.name} đã được mượn!");
                    }

                    b.status = false;
                    var borrowingCard = new BorrowingCard()
                    {
                        id = nextId,
                        bookInfoId = b.id,
                        borrowingDate = cardInfo.borrowingDate,
                        dueDate = cardInfo.dueDate,
                        employeeId = cardInfo.employeeId,
                        readerCardId = cardInfo.readerCardId
                    };

                    context.BorrowingCards.Add(borrowingCard);
                    nextId = CreateBorrowingCardId(nextId);
                }

                context.SaveChanges();
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

        //public (bool, string message) CreateReturnCardList(List<ReturnCardDTO> returnCardList)
        //{
        //    try
        //    {
        //        var context = DataProvider.Ins.DB;

        //        List<BorrowingCard> borrowingCards = new List<BorrowingCard>();
        //        string maxId = context.BorrowingCards.Max(b => b.id);
        //        string nextId = CreateBorrowingCardId(maxId);

        //        var borrowingCardIdList = context.BorrowingCards.Where(b => b.id == nextId).ToList();
        //        foreach (var b in returnCardList)
        //        {
        //            if (!b.b)
        //            {
        //                return (false, $"{b.Book.BaseBook.name} đã được mượn!");
        //            }

        //            b.status = false;
        //            var borrowingCard = new BorrowingCard()
        //            {
        //                id = nextId,
        //                bookInfoId = b.id,
        //                borrowingDate = cardInfo.borrowingDate,
        //                dueDate = cardInfo.dueDate,
        //                employeeId = cardInfo.employeeId,
        //                readerCardId = cardInfo.readerCardId
        //            };

        //            context.BorrowingCards.Add(borrowingCard);
        //            nextId = CreateBorrowingCardId(nextId);
        //        }

        //        context.SaveChanges();
        //        return (true, "Thêm tác giả thành công");
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        return (false, e.Message);

        //    }
        //    catch (DbUpdateException e)
        //    {
        //        return (false, e?.InnerException.Message);
        //    }
        //}
    }
}
