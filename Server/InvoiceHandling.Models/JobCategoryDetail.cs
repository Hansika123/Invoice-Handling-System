using System;

namespace InvoiceHandling.Models
{
	public class JobCategoryDetail
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
