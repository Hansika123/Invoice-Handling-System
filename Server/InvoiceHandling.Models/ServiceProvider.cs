using System;

namespace InvoiceHandling.Models
{
	public class ServiceProvider
	{
		public int Id { get; set; }
		public int? CatId { get; set; }
		public string Address { get; set; }
		public string CompanyName { get; set; }
		public string Category { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public int? FeedbackLevel { get; set; }

		public BankDetail BankDetail { get; set; }
		public UserAccountDetails UserAccountDetails { get; set; }
		public JobCategoryDetail JobCategoryDetail { get; set; }
	}
}
