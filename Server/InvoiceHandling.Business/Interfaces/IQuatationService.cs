using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IQuatationService
	{
		ServiceResponse<QuatationRequestDetail> CreateQuatationRequest(QuatationRequestDetail quatationRequestDetail);

		ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderId(int serviceProviderId);

		ServiceResponseList<QuatationRequestDetail> GetAllQuoteRequest();

		ServiceResponse<QuotationDetail> CreateQuatation(QuotationDetail quotationDetail);

		ServiceResponseList<QuotationDetail> GetAllQuatations();

		ServiceResponseList<QuotationDetail> GetAllQuatationsByServiceProviderId(int serviceProviderId);

		ServiceResponse<QuotationDetail> UpdateQuatationStatus(int quatationId, int statusId);

		ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderCategoryId(int categoryId);
	}
}
