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
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.InvoiceItems = new HashSet<InvoiceItem>();
            this.QuotationItems = new HashSet<QuotationItem>();
        }
    
        public int Id { get; set; }
        public string ItemDescription { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public Nullable<int> EstimatedQuentity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuotationItem> QuotationItems { get; set; }
    }
}
