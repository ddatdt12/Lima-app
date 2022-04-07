using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ImportSerivce
    {
        private ImportSerivce() { }
        private static ImportSerivce _ins;
        public static ImportSerivce Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportSerivce();
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


        //public void GetAllImportReceipt()
        //{
        //    var context = DataProvider.Ins.DB;
        //    var bookList = context.ImportReceipts.Select(imR => new ImportReceipt { }).ToList();

        //    BookService.Ins.ImportBooks(bookList);
        //    List<ImportReceiptDetail> importReceiptDetails = importReceiptDetailList.Select(imR =>
        //   new ImportReceiptDetail
        //   {
        //       importReceiptId = imR.importReceiptId,
        //       bookId = imR.bookId,
        //       quantity = imR.quantity,
        //       unitPrice = imR.unitPrice
        //   }).ToList();

        //    context.ImportReceiptDetails.AddRange(importReceiptDetails);
        //}

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
                var statusCode = e.Data.Keys.Cast<int>().Single(); 
                
                if(statusCode == 400)
                {
                    var statusMessage = e.Data[statusCode].ToString(); 
                    return (false, statusMessage);
                }
                return (false, "Lỗi hệ thống");

            }

        }

    }
}
