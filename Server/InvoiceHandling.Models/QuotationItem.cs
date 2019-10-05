using System;

namespace InvoiceHandling.Models
{
	public class QuotationItem
	{
		public int Id { get; set; }
		public string ItemDescription { get; set; }
		public decimal? UnitPrice { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
