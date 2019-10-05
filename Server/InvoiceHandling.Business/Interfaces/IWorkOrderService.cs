using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IWorkOrderService
	{
		ServiceResponseList<WorkOrderDetail> GetAllWorkOrders();

		ServiceResponseList<WorkOrderDetail> GetWorkOrdersByServiceProviderId(int serviceProviderId);

		ServiceResponse<WorkOrderDetail> GetWorkOrderById(int workOrderId);

		ServiceResponse<WorkOrderDetail> CreateWorkOrder(WorkOrderDetail workOrderDetail);

		ServiceResponse<WorkOrderDetail> UpdateWorkOrder(WorkOrderDetail workOrderDetail);
	}
}
