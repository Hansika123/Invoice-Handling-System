using System.Collections.Generic;

namespace InvoiceHandling.Common.EntityWrapper
{
	public class ServiceResponseList<T>
	{
		public readonly IList<T> Entities;

		public ServiceResponseList(IList<T> t)
		{
			Entities = t;
		}

		public string ErrorMessage { get; set; }
		public bool HasError { get; set; }
	}
}
