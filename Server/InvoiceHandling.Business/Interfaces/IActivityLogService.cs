using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IActivityLogService
	{
		ServiceResponseList<ActivityLogDetails> GetAllActivityLogs();

		ServiceResponse<ActivityLogDetails> GetActivityLogById(int activityLogId);
	}
}
