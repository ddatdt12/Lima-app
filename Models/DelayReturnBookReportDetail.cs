//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DelayReturnBookReportDetail
    {
        public int delayReturnBookReportId { get; set; }
        public string borrowingCardId { get; set; }
        public int borrowingDate { get; set; }
        public int numberOfDelayReturn { get; set; }
    
        public virtual BorrowingCard BorrowingCard { get; set; }
        public virtual DelayReturnBookReport DelayReturnBookReport { get; set; }
    }
}