using LibraryManagement.DTOs;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ReceiptService
    {
        private ReceiptService() { }
        private static ReceiptService _ins;
        public static ReceiptService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ReceiptService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

    }
}
