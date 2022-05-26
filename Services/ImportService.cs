using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ImportService
    {
        private ImportService() { }
        private static ImportService _ins;
        public static ImportService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        private string CreateReceiptId(string maxId)
        {
            if (maxId is null)
            {
                return "IPR0001";
            }
            string newIdString = $"0000{int.Parse(maxId.Substring(3)) + 1}";
            return "IPR" + newIdString.Substring(newIdString.Length - 4, 4);
        }


        public List<ImportReceiptDTO> GetAllImportReceipt()
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var importReceiptList = context.ImportReceipts.Select(imR => new ImportReceiptDTO
                {
                    id = imR.id,
                    totalPrice = imR.totalPrice,
                    supplier = imR.supplier,
                    employee = new EmployeeDTO
                    {
                        id = imR.Employee.id,
                        email = imR.Employee.email,
                        name = imR.Employee.name,
                        phoneNumber = imR.Employee.phoneNumber,
                    },
                    createdAt = imR.createdAt,
                    employeeId = imR.employeeId,
                }).ToList();

                return importReceiptList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
        public ImportReceiptDTO GetImportReceiptDetail(string importReceiptId)
        {
            try
            {
                var context = DataProvider.Ins.DB;
                var importReceiptList = context.ImportReceipts.Where(iR => iR.id == importReceiptId).Select(imR => new ImportReceiptDTO
                {
                    id = imR.id,
                    totalPrice = imR.totalPrice,
                    supplier = imR.supplier,
                    employee = new EmployeeDTO
                    {
                        id = imR.Employee.id,
                        email = imR.Employee.email,
                        name = imR.Employee.name,
                        phoneNumber = imR.Employee.phoneNumber,
                    },
                    createdAt = imR.createdAt,
                    employeeId = imR.employeeId,
                    importReceiptDetailList = imR.ImportReceiptDetails.Select(iR => new ImportReceiptDetailDTO
                    {
                        importReceiptId = iR.importReceiptId,
                        quantity = iR.quantity,
                        unitPrice = iR.unitPrice,
                        book = new BookDTO
                        {
                            id = iR.Book.id,
                            quantity = iR.Book.quantity,
                            publisher = iR.Book.publisher,
                            yearOfPublication = iR.Book.yearOfPublication,
                            baseBookId = iR.Book.baseBookId,
                            baseBook = new BaseBookDTO
                            {
                                id = iR.Book.baseBookId,
                                name = iR.Book.BaseBook.name,
                                genre = new GenreDTO
                                {
                                    id = iR.Book.BaseBook.Genre.id,
                                    name = iR.Book.BaseBook.Genre.name,
                                },
                                authors = iR.Book.BaseBook.Authors.Select(x => new AuthorDTO
                                {
                                    id = x.id,
                                    name = x.name,
                                    birthDate = x.birthDate ?? DateTime.Now
                                }).ToList(),
                            },
                        }
                    }).ToList()
                }).FirstOrDefault();

                return importReceiptList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }

        private void CreateReceipt(List<ImportReceiptDetailDTO> importReceiptDetailList)
        {
            var context = DataProvider.Ins.DB;
            var bookList = importReceiptDetailList.Select(imR =>
            {
                imR.book.quantity = imR.quantity;
                return imR.book;
            }).ToList();

            BookService.Ins.ImportBooks(bookList);

            List<ImportReceiptDetail> importReceiptDetails = importReceiptDetailList.Select((imR, i) =>
           {
               return new ImportReceiptDetail
               {
                   importReceiptId = imR.importReceiptId,
                   bookId = bookList[i].id,
                   quantity = imR.quantity,
                   unitPrice = imR.unitPrice
               };
           }).ToList();

            context.ImportReceiptDetails.AddRange(importReceiptDetails);
        }

        public (bool, string) CreateNewBookImportReceipt(ImportReceiptDTO imReceipt)
        {
            try
            {
                var context = DataProvider.Ins.DB;

                string maxId = context.ImportReceipts.Max(imR => imR.id);

                string nextImR = CreateReceiptId(maxId);

                var newImportReceipt = new ImportReceipt
                {
                    id = nextImR,
                    totalPrice = imReceipt.importReceiptDetailList.Sum(imR => imR.unitPrice * imR.quantity),
                    supplier = imReceipt.supplier,
                    createdAt = imReceipt.createdAt,
                    employeeId = imReceipt.employeeId,
                };

                context.ImportReceipts.Add(newImportReceipt);

                imReceipt.importReceiptDetailList.ForEach(imR =>
                {
                    imR.importReceiptId = nextImR;
                });

                CreateReceipt(imReceipt.importReceiptDetailList);


                context.SaveChanges();
                return (true, "Nhập sách thành công!");
            }
            catch (Exception e)
            {
                var statusCode = e.Data.Keys.Cast<int>().SingleOrDefault();

                if (statusCode == 400)
                {
                    var statusMessage = e.Data[statusCode].ToString();
                    return (false, statusMessage);
                }
                return (false, "Lỗi hệ thống");

            }

        }

    }
}
