using System;

namespace InvoiceHandling.Models
{
	public class QuatationRequestDetail
	{
		public int QuatationRequestId { get; set; }
		public int QuatationRequestDetailId { get; set; }
		public int? RefId { get; set; }

		public string RequestName { get; set; }
		public string RequestDescription { get; set; }
		public DateTime DueDate { get; set; }

		public int ServiceProviderId { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }
		public bool isQuotationCreated { get; set; }
		public string PropertyHolderName { get; set; }
		public string PHAddress { get; set; }
		public string PHPhoneNumber { get; set; }
		public int JobCategoryId { get; set; }
		public JobCategoryDetail JobCategoryDetail { get; set; }
		public int? TaskId { get; set; }
	}
}
