using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class InvoiceController : ApiController
	{
		private readonly IInvoiceService _invoiceService;

		public InvoiceController(IInvoiceService invoiceService)
		{
			_invoiceService = invoiceService;
		}

		public InvoiceController() : this(new InvoiceService())
		{
		}

		[HttpPost]
		[Route("CreateInvoice")]
		public ServiceResponse<InvoiceDetail> CreateInvoice([FromBody]InvoiceDetail invoiseDetail)
		{
			try
			{
				var invoice = _invoiceService.CreateInvoice(invoiseDetail).Entity;
				return new ServiceResponse<InvoiceDetail>(invoice);
			}
			catch (Exception e)
			{
				return new ServiceResponse<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetAllInvoice")]
		public ServiceResponseList<InvoiceDetail> GetAllInvoice()
		{
			try
			{
				var invoice = _invoiceService.GetAllInvoice().Entities;
				return new ServiceResponseList<InvoiceDetail>(invoice);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetInvoiceById")]
		public ServiceResponse<InvoiceDetail> GetInvoiceById(int invoiceId)
		{
			try
			{
				var invoice = _invoiceService.GetInvoiceById(invoiceId).Entity;
				return new ServiceResponse<InvoiceDetail>(invoice);
			}
			catch (Exception e)
			{
				return new ServiceResponse<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpGet]
		[Route("GetInvoiceByServiceProviderId")]
		public ServiceResponseList<InvoiceDetail> GetInvoiceByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var invoices = _invoiceService.GetInvoiceByServiceProviderId(serviceProviderId).Entities;
				return new ServiceResponseList<InvoiceDetail>(invoices);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		[HttpPost]
		[Route("UpdateInvoiceStatus")]
		public ServiceResponse<bool> UpdateInvoiceStatus(int invoiceId, int invoiceStatus)
		{
			try
			{
				// invoice status: 0: Pending, 1: Paid, 2: Reject
				var invoice = _invoiceService.UpdateInvoiceStatus(invoiceId, invoiceStatus).Entity;
				return new ServiceResponse<bool>(invoice);
			}
			catch (Exception e)
			{
				return new ServiceResponse<bool>(false)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
