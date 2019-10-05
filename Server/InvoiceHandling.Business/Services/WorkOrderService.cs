using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Entity;
using InvoiceHandling.Models;
using InvoiceHandling.Repository.Interfaces;
using InvoiceHandling.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace InvoiceHandling.Business.Services
{
	public class WorkOrderService : IWorkOrderService
	{
		private readonly ICommonRepository<WorkOrder> _workOrderRepository;
		private readonly ICommonRepository<Entity.ServiceProvider> _serviceProviderRepository;
		private readonly ICommonRepository<BankAcount> _bankAccountRepository;
		private readonly ICommonRepository<ActivityLog> _activityLogRepository;


		public WorkOrderService(ICommonRepository<WorkOrder> workOrderRepository,
			ICommonRepository<Entity.ServiceProvider> serviceProviderRepository,
			ICommonRepository<BankAcount> bankAccountRepository, ICommonRepository<ActivityLog> activityLogRepository)
		{
			_workOrderRepository = workOrderRepository;
			_serviceProviderRepository = serviceProviderRepository;
			_bankAccountRepository = bankAccountRepository;
			_activityLogRepository = activityLogRepository;
		}


		public WorkOrderService() : this(new CommonRepository<WorkOrder>(), new CommonRepository<Entity.ServiceProvider>(),
			new CommonRepository<BankAcount>(), new CommonRepository<ActivityLog>())
		{ }



		public ServiceResponse<WorkOrderDetail> CreateWorkOrder(WorkOrderDetail workOrderDetail)
		{
			try
			{
				var workOrderDetailModel = new WorkOrderDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					// Create WorkOrder
					var workOrder = new WorkOrder()
					{
						Description = workOrderDetail.Description,
						DueDate = workOrderDetail.DueDate,
						ServiceProviderID = workOrderDetail.ServiceProviderId,
						Status = workOrderDetail.Status, // 0: not completed, 1: completed, 2: fail
						CreatedAt = DateTime.Now,
						ModifiedAt = DateTime.Now,
						PropertyHolderName = workOrderDetail.PropertyHolderName,
						PHPhoneNumber = workOrderDetail.PHPhoneNumber,
						PHAddress = workOrderDetail.PHAddress,
						TaskId = (workOrderDetail.TaskId == null || workOrderDetail.TaskId == 0) 
                        ? null : workOrderDetail.TaskId
					};
					_workOrderRepository.AddOrUpdate(workOrder);
					_workOrderRepository.Save();

					//Add activity log
					var activityLog = new ActivityLog()
					{
						CreatedAt = DateTime.Now,
						EntityId = workOrder.Id,
						Description = workOrder.Description,
						Name = "New Work Order Created",
					};

					_activityLogRepository.AddOrUpdate(activityLog);
					_activityLogRepository.Save();

					scope.Complete();

					// Genarate QuotationDetailModel					
					workOrderDetailModel.Description = workOrder.Description;
					workOrderDetailModel.DueDate = workOrder.DueDate;
					workOrderDetailModel.Id = workOrder.Id;
					workOrderDetailModel.ServiceProviderId = workOrder.ServiceProviderID;
					workOrderDetailModel.Status = workOrder.Status;
					workOrderDetailModel.PropertyHolderName = workOrder.PropertyHolderName;
					workOrderDetailModel.PHPhoneNumber = workOrder.PHPhoneNumber;
					workOrderDetailModel.PHAddress = workOrder.PHAddress;


                    return new ServiceResponse<WorkOrderDetail>(workOrderDetailModel);
				}
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


		public ServiceResponseList<WorkOrderDetail> GetAllWorkOrders()
		{
			try
			{
				var workOrderList = new List<WorkOrderDetail>();

				var workOrderRequest = _workOrderRepository.GetAll();
				foreach (var order in workOrderRequest)
				{
					var bankDetail = _bankAccountRepository.Get(x => x.ServiceProvider.Id == order.ServiceProviderID).FirstOrDefault();
					var serviceProvider = _serviceProviderRepository.Get(x => x.Id == order.ServiceProviderID).FirstOrDefault();

					var serviceProviderBankDetail = new BankDetail();
					if (bankDetail != null)
					{
						serviceProviderBankDetail.Id = bankDetail.Id;
						serviceProviderBankDetail.AccountName = bankDetail.AccountName;
						serviceProviderBankDetail.AccountNumber = bankDetail.AccountNumber;
						serviceProviderBankDetail.Bank = bankDetail.Bank;
						serviceProviderBankDetail.Branch = bankDetail.Branch;
						serviceProviderBankDetail.Info = bankDetail.Info;
						serviceProviderBankDetail.CreatedAt = bankDetail.CreatedAt;
					}


					var serviceProviderModel = new Models.ServiceProvider()
					{
						Id = serviceProvider.Id,
						Address = serviceProvider.Address,
						Category = serviceProvider.Category,
						CreatedAt = serviceProvider.CreatedAt,
						CatId = serviceProvider.CatId,
						CompanyName = serviceProvider.CompanyName,
						ModifiedAt = serviceProvider.ModifiedAt,
						BankDetail = serviceProviderBankDetail
					};

					var workOrderDetailsModel = new WorkOrderDetail()
					{
						Description = order.Description,
						DueDate = order.DueDate,
						ServiceProviderId = order.ServiceProviderID,
						Status = order.Status,
						CreatedAt = order.CreatedAt,
						Id = order.Id,
						ServiceProvider = serviceProviderModel
					};

					workOrderList.Add(workOrderDetailsModel);
				}

				return new ServiceResponseList<WorkOrderDetail>(workOrderList);
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


		public ServiceResponse<WorkOrderDetail> GetWorkOrderById(int workOrderId)
		{
			try
			{
				var workOrderRequest = _workOrderRepository.Get(x => x.Id == workOrderId).FirstOrDefault();

				if (workOrderRequest != null)
				{
					var bankDetail = _bankAccountRepository.Get(x => x.ServiceProvider.Id == workOrderRequest.ServiceProviderID).FirstOrDefault();
					var serviceProvider = _serviceProviderRepository.Get(x => x.Id == workOrderRequest.ServiceProviderID).FirstOrDefault();

					var serviceProviderBankDetail = new BankDetail();
					if (bankDetail != null)
					{
						serviceProviderBankDetail.Id = bankDetail.Id;
						serviceProviderBankDetail.AccountName = bankDetail.AccountName;
						serviceProviderBankDetail.AccountNumber = bankDetail.AccountNumber;
						serviceProviderBankDetail.Bank = bankDetail.Bank;
						serviceProviderBankDetail.Branch = bankDetail.Branch;
						serviceProviderBankDetail.Info = bankDetail.Info;
						serviceProviderBankDetail.CreatedAt = bankDetail.CreatedAt;
					}

					var serviceProviderModel = new Models.ServiceProvider()
					{
						Id = serviceProvider.Id,
						Address = serviceProvider.Address,
						Category = serviceProvider.Category,
						CreatedAt = serviceProvider.CreatedAt,
						CatId = serviceProvider.CatId,
						CompanyName = serviceProvider.CompanyName,
						ModifiedAt = serviceProvider.ModifiedAt,
						BankDetail = serviceProviderBankDetail
					};

					var workOrderDetailsModel = new WorkOrderDetail()
					{
						Description = workOrderRequest.Description,
						DueDate = workOrderRequest.DueDate,
						ServiceProviderId = workOrderRequest.ServiceProviderID,
						Status = workOrderRequest.Status,
						CreatedAt = workOrderRequest.CreatedAt,
						Id = workOrderRequest.Id,
						ServiceProvider = serviceProviderModel
					};

					return new ServiceResponse<WorkOrderDetail>(workOrderDetailsModel);
				}
				return new ServiceResponse<WorkOrderDetail>(null)
				{
					HasError = false,
					ErrorMessage = "No WorkOrder found"
				};
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


		public ServiceResponseList<WorkOrderDetail> GetWorkOrdersByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var workOrderList = new List<WorkOrderDetail>();

				var workOrderRequest = _workOrderRepository.Get(x => x.ServiceProviderID == serviceProviderId);
				foreach (var order in workOrderRequest)
				{
					var bankDetail = _bankAccountRepository.Get(x => x.ServiceProvider.Id == order.ServiceProviderID).FirstOrDefault();
					var serviceProvider = _serviceProviderRepository.Get(x => x.Id == order.ServiceProviderID).FirstOrDefault();

					var serviceProviderBankDetail = new BankDetail();
					if (bankDetail != null)
					{
						serviceProviderBankDetail.Id = bankDetail.Id;
						serviceProviderBankDetail.AccountName = bankDetail.AccountName;
						serviceProviderBankDetail.AccountNumber = bankDetail.AccountNumber;
						serviceProviderBankDetail.Bank = bankDetail.Bank;
						serviceProviderBankDetail.Branch = bankDetail.Branch;
						serviceProviderBankDetail.Info = bankDetail.Info;
						serviceProviderBankDetail.CreatedAt = bankDetail.CreatedAt;
					}

					var serviceProviderModel = new Models.ServiceProvider()
					{
						Id = serviceProvider.Id,
						Address = serviceProvider.Address,
						Category = serviceProvider.Category,
						CreatedAt = serviceProvider.CreatedAt,
						CatId = serviceProvider.CatId,
						CompanyName = serviceProvider.CompanyName,
						ModifiedAt = serviceProvider.ModifiedAt,
						BankDetail = serviceProviderBankDetail
					};

					var workOrderDetailsModel = new WorkOrderDetail()
					{
						Description = order.Description,
						DueDate = order.DueDate,
						ServiceProviderId = order.ServiceProviderID,
						Status = order.Status,
						CreatedAt = order.CreatedAt,
						Id = order.Id,
						ServiceProvider = serviceProviderModel,
						PropertyHolderName = order.PropertyHolderName,
						PHPhoneNumber = order.PHPhoneNumber,
						PHAddress = order.PHAddress
					};

					workOrderList.Add(workOrderDetailsModel);
				}

				return new ServiceResponseList<WorkOrderDetail>(workOrderList);
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


		public ServiceResponse<WorkOrderDetail> UpdateWorkOrder(WorkOrderDetail workOrderDetail)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var updatedWorkOrder = new WorkOrderDetail();
					var workOrderDetails = _workOrderRepository.Get(x => x.Id == workOrderDetail.Id).FirstOrDefault();

					if (workOrderDetails != null)
					{
						workOrderDetails.Status = workOrderDetail.Status;

						_workOrderRepository.AddOrUpdate(workOrderDetails);
						_workOrderRepository.Save();
					}

					scope.Complete();

					// Genarate task details object
					updatedWorkOrder.Id = workOrderDetails.Id;
					updatedWorkOrder.DueDate = workOrderDetails.DueDate;
					updatedWorkOrder.PHAddress = workOrderDetails.PHAddress;
					updatedWorkOrder.PHPhoneNumber = workOrderDetails.PHPhoneNumber;
					updatedWorkOrder.PropertyHolderName = workOrderDetails.PropertyHolderName;
					updatedWorkOrder.CreatedAt = workOrderDetails.CreatedAt;
					updatedWorkOrder.Description = workOrderDetails.Description;
					updatedWorkOrder.Status = workOrderDetails.Status;
					updatedWorkOrder.ServiceProviderId = workOrderDetails.ServiceProviderID;

					return new ServiceResponse<WorkOrderDetail>(updatedWorkOrder);
				}
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
