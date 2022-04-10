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

        private string CreateReturnCardId(string maxId)
        {
            if (maxId is null)
            {
                return "RECA0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(4)) + 1}";
            return "RECA" + newIdString.Substring(newIdString.Length - 4, 4);
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

        public List<BorrowingCardDTO> GetBorrowingCardsByReaderId(string readerId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var borrowingCards = context.Borrowing_ReturnCard.Where(b => b.readerCardId == readerId && b.returnedDate == null)
                    .Select(b => new BorrowingCardDTO
                    {
                        id = b.id,
                        bookInfoId = b.bookInfoId,
                        borrowingDate = b.borrowingDate,
                        dueDate = b.dueDate,
                        employeeId = b.borrowing_employeeId,
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
        public List<BorrowingCardDTO> GetDelayBorrowingCardsByReaderId(string readerId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var borrowingCards = context.Borrowing_ReturnCard.Where(b => b.readerCardId == readerId && b.returnedDate == null && b.dueDate < DateTime.Now)
                    .Select(b => new BorrowingCardDTO
                    {
                        id = b.id,
                        bookInfoId = b.bookInfoId,
                        borrowingDate = b.borrowingDate,
                        dueDate = b.dueDate,
                        employeeId = b.borrowing_employeeId,
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
                var readerCard = ReaderService.Ins.GetReaderInfo(cardInfo.readerCardId);
                if (readerCard is null)
                {
                    return (false, "Độc giả không tồn tại");
                }

                int max = ParameterService.Ins.GetRuleValue(Utils.Rules.ALLOWED_BOOK_MAXIMUM);
                if (readerCard.haveDelayBook || readerCard.numberOfBorrowingBooks + bookInfoIdList.Count > max)
                {
                    return (false, "Độc giả không đủ điều kiện để mượn sách");
                }

                //
                List<Borrowing_ReturnCard> borrowingCards = new List<Borrowing_ReturnCard>();
                string maxId = context.Borrowing_ReturnCard.Max(b => b.id);
                string nextId = CreateBorrowingCardId(maxId);

                //Cập nhật sách đã được mượn
                var bookInfoList = context.BookInfoes.Where(b => bookInfoIdList.Contains(b.id)).ToList();

                foreach (var b in bookInfoList)
                {
                    if (!b.status)
                    {
                        return (false, $"{b.Book.BaseBook.name} đã được mượn!");
                    }

                    b.status = false;
                    var borrowingCard = new Borrowing_ReturnCard()
                    {
                        id = nextId,
                        bookInfoId = b.id,
                        borrowingDate = cardInfo.borrowingDate,
                        dueDate = cardInfo.dueDate,
                        borrowing_employeeId = cardInfo.employeeId,
                        readerCardId = cardInfo.readerCardId
                    };

                    borrowingCards.Add(borrowingCard);
                    nextId = CreateBorrowingCardId(nextId);
                }

                context.Borrowing_ReturnCard.AddRange(borrowingCards);
                context.SaveChanges();
                return (true, "Mượn sách thành công");
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

        public (bool, string message) CreateReturnCardList(List<ReturnCardDTO> returnCardList)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                var borrowingCardIdList = returnCardList.Select(r => r.borrowingCardId).ToList();
                var returnCardDictionary = returnCardList.ToDictionary(r => r.id, r => r);

                var borrowingCardQuery = context.Borrowing_ReturnCard.Where(b => borrowingCardIdList.Contains(b.id) && b.returnedDate == null);

                if (borrowingCardQuery.Count() == 0)
                {
                    return (false, "Danh sách sách trả không được rỗng");
                }

                var readerCardId = borrowingCardQuery.FirstOrDefault().readerCardId;
                var bookInfoIdList = borrowingCardQuery.Select(bC => bC.bookInfoId).ToList();

                int totalFine = 0;
                foreach (var returnCard in borrowingCardQuery)
                {
                    if (returnCardDictionary.ContainsKey(returnCard.id))
                    {
                        var data = returnCardDictionary[returnCard.id];
                        totalFine += data.fine;
                        returnCard.returnedDate = data.returnedDate;
                        returnCard.fine = data.fine;
                        returnCard.return_employeeId = data.employeeId;
                    }
                }

                var bookInfoes = context.BookInfoes.Where(b => bookInfoIdList.Contains(b.id)).ToList();
                foreach (var bI in bookInfoes)
                {
                    bI.status = true;
                }
                context.ReaderCards.Find(readerCardId).totalFine += totalFine;

                context.SaveChanges();
                return (true, "Trả sách thành công");
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
