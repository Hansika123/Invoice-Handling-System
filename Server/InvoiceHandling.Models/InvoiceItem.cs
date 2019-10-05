using System;

namespace InvoiceHandling.Models
{
	public class InvoiceItem
	{
		public int Id { get; set; }
		public int InvoiceId { get; set; }
		public int ItemId { get; set; }
		public DateTime CreatedAt { get; set; }

		public Item Item { get; set; }
	}
}
