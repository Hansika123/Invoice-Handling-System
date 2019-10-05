using System;
using System.Collections.Generic;

namespace InvoiceHandling.Models
{
	public class QuotationDetail
	{
		public int Id { get; set; }
		public int? RefferenceId { get; set; }
		public int? ApprovedAdminId { get; set; }
		public decimal? EstimatedSubTotal { get; set; }
		public decimal? EstimatedServiceFee { get; set; }
		public decimal? EstimatedTotal { get; set; }
		public string JobDescription { get; set; }
		public int ServiceProviderId { get; set; }
		public int? Status { get; set; }
		public DateTime CreatedAt { get; set; }

		public virtual ServiceProvider MyProperty { get; set; }
		public List<Item> Items { get; set; }
		public string ServiceProviderName { get; set; }
		public string ServiceProviderAddress { get; set; }
		public int QuatationRequestId { get; set; }
		public QuatationRequestDetail QuatationRequestDetail { get; set; }
	}
}
