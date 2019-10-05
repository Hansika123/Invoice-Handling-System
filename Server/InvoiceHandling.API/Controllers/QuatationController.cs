using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class QuatationController : ApiController
	{
		private readonly IQuatationService _quatationService;

		public QuatationController(IQuatationService quatationService)
		{
			_quatationService = quatationService;
		}

		public QuatationController() : this(new QuatationService())
		{
		}

		[HttpPost]
		[Route("CreateQuatationRequest")]
		public ServiceResponse<QuatationRequestDetail> CreateQuatationRequest([FromBody]QuatationRequestDetail quatationRequestDetail)
		{
			try
			{
				var quatation = _quatationService.CreateQuatationRequest(quatationRequestDetail).Entity;
				return new ServiceResponse<QuatationRequestDetail>(quatation);
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuatationRequestDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetQuoteRequestByServiceProviderId")]
		public ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var quatationRequestDetail = _quatationService.GetQuoteRequestByServiceProviderId(serviceProviderId).Entities;
				return new ServiceResponseList<QuatationRequestDetail>(quatationRequestDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<QuatationRequestDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetAllQuoteRequest")]
		public ServiceResponseList<QuatationRequestDetail> GetAllQuoteRequest()
		{
			try
			{
				var quatationRequestDetail = _quatationService.GetAllQuoteRequest().Entities;
				return new ServiceResponseList<QuatationRequestDetail>(quatationRequestDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<QuatationRequestDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpPost]
		[Route("CreateQuatation")]
		public ServiceResponse<QuotationDetail> CreateQuatation([FromBody]QuotationDetail quotationDetail)
		{
			try
			{
				var quotationDetailModel = _quatationService.CreateQuatation(quotationDetail).Entity;
				return new ServiceResponse<QuotationDetail>(quotationDetailModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuotationDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetAllQuatations")]
		public ServiceResponseList<QuotationDetail> GetAllQuatations()
		{
			try
			{
				var quatationRequestDetail = _quatationService.GetAllQuatations().Entities;
				return new ServiceResponseList<QuotationDetail>(quatationRequestDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<QuotationDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetAllQuatationsByServiceProviderId")]
		public ServiceResponseList<QuotationDetail> GetAllQuatationsByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var quatationRequestDetail = _quatationService.GetAllQuatationsByServiceProviderId(serviceProviderId).Entities;
				return new ServiceResponseList<QuotationDetail>(quatationRequestDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<QuotationDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpPost]
		[Route("UpdateQuatationStatus")]
		public ServiceResponse<QuotationDetail> UpdateQuatationStatus(int quatationId, int statusId)
		{
			try
			{
				var quotationDetailModel = _quatationService.UpdateQuatationStatus(quatationId, statusId).Entity;
				return new ServiceResponse<QuotationDetail>(quotationDetailModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuotationDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpGet]
		[Route("GetQuoteRequestByServiceProviderCategoryId")]
		public ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderCategoryId(int categoryId)
		{
			try
			{
				var quatationRequestDetail = _quatationService.GetQuoteRequestByServiceProviderCategoryId(categoryId).Entities;
				return new ServiceResponseList<QuatationRequestDetail>(quatationRequestDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<QuatationRequestDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
