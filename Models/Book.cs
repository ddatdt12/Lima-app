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
    
    public partial class Book
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            this.ImportReceiptDetails = new HashSet<ImportReceiptDetail>();
            this.BookInfoes = new HashSet<BookInfo>();
        }
    
        public string id { get; set; }
        public string name { get; set; }
        public int genreId { get; set; }
        public int authorId { get; set; }
        public int yearOfPublication { get; set; }
        public string publisher { get; set; }
        public int quantity { get; set; }
        public bool isDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportReceiptDetail> ImportReceiptDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookInfo> BookInfoes { get; set; }
        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
