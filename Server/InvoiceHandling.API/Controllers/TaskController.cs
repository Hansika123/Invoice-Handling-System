using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class TaskController : ApiController
	{
		private readonly ITaskService _taskService;

		public TaskController(ITaskService taskService)
		{
			_taskService = taskService;
		}

		public TaskController() : this(new TaskService())
		{
		}


		[HttpGet]
		[Route("GetAllTasks")]
		public ServiceResponseList<TaskDetail> GetAllTasks()
		{
			try
			{
				var taskDetails = _taskService.GetAllTasks().Entities;
				return new ServiceResponseList<TaskDetail>(taskDetails);
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


		[HttpGet]
		[Route("GetTasksByUserId")]
		public ServiceResponseList<TaskDetail> GetTasksByUserId(int callerSystemUserId)
		{
			try
			{
				var taskDetails = _taskService.GetTasksByUserId(callerSystemUserId).Entities;
				return new ServiceResponseList<TaskDetail>(taskDetails);
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


		[HttpGet]
		[Route("GetTaskById")]
		public ServiceResponse<TaskDetail> GetTaskById(int taskId)
		{
			try
			{
				var taskDetail = _taskService.GetTaskById(taskId).Entity;
				return new ServiceResponse<TaskDetail>(taskDetail);
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


		[HttpPost]
		[Route("CreateTask")]
		public ServiceResponse<TaskDetail> CreateTask([FromBody]TaskDetail taskDetail)
		{
			try
			{
				var task = _taskService.CreateTask(taskDetail).Entity;
				return new ServiceResponse<TaskDetail>(task);
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


		[HttpPost]
		[Route("UpdateTask")]
		public ServiceResponse<TaskDetail> UpdateTask([FromBody]TaskDetail taskDetail)
		{
			try
			{
				var task = _taskService.UpdateTask(taskDetail).Entity;
				return new ServiceResponse<TaskDetail>(task);
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
