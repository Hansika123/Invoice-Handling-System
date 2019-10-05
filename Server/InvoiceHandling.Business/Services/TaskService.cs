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
	public class TaskService : ITaskService
	{

		private readonly ICommonRepository<Task> _taskRepository;
		private readonly ICommonRepository<ActivityLog> _activityLogRepository;
		private readonly ICommonRepository<Entity.ServiceProvider> _serviceProviderRepository;
		private readonly ICommonRepository<WorkOrder> _workOrderRepository;

		public TaskService(ICommonRepository<Task> taskRepository, ICommonRepository<ActivityLog> activityLogRepository,
			ICommonRepository<Entity.ServiceProvider> serviceProviderRepository, ICommonRepository<WorkOrder> workOrderRepository)
		{
			_taskRepository = taskRepository;
			_activityLogRepository = activityLogRepository;
			_serviceProviderRepository = serviceProviderRepository;
			_workOrderRepository = workOrderRepository;
		}


		public TaskService() : this(new CommonRepository<Task>(), new CommonRepository<ActivityLog>(),
			new CommonRepository<Entity.ServiceProvider>(), new CommonRepository<WorkOrder>())
		{ }


		public ServiceResponseList<TaskDetail> GetAllTasks()
		{
			var taskList = new List<TaskDetail>();
			try
			{
				var taskDetails = _taskRepository.GetAll();
				foreach (var task in taskDetails)
				{
					var serviceProviderDetail = new Models.ServiceProvider();
					if (task.ServiceProvider != null)
					{

						serviceProviderDetail.Id = task.ServiceProvider.Id;
						serviceProviderDetail.Address = task.ServiceProvider.Address;
						serviceProviderDetail.CatId = task.ServiceProvider.CatId;
						serviceProviderDetail.CompanyName = task.ServiceProvider.CompanyName;
						serviceProviderDetail.CreatedAt = task.ServiceProvider.CreatedAt;
						serviceProviderDetail.ModifiedAt = task.ServiceProvider.ModifiedAt;

						var accountProfile = new UserAccountDetails()
						{
							Address = task.ServiceProvider.AccountProfile.AddressLine1,
							Email = task.ServiceProvider.AccountProfile.SystemUser.Email,
							FirstName = task.ServiceProvider.AccountProfile.FirstName,
							ID = task.ServiceProvider.AccountProfile.Id,
							LastName = task.ServiceProvider.AccountProfile.LastName,
							MobileNumber = task.ServiceProvider.AccountProfile.Mobile,
							IsActive = task.ServiceProvider.AccountProfile.SystemUser.IsActive,
							CreatedAt = task.ServiceProvider.AccountProfile.CreatedAt,
						};

						serviceProviderDetail.UserAccountDetails = accountProfile;
					}

					var workOrder = new WorkOrderDetail();
					var order = _workOrderRepository.Get(a => a.TaskId == task.Id).FirstOrDefault();
					if (order != null)
					{
						workOrder.Id = order.Id;
						workOrder.Description = order.Description;
						workOrder.CreatedAt = order.CreatedAt;
						workOrder.DueDate = order.DueDate;
						workOrder.PHAddress = order.PHAddress;
						workOrder.PHPhoneNumber = order.PHPhoneNumber;
						workOrder.PropertyHolderName = order.PropertyHolderName;
						workOrder.Status = order.Status;
					}

					var jobCategory = new JobCategoryDetail();
					if (task.JobCategory != null)
					{
						jobCategory.Id = task.JobCategory.Id;
						jobCategory.Name = task.JobCategory.Name;
					}

					var taskModel = new TaskDetail()
					{
						Id = task.Id,
						PHAddress = task.PHAddress,
						AssignStatus = task.AssignStatus,
						DueDate = task.DueDate,
						PHPhoneNumber = task.PHPhoneNumber,
						PropertyHolderName = task.PropertyHolderName,
						CreatedAt = task.CreatedAt,
						JobCategoryId = task.JobCategoryId,
						ServiceProviderId = task.ServiceProviderId ?? 0,
						ServiceProvider = serviceProviderDetail,
						WorkOrderDetail = workOrder,
						JobCategoryDetail = jobCategory,
						Purpose = task.Purpose,
						MaximumBudget = task.MaximumBudget ?? null
					};

					taskList.Add(taskModel);
				}

				return new ServiceResponseList<TaskDetail>(taskList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<TaskDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		public ServiceResponseList<TaskDetail> GetTasksByUserId(int systemUserId)
		{
			var taskList = new List<TaskDetail>();
			try
			{
				var taskDetails = _taskRepository.Get(x => x.CallerSystemUserId == systemUserId);
				foreach (var task in taskDetails)
				{
					var taskModel = new TaskDetail()
					{
						Id = task.Id,
						PHAddress = task.PHAddress,
						AssignStatus = task.AssignStatus,
						DueDate = task.DueDate,
						PHPhoneNumber = task.PHPhoneNumber,
						PropertyHolderName = task.PropertyHolderName,
						CallerSystemUserId = task.CallerSystemUserId,
						CreatedAt = task.CreatedAt
					};

					taskList.Add(taskModel);
				}

				return new ServiceResponseList<TaskDetail>(taskList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<TaskDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		public ServiceResponse<TaskDetail> GetTaskById(int taskId)
		{
			try
			{
				var taskDetails = _taskRepository.Get(x => x.Id == taskId).FirstOrDefault();

				var taskModel = new TaskDetail()
				{
					Id = taskDetails.Id,
					PHAddress = taskDetails.PHAddress,
					AssignStatus = taskDetails.AssignStatus,
					DueDate = taskDetails.DueDate,
					PHPhoneNumber = taskDetails.PHPhoneNumber,
					PropertyHolderName = taskDetails.PropertyHolderName,
					CreatedAt = taskDetails.CreatedAt
				};

				return new ServiceResponse<TaskDetail>(taskModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<TaskDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		public ServiceResponse<TaskDetail> CreateTask(TaskDetail taskDetail)
		{
			try
			{
				var taskDetailModel = new TaskDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					// Create Task
					var taskModel = new Task()
					{
						AssignStatus = taskDetail.AssignStatus,
						DueDate = taskDetail.DueDate,
						PHAddress = taskDetail.PHAddress,
						PHPhoneNumber = taskDetail.PHPhoneNumber,
						PropertyHolderName = taskDetail.PropertyHolderName,
						CallerSystemUserId = taskDetail.CallerSystemUserId,
						CreatedAt = DateTime.Now,
						ModifiedAt = DateTime.Now,
						JobCategoryId = taskDetail.JobCategoryId,
						MaximumBudget = taskDetail.MaximumBudget,
						Purpose = taskDetail.Purpose
					};

					_taskRepository.AddOrUpdate(taskModel);
					_taskRepository.Save();

					//Add activity log
					var activityLog = new ActivityLog()
					{
						CreatedAt = DateTime.Now,
						EntityId = taskModel.Id,
						Description = taskModel.PropertyHolderName,
						Name = "Task Created",
					};

					_activityLogRepository.AddOrUpdate(activityLog);
					_activityLogRepository.Save();

					scope.Complete();

					// Genarate task details object
					taskDetailModel.AssignStatus = taskModel.AssignStatus;
					taskDetailModel.DueDate = taskModel.DueDate;
					taskDetailModel.Id = taskModel.Id;
					taskDetailModel.PHAddress = taskModel.PHAddress;
					taskDetailModel.PHPhoneNumber = taskModel.PHPhoneNumber;
					taskDetailModel.PropertyHolderName = taskModel.PropertyHolderName;
					taskDetailModel.CreatedAt = taskModel.CreatedAt;
					taskDetailModel.CallerSystemUserId = taskModel.CallerSystemUserId;

					return new ServiceResponse<TaskDetail>(taskDetailModel);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<TaskDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

		public ServiceResponse<TaskDetail> UpdateTask(TaskDetail taskDetail)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var taskDetailModel = new TaskDetail();
					var existingTask = _taskRepository.Get(x => x.Id == taskDetail.Id).FirstOrDefault();

					if (existingTask != null)
					{
						existingTask.AssignStatus = taskDetail.AssignStatus;
						existingTask.DueDate = taskDetail.DueDate;
						existingTask.PHAddress = taskDetail.PHAddress;
						existingTask.PHPhoneNumber = taskDetail.PHPhoneNumber;
						existingTask.PropertyHolderName = taskDetail.PropertyHolderName;
						//existingTask.CallerSystemUserId = taskDetail.CallerSystemUserId;
						existingTask.ModifiedAt = DateTime.Now;
						existingTask.JobCategoryId = taskDetail.JobCategoryId;
						existingTask.Purpose = taskDetail.Purpose;

						_taskRepository.AddOrUpdate(existingTask);
						_taskRepository.Save();

						//Add activity log
						var activityLog = new ActivityLog();

						activityLog.CreatedAt = DateTime.Now;
						activityLog.EntityId = existingTask.Id;
						activityLog.Description = existingTask.PropertyHolderName;
						activityLog.Name = "Task Updated";


						_activityLogRepository.AddOrUpdate(activityLog);
						_activityLogRepository.Save();
					}

					scope.Complete();

					// Genarate task details object
					taskDetailModel.AssignStatus = existingTask.AssignStatus;
					taskDetailModel.DueDate = existingTask.DueDate;
					taskDetailModel.Id = existingTask.Id;
					taskDetailModel.PHAddress = existingTask.PHAddress;
					taskDetailModel.PHPhoneNumber = existingTask.PHPhoneNumber;
					taskDetailModel.PropertyHolderName = existingTask.PropertyHolderName;
					taskDetailModel.CreatedAt = existingTask.CreatedAt;
					taskDetailModel.CallerSystemUserId = existingTask.CallerSystemUserId;

					return new ServiceResponse<TaskDetail>(taskDetailModel);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<TaskDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
