using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Entity;
using InvoiceHandling.Models;
using InvoiceHandling.Repository.Interfaces;
using InvoiceHandling.Repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceHandling.Business.Services
{
	public class ActivityLogService : IActivityLogService
	{
		private readonly ICommonRepository<ActivityLog> _activityLogRepository;


		public ActivityLogService(ICommonRepository<ActivityLog> activityLogRepository)
		{
			_activityLogRepository = activityLogRepository;

		}


		public ActivityLogService() : this(new CommonRepository<ActivityLog>())
		{ }


		public ServiceResponseList<ActivityLogDetails> GetAllActivityLogs()
		{
			try
			{
				var activityLogList = new List<ActivityLogDetails>();

				var activities = _activityLogRepository.GetAll();
				foreach (var activity in activities)
				{
					var activityLogModel = new ActivityLogDetails()
					{
						Id = activity.Id,
						Description = activity.Description,
						InvoiceId = activity.EntityId,
						CreatedAt = activity.CreatedAt,
						Name = activity.Name,
						ModifiedAt = activity.ModifiedAt
					};

					activityLogList.Add(activityLogModel);
				}

				return new ServiceResponseList<ActivityLogDetails>(activityLogList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<ActivityLogDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<ActivityLogDetails> GetActivityLogById(int activityLogId)
		{
			try
			{
				var activities = _activityLogRepository.Get(x => x.Id == activityLogId).FirstOrDefault();
				var activityLogModel = new ActivityLogDetails()
				{
					Id = activities.Id,
					Description = activities.Description,
					InvoiceId = activities.EntityId,
					CreatedAt = activities.CreatedAt,
					Name = activities.Name,
					ModifiedAt = activities.ModifiedAt
				};

				return new ServiceResponse<ActivityLogDetails>(activityLogModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<ActivityLogDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
