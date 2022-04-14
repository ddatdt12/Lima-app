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

    public partial class Borrowing_ReturnCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Borrowing_ReturnCard()
        {
            this.DelayReturnBookReportDetails = new HashSet<DelayReturnBookReportDetail>();
        }

        public string id { get; set; }
        public string readerCardId { get; set; }
        public System.DateTime borrowingDate { get; set; }
        public System.DateTime dueDate { get; set; }
        public string borrowing_employeeId { get; set; }
        public string bookInfoId { get; set; }
        public Nullable<System.DateTime> returnedDate { get; set; }
        public string return_employeeId { get; set; }
        public int fine { get; set; }

        public virtual BookInfo BookInfo { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DelayReturnBookReportDetail> DelayReturnBookReportDetails { get; set; }
        public virtual ReaderCard ReaderCard { get; set; }

    }
}