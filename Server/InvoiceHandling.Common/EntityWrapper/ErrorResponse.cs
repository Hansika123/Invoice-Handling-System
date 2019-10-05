namespace InvoiceHandling.Common.EntityWrapper
{
	public class ErrorResponse
	{
		public string ErrorDescription { get; set; }
		public bool HasError { get; set; }
		public dynamic ReturnedObject { get; set; }
	}
}
