using System;

namespace InvoiceHandling.Models
{
	public class UserAccountDetails
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string MobileNumber { get; set; }
		public string ProfileImage { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public bool? IsActive { get; set; }
		public DateTime CreatedAt { get; set; }
		public int UserRole { get; set; }
		public int? ServiceProviderId { get; set; }
		public int ServiceProviderCategoryId { get; set; }
	}
}
