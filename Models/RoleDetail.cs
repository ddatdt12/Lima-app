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
    
    public partial class RoleDetail
    {
        public int roleId { get; set; }
        public int permissionId { get; set; }
        public bool isPermitted { get; set; }
    
        public virtual Permission Permission { get; set; }
        public virtual Role Role { get; set; }
    }
}
