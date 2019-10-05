using System;

namespace InvoiceHandling.Models
{
	public class TaskDetail
	{
		public int Id { get; set; }
		public DateTime? DueDate { get; set; }
		public string PropertyHolderName { get; set; }
		public string PHAddress { get; set; }
		public string PHPhoneNumber { get; set; }
		public string AssignStatus { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CallerSystemUserId { get; set; }
		public int JobCategoryId { get; set; }
		public decimal? MaximumBudget { get; set; }
		public int? ServiceProviderId { get; set; }
		public string Purpose { get; set; }
		public JobCategoryDetail JobCategoryDetail { get; set; }
		public ServiceProvider ServiceProvider { get; set; }
		public WorkOrderDetail WorkOrderDetail { get; set; }
	}
}
