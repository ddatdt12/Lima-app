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
    
    public partial class ImportReceiptDetail
    {
        public string importReceiptId { get; set; }
        public string bookId { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual ImportReceipt ImportReceipt { get; set; }
    }
}
