using System;

namespace InvoiceHandling.Models
{
	public class FeedbackDetails
	{
		public int Id { get; set; }
		public string PropertyHolderName { get; set; }
		public int? CategoryId { get; set; }
		public int ServiceProviderId { get; set; }
		public int? RatingLevel { get; set; }
		public string JobType { get; set; }
		public DateTime CreatedAt { get; set; }

		public virtual ServiceProvider ServiceProvider { get; set; }
	}
}
