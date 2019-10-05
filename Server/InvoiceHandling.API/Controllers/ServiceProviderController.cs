using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class ServiceProviderController : ApiController
	{
		private readonly IServiceProviderService _serviceProviderService;

		public ServiceProviderController(IServiceProviderService serviceProviderService)
		{
			_serviceProviderService = serviceProviderService;
		}

		public ServiceProviderController() : this(new ServiceProviderService())
		{
		}


		[HttpGet]
		[Route("GetAllServiceProviders")]
		public ServiceResponseList<ServiceProvider> GetAllServiceProviders()
		{
			try
			{
				var serviceProvidersDetail = _serviceProviderService.GetAllServiceProviders().Entities;
				return new ServiceResponseList<ServiceProvider>(serviceProvidersDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<ServiceProvider>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpGet]
		[Route("GetServiceProviderById")]
		public ServiceResponse<ServiceProvider> GetServiceProviderById(int serviceProviderId)
		{
			try
			{
				var serviceProviderDetail = _serviceProviderService.GetServiceProviderById(serviceProviderId).Entity;
				return new ServiceResponse<ServiceProvider>(serviceProviderDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponse<ServiceProvider>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetAllCategories")]
		public ServiceResponseList<JobCategoryDetail> GetAllCategories()
		{
			try
			{
				var jobCategoryDetails = _serviceProviderService.GetAllCategories().Entities;
				return new ServiceResponseList<JobCategoryDetail>(jobCategoryDetails);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<JobCategoryDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpPost]
		[Route("CreateFeedback")]
		public ServiceResponse<FeedbackDetails> CreateFeedback(FeedbackDetails feedbackDetails)
		{
			try
			{
				var feedback = _serviceProviderService.CreateFeedback(feedbackDetails).Entity;
				return new ServiceResponse<FeedbackDetails>(feedback);
			}
			catch (Exception e)
			{
				return new ServiceResponse<FeedbackDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetFeedbackByServiceProviderId")]
		public ServiceResponse<FeedbackDetails> GetFeedbackByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var jobCategoryDetails = _serviceProviderService.GetFeedbackByServiceProviderId(serviceProviderId).Entity;
				return new ServiceResponse<FeedbackDetails>(jobCategoryDetails);
			}
			catch (Exception e)
			{
				return new ServiceResponse<FeedbackDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpPost]
		[Route("CreateBankAccount")]
		public ServiceResponse<BankAcountDetail> CreateBankAccount(BankAcountDetail bankAcountDetail)
		{
			try
			{
				var accountDetail = _serviceProviderService.CreateBankAccount(bankAcountDetail).Entity;
				return new ServiceResponse<BankAcountDetail>(accountDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponse<BankAcountDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetBankAccountByServiceProviderId")]
		public ServiceResponse<BankAcountDetail> GetBankAccountByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var bankAccountDetails = _serviceProviderService.GetBankAccountByServiceProviderId(serviceProviderId).Entity;
				return new ServiceResponse<BankAcountDetail>(bankAccountDetails);
			}
			catch (Exception e)
			{
				return new ServiceResponse<BankAcountDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
