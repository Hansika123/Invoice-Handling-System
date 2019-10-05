using System;

namespace InvoiceHandling.Models
{
	public class ActivityLogDetails
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public int? InvoiceId { get; set; }
	}
}
