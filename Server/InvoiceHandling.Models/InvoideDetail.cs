using System;
using System.Collections.Generic;

namespace InvoiceHandling.Models
{
	public class InvoiceDetail
	{
		public int Id { get; set; }
		public int InvoiceNumber { get; set; }
		public int ItemCode { get; set; }
		public int? Quentity { get; set; }
		public decimal? SubTotal { get; set; }
		public int Discount { get; set; }
		public decimal? Total { get; set; }
		public DateTime Date { get; set; }
		public string Description { get; set; }
		public decimal? ServiceFee { get; set; }
		public int? InvoiceStatus { get; set; }
		public int ServiceProvideId { get; set; }
		public string InvoiceSubject { get; set; }
		public List<Item> Items { get; set; }
		public string ServiceProviderName { get; set; }
		public string ServiceProviderAddress { get; set; }
	}
}
