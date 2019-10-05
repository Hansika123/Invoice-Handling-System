using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IServiceProviderService
	{
		ServiceResponseList<ServiceProvider> GetAllServiceProviders();

		ServiceResponse<ServiceProvider> GetServiceProviderById(int serviceProviderId);

		ServiceResponseList<JobCategoryDetail> GetAllCategories();

		ServiceResponse<FeedbackDetails> CreateFeedback(FeedbackDetails feedbackDetails);

		ServiceResponse<FeedbackDetails> GetFeedbackByServiceProviderId(int serviceProviderId);

		ServiceResponse<BankAcountDetail> CreateBankAccount(BankAcountDetail bankAcountDetail);

		ServiceResponse<BankAcountDetail> GetBankAccountByServiceProviderId(int serviceProviderId);
	}
}
