//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceHandling.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankAcount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string Info { get; set; }
        public int ServiceProviderId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
    
        public virtual ServiceProvider ServiceProvider { get; set; }
    }
}
