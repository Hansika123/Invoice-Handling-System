using System;

namespace InvoiceHandling.Models
{
	public class BankDetail
	{
		public int Id { get; set; }
		public string AccountNumber { get; set; }
		public string AccountName { get; set; }
		public string Bank { get; set; }
		public string Branch { get; set; }
		public string Info { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
