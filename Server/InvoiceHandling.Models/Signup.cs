namespace InvoiceHandling.Models
{
	public class Signup
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public string EmployeePosition { get; set; }

		public string ContactNumber { get; set; }

		public string CompanyName { get; set; }

		public string JobCategory { get; set; }

		public string Address { get; set; }

		public int UserRole { get; set; }

		public int JobCategoryId { get; set; }
	}
}
