namespace InvoiceHandling.Common.EntityWrapper
{
	public class ServiceResponse<T>
	{
		public readonly T Entity;

		public ServiceResponse(T t)
		{
			Entity = t;
		}

		public string ErrorMessage { get; set; }
		public bool HasError { get; set; }
	}
}
