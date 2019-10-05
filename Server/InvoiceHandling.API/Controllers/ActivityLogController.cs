using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class ActivityLogController : ApiController
	{

		private readonly IActivityLogService _activityLogService;

		public ActivityLogController(IActivityLogService activityLogService)
		{
			_activityLogService = activityLogService;
		}

		public ActivityLogController() : this(new ActivityLogService())
		{
		}



		[HttpGet]
		[Route("GetAllActivityLogs")]
		public ServiceResponseList<ActivityLogDetails> GetAllActivityLogs()
		{
			try
			{
				var activityLogs = _activityLogService.GetAllActivityLogs().Entities;
				return new ServiceResponseList<ActivityLogDetails>(activityLogs);
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

		[HttpGet]
		[Route("GetActivityLogById")]
		public ServiceResponse<ActivityLogDetails> GetActivityLogById(int activityLogId)
		{
			try
			{
				var activityLog = _activityLogService.GetActivityLogById(activityLogId).Entity;
				return new ServiceResponse<ActivityLogDetails>(activityLog);
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
