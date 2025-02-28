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
    
    public partial class QuoteRequest
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuoteRequest()
        {
            this.QuoteRequestDetails = new HashSet<QuoteRequestDetail>();
            this.Quotations = new HashSet<Quotation>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime DueDate { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<System.DateTime> DeletedAt { get; set; }
        public bool isQuotationCreated { get; set; }
        public string PropertyHolderName { get; set; }
        public string PHAddress { get; set; }
        public string PHPhoneNumber { get; set; }
        public int JobCategoryId { get; set; }
        public Nullable<int> TaskId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuoteRequestDetail> QuoteRequestDetails { get; set; }
        public virtual JobCategory JobCategory { get; set; }
        public virtual Task Task { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
