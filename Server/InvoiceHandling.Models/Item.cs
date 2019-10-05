using System;

namespace InvoiceHandling.Models
{
	public class Item
	{
		public int Id { get; set; }
		public string ItemDescription { get; set; }
		public Nullable<decimal> UnitPrice { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public int EstimatedQuentity { get; set; }
		public System.DateTime CreatedAt { get; set; }
		public Nullable<System.DateTime> ModifiedAt { get; set; }
		public Nullable<System.DateTime> DeletedAt { get; set; }
	}
}
