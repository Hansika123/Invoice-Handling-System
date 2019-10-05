using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface ITaskService
	{
		ServiceResponseList<TaskDetail> GetAllTasks();

		ServiceResponseList<TaskDetail> GetTasksByUserId(int systemUserId);

		ServiceResponse<TaskDetail> CreateTask(TaskDetail taskDetail);

		ServiceResponse<TaskDetail> GetTaskById(int taskId);

		ServiceResponse<TaskDetail> UpdateTask(TaskDetail taskDetail);
	}
}
