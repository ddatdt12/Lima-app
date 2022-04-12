using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;


namespace LibraryManagement.Services
{
    public class StatisticService
    {
        DateTime MIN_DATE_OF_APP = new DateTime(2022, 3, 1);
        private StatisticService() { }
        private static StatisticService _ins;
        public static StatisticService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new StatisticService();
                }
                return _ins;
            }
            private set => _ins = value;
        }


        public BorrowedGenreReportDTO CreateBookBorrowingStatisticsReportByGenre(int month, int year)
        {
            var context = DataProvider.Ins.DB;
            var reportDate = new DateTime(year, month, 1);
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var bookBorrowingStatisticReport = new BorrowedGenreReport
                    {
                        reportDate = reportDate,
                    };

                    context.BorrowedGenreReports.Add(bookBorrowingStatisticReport);
                    context.SaveChanges();
                    var borrowedGenreReportId = bookBorrowingStatisticReport.id;
                    var lastReportDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)); ;
                    var cards = context.Borrowing_ReturnCard.Where(bC => bC.borrowingDate >= reportDate && bC.borrowingDate <= lastReportDate);

                    var borrowedGenreReportDetails = cards.GroupBy(c => c.BookInfo.Book.BaseBook.genreId, (genreId, groupCards) => new
                    {
                        genreId = genreId,
                        NumberOfBorrowings = groupCards.Count(),
                        borrowedGenreReportId = borrowedGenreReportId
                    }).ToList().Select(bD => new BorrowedGenreReportDetail
                    {
                        genreId = bD.genreId,
                        NumberOfBorrowings = bD.NumberOfBorrowings,
                        borrowedGenreReportId = bD.borrowedGenreReportId
                    });

                    bookBorrowingStatisticReport.totalNumberOfBorrowings = borrowedGenreReportDetails.Sum(bD => bD.NumberOfBorrowings);
                    context.BorrowedGenreReportDetails.AddRange(borrowedGenreReportDetails);

                    context.SaveChanges();
                    transaction.Commit();

                    var report = context.BorrowedGenreReports.Find(borrowedGenreReportId);

                    if (report is null) return null;

                    foreach (var detail in report.BorrowedGenreReportDetails)
                    {
                        context.Entry(detail).Reference(s => s.Genre).Load(); // loads Courses collection
                    }

                    var reportDTO = report is null ? null : new BorrowedGenreReportDTO
                    {
                        id = report.id,
                        reportDate = reportDate,
                        totalNumberOfBorrowings = report.totalNumberOfBorrowings,
                        borrowedGenreReportDetails = report.BorrowedGenreReportDetails.Select(r => new BorrowedGenreReportDetailDTO
                        {
                            Genre = new GenreDTO
                            {
                                id = r.Genre.id,
                                name = r.Genre.name
                            },
                            NumberOfBorrowings = r.NumberOfBorrowings,
                            rate = 1.0 * r.NumberOfBorrowings / report.totalNumberOfBorrowings
                        }).ToList()
                    };

                    return reportDTO;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Dùng khi người dùng chọn tháng >= tháng hiện tại
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private BorrowedGenreReportDTO StatisticBookBorrowingReportByGenre(int month, int year)
        {
            var context = DataProvider.Ins.DB;
            var reportDate = new DateTime(year, month, 1);

            try
            {
                var lastReportDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                var cards = context.Borrowing_ReturnCard.Where(bC => bC.borrowingDate >= reportDate && bC.borrowingDate <= lastReportDate);

                var borrowedGenreReportDetails = cards.GroupBy(c => c.BookInfo.Book.BaseBook.genreId, (genreId, groupCards) => new BorrowedGenreReportDetailDTO
                {
                    genreId = genreId,
                    NumberOfBorrowings = groupCards.Count(),
                    Genre = groupCards.Select(c => new GenreDTO
                    {
                        id = c.BookInfo.Book.BaseBook.genreId,
                        name = c.BookInfo.Book.BaseBook.Genre.name,
                    }).FirstOrDefault(),
                }).ToList();
                var totalNumberOfBorrowings = borrowedGenreReportDetails.Sum(bD => bD.NumberOfBorrowings);
                var reportDTO = new BorrowedGenreReportDTO
                {
                    reportDate = reportDate,
                    borrowedGenreReportDetails = borrowedGenreReportDetails.Select(b => new BorrowedGenreReportDetailDTO
                    {
                        genreId = b.genreId,
                        NumberOfBorrowings = b.NumberOfBorrowings,
                        Genre = b.Genre,
                        rate = 1.0 * b.NumberOfBorrowings / totalNumberOfBorrowings
                    }).ToList(),
                    totalNumberOfBorrowings = totalNumberOfBorrowings
                };
                return reportDTO;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public (BorrowedGenreReportDTO, string error) GetBookBorrowingStatisticsReportByGenre(int month, int year)
        {
            var context = DataProvider.Ins.DB;
            try
            {
                var reportDate = new DateTime(year, month, 1);
                var lastReportDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                var minDate = context.BorrowedGenreReports.Min(b => (DateTime?)b.reportDate) ?? MIN_DATE_OF_APP;
                var now = DateTime.Now;

                //Check report 
                if (reportDate > DateTime.Now)
                {
                    return (null, "Dữ liệu trống!");
                }
                if (now <= lastReportDate)
                {
                    return (StatisticBookBorrowingReportByGenre(month, year), null);
                }

                var report = context.BorrowedGenreReports.Where(b => b.reportDate == reportDate).FirstOrDefault();

                if (reportDate < minDate)
                {
                    return (null, $"Hệ thống bắt đầu từ {minDate.ToString("MM/yyyy", CultureInfo.InvariantCulture)}. Không có dữ liệu trước thời điểm đó");
                }

                if (report is null)
                {
                    var data = CreateBookBorrowingStatisticsReportByGenre(month, year);
                    return (data, null);
                }

                var reportDTO = new BorrowedGenreReportDTO
                {
                    id = report.id,
                    reportDate = reportDate,
                    totalNumberOfBorrowings = report.totalNumberOfBorrowings,
                    borrowedGenreReportDetails = report.BorrowedGenreReportDetails.Select(r => new BorrowedGenreReportDetailDTO
                    {
                        Genre = new GenreDTO
                        {
                            id = r.Genre.id,
                            name = r.Genre.name
                        },
                        NumberOfBorrowings = r.NumberOfBorrowings,
                        rate = 1.0 * r.NumberOfBorrowings / report.totalNumberOfBorrowings
                    }).ToList()
                };
                return (reportDTO, null);
            }
            catch (Exception e)
            {
                return (null, "Lỗi hệ thống!");
            }
        }

        public DelayReturnBookReportDTO CreateDelayBookStatsReport(DateTime date)
        {
            var context = DataProvider.Ins.DB;
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {

                    var now = DateTime.Now.Date;


                    var report = new DelayReturnBookReport
                    {
                        reportDate = date,
                    };
                    context.DelayReturnBookReports.Add(report);

                    context.SaveChanges();

                    var delayReports = context.Borrowing_ReturnCard.Where(c => date > c.borrowingDate && c.dueDate < date &&
                    (c.returnedDate == null || date < c.returnedDate)).ToList().Select(c => new DelayReturnBookReportDetail
                    {
                        borrowingReturnCardId = c.id,
                        numberOfDelayReturn = (c.returnedDate == null ? now.Subtract(c.dueDate).Days : c.returnedDate?.Subtract(c.dueDate).Days) ?? 0,
                        delayReturnBookReportId = report.id,
                    }).ToList();

                    context.DelayReturnBookReportDetails.AddRange(delayReports);

                    context.SaveChanges();
                    transaction.Commit();
                    var createdReport = context.DelayReturnBookReports.Find(report.id);

                    if (createdReport is null) return null;

                    var detailList = createdReport.DelayReturnBookReportDetails;
                    var reportDTO = new DelayReturnBookReportDTO
                    {
                        id = createdReport.id,
                        reportDate = createdReport.reportDate,
                        delayReturnBookReportDetails = detailList.Select(d => new DelayReturnBookReportDetailDTO
                        {
                            BorrowingReturnCard = new BorrowingCardDTO
                            {
                                id = d.Borrowing_ReturnCard.id,
                                bookInfoId = d.Borrowing_ReturnCard.bookInfoId,
                                borrowingDate = d.Borrowing_ReturnCard.borrowingDate,
                                dueDate = d.Borrowing_ReturnCard.dueDate,
                                bookInfo = new BookInfoDTO
                                {
                                    id = d.Borrowing_ReturnCard.bookInfoId,
                                    Book = new BookDTO
                                    {
                                        id = d.Borrowing_ReturnCard.BookInfo.Book.id,
                                        publisher = d.Borrowing_ReturnCard.BookInfo.Book.publisher,
                                        yearOfPublication = d.Borrowing_ReturnCard.BookInfo.Book.yearOfPublication,
                                        baseBook = new BaseBookDTO
                                        {
                                            id = d.Borrowing_ReturnCard.BookInfo.Book.BaseBook.id,
                                            name = d.Borrowing_ReturnCard.BookInfo.Book.BaseBook.name,
                                        }
                                    }
                                },
                                numberOfDelayReturnDays = d.numberOfDelayReturn,
                            },
                        numberOfDelayReturn = d.numberOfDelayReturn

                        }).ToList(),
                    };
                    return reportDTO;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        }
        public (DelayReturnBookReportDTO, string error) GetDelayBookStatsReport(DateTime date)
        {
            var context = DataProvider.Ins.DB;
            try
            {
                date = date.Date;

                var dateNow = DateTime.Now.Date;
                var report = context.DelayReturnBookReports.Where(r => r.reportDate == date).FirstOrDefault();

                if (report is null)
                {
                    return (CreateDelayBookStatsReport(date), null);
                }

                if (date > dateNow)
                {
                    return (null, "Chưa có dữ liệu trong tương lai!");
                }

                var reportDTO = new DelayReturnBookReportDTO
                {
                    id = report.id,
                    reportDate = report.reportDate,
                    delayReturnBookReportDetails = report.DelayReturnBookReportDetails.Select(d => new DelayReturnBookReportDetailDTO
                    {
                        BorrowingReturnCard = new BorrowingCardDTO
                        {
                            id = d.Borrowing_ReturnCard.id,
                            bookInfoId = d.Borrowing_ReturnCard.bookInfoId,
                            borrowingDate = d.Borrowing_ReturnCard.borrowingDate,
                            dueDate = d.Borrowing_ReturnCard.dueDate,
                            bookInfo = new BookInfoDTO
                            {
                                id = d.Borrowing_ReturnCard.bookInfoId,
                                Book = new BookDTO
                                {
                                    id = d.Borrowing_ReturnCard.BookInfo.Book.id,
                                    publisher = d.Borrowing_ReturnCard.BookInfo.Book.publisher,
                                    yearOfPublication = d.Borrowing_ReturnCard.BookInfo.Book.yearOfPublication,
                                    baseBook = new BaseBookDTO
                                    {
                                        id = d.Borrowing_ReturnCard.BookInfo.Book.BaseBook.id,
                                        name = d.Borrowing_ReturnCard.BookInfo.Book.BaseBook.name,
                                    }
                                }
                            },
                            numberOfDelayReturnDays = d.numberOfDelayReturn,
                        },
                        numberOfDelayReturn = d.numberOfDelayReturn
                    }).ToList(),
                };
                return (reportDTO, null);
            }
            catch (Exception e)
            {
                return (null, "Lỗi hệ thống!");
            }
        }

    }
}
