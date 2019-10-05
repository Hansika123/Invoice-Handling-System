using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IInvoiceService
	{
		ServiceResponse<InvoiceDetail> CreateInvoice(InvoiceDetail invoideDetail);

		ServiceResponseList<InvoiceDetail> GetAllInvoice();

		ServiceResponse<InvoiceDetail> GetInvoiceById(int invoiceId);

		ServiceResponseList<InvoiceDetail> GetInvoiceByServiceProviderId(int serviceProviderId);

		ServiceResponse<bool> UpdateInvoiceStatus(int invoiceId, int invoiceStatus);
	}
}
