using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class WorkOrderController : ApiController
	{
		private readonly IWorkOrderService _workOrderService;

		public WorkOrderController(IWorkOrderService workOrderService)
		{
			_workOrderService = workOrderService;
		}

		public WorkOrderController() : this(new WorkOrderService())
		{
		}


		[HttpGet]
		[Route("GetAllWorkOrders")]
		public ServiceResponseList<WorkOrderDetail> GetAllWorkOrders()
		{
			try
			{
				var workOrderDetails = _workOrderService.GetAllWorkOrders().Entities;
				return new ServiceResponseList<WorkOrderDetail>(workOrderDetails);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<WorkOrderDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpGet]
		[Route("GetWorkOrdersByServiceProviderId")]
		public ServiceResponseList<WorkOrderDetail> GetWorkOrdersByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var workOrderDetails = _workOrderService.GetWorkOrdersByServiceProviderId(serviceProviderId).Entities;
				return new ServiceResponseList<WorkOrderDetail>(workOrderDetails);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<WorkOrderDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpGet]
		[Route("GetWorkOrderById")]
		public ServiceResponse<WorkOrderDetail> GetWorkOrderById(int workOrderId)
		{
			try
			{
				var workOrderDetail = _workOrderService.GetWorkOrderById(workOrderId).Entity;
				return new ServiceResponse<WorkOrderDetail>(workOrderDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponse<WorkOrderDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpPost]
		[Route("CreateWorkOrder")]
		public ServiceResponse<WorkOrderDetail> CreateWorkOrder([FromBody]WorkOrderDetail workOrderDetail)
		{
			try
			{
				var workOrder = _workOrderService.CreateWorkOrder(workOrderDetail).Entity;
				return new ServiceResponse<WorkOrderDetail>(workOrder);
			}
			catch (Exception e)
			{
				return new ServiceResponse<WorkOrderDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		[HttpPost]
		[Route("UpdateWorkOrder")]
		public ServiceResponse<WorkOrderDetail> UpdateWorkOrder([FromBody]WorkOrderDetail workOrderDetail)
		{
			try
			{
				var workOrder = _workOrderService.UpdateWorkOrder(workOrderDetail).Entity;
				return new ServiceResponse<WorkOrderDetail>(workOrder);
			}
			catch (Exception e)
			{
				return new ServiceResponse<WorkOrderDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
