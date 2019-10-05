using System;

namespace InvoiceHandling.Models
{
	public class WorkOrderDetail
	{
		public int Id { get; set; }
		public int? Status { get; set; }
		public DateTime? DueDate { get; set; }
		public string Description { get; set; }
		public int? ServiceProviderId { get; set; }
		public DateTime CreatedAt { get; set; }
		public string PropertyHolderName { get; set; }
		public string PHAddress { get; set; }
		public string PHPhoneNumber { get; set; }
		public int? TaskId { get; set; }

		public ServiceProvider ServiceProvider { get; set; }
	}
}
