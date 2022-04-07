﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LibraryManagementEntities : DbContext
    {
        public LibraryManagementEntities()
            : base("name=LibraryManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<BaseBook> BaseBooks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookInfo> BookInfoes { get; set; }
        public virtual DbSet<BookReturnCard> BookReturnCards { get; set; }
        public virtual DbSet<BorrowedGenreReport> BorrowedGenreReports { get; set; }
        public virtual DbSet<BorrowedGenreReportDetail> BorrowedGenreReportDetails { get; set; }
        public virtual DbSet<BorrowingCard> BorrowingCards { get; set; }
        public virtual DbSet<DelayReturnBookReport> DelayReturnBookReports { get; set; }
        public virtual DbSet<DelayReturnBookReportDetail> DelayReturnBookReportDetails { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<FineReceipt> FineReceipts { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<ImportReceipt> ImportReceipts { get; set; }
        public virtual DbSet<ImportReceiptDetail> ImportReceiptDetails { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<ReaderType> ReaderTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleDetail> RoleDetails { get; set; }
        public virtual DbSet<ReaderCard> ReaderCards { get; set; }
    }
}
