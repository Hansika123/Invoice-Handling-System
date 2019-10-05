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
	public class QuatationService : IQuatationService
	{
		private readonly ICommonRepository<QuoteRequest> _quatationRequestRepository;
		private readonly ICommonRepository<QuoteRequestDetail> _quoteRequestDetailRepository;
		private readonly ICommonRepository<Entity.ServiceProvider> _serviceProviderRepository;
		private readonly ICommonRepository<Quotation> _quotationRepository;
		private readonly ICommonRepository<Entity.QuotationItem> _quotationItemRepository;
		private readonly ICommonRepository<Entity.Item> _itemRepository;
		private readonly ICommonRepository<ActivityLog> _activityLogRepository;
		private readonly ICommonRepository<JobCategory> _jobCategoryRepository;


		public QuatationService(ICommonRepository<QuoteRequest> quatationRequestRepository,
			ICommonRepository<QuoteRequestDetail> quoteRequestDetailRepository,
			ICommonRepository<Entity.ServiceProvider> serviceProviderRepository,
			ICommonRepository<Entity.Quotation> quotationRepository, ICommonRepository<Entity.QuotationItem> quotationItemRepository,
			ICommonRepository<Entity.Item> itemRepository, ICommonRepository<ActivityLog> activityLogRepository,
			ICommonRepository<JobCategory> jobCategoryRepository)
		{
			_quatationRequestRepository = quatationRequestRepository;
			_quoteRequestDetailRepository = quoteRequestDetailRepository;
			_serviceProviderRepository = serviceProviderRepository;
			_quotationRepository = quotationRepository;
			_itemRepository = itemRepository;
			_quotationItemRepository = quotationItemRepository;
			_activityLogRepository = activityLogRepository;
			_jobCategoryRepository = jobCategoryRepository;
		}


		public QuatationService() : this(new CommonRepository<QuoteRequest>(), new CommonRepository<QuoteRequestDetail>(),
			new CommonRepository<Entity.ServiceProvider>(), new CommonRepository<Entity.Quotation>(),
			new CommonRepository<Entity.QuotationItem>(), new CommonRepository<Entity.Item>(), new CommonRepository<ActivityLog>(),
			new CommonRepository<JobCategory>())
		{ }



		public ServiceResponse<QuatationRequestDetail> CreateQuatationRequest(QuatationRequestDetail quatationRequestDetail)
		{
			try
			{
				var quatationRequestDetailModel = new QuatationRequestDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					// Create QuoteRequest
					var quoteRequest = new QuoteRequest()
					{
						Description = quatationRequestDetail.RequestDescription,
						Name = quatationRequestDetail.RequestName,
						CreatedAt = DateTime.Now,
						DueDate = quatationRequestDetail.DueDate,
						PropertyHolderName = quatationRequestDetail.PropertyHolderName,
						PHPhoneNumber = quatationRequestDetail.PHPhoneNumber,
						PHAddress = quatationRequestDetail.PHAddress,
						JobCategoryId = quatationRequestDetail.JobCategoryId,
						TaskId = (quatationRequestDetail.TaskId == null || quatationRequestDetail.TaskId == 0)
                        ? null : quatationRequestDetail.TaskId
                    };
					_quatationRequestRepository.AddOrUpdate(quoteRequest);
					_quatationRequestRepository.Save();


					//var serviceProvider = _serviceProviderRepository.Get(quatationRequestDetail.ServiceProviderId);
					//// Crearte Quatation request details
					//var quoteRequestDetail = new QuoteRequestDetail()
					//{
					//	RefId = RandomNumber(100, 10000),
					//	QuoteRequestId = quoteRequest.Id,
					//	ServiceProviderId = serviceProvider.Id,
					//	CreatedAt = DateTime.Now,
					//};
					//_quoteRequestDetailRepository.AddOrUpdate(quoteRequestDetail);
					//_quoteRequestDetailRepository.Save();

					//Add activity log
					var activityLog = new ActivityLog()
					{
						CreatedAt = DateTime.Now,
						EntityId = quoteRequest.Id,
						Description = quoteRequest.Description,
						Name = "Quatation Created",
					};

					_activityLogRepository.AddOrUpdate(activityLog);
					_activityLogRepository.Save();

					scope.Complete();

					// Genarate quatationRequestDetailModel
					quatationRequestDetailModel.QuatationRequestId = quoteRequest.Id;
					//quatationRequestDetailModel.QuatationRequestDetailId = quoteRequestDetail.Id;
					quatationRequestDetailModel.RequestName = quoteRequest.Name;
					quatationRequestDetailModel.DueDate = quoteRequest.DueDate;
					quatationRequestDetailModel.RequestDescription = quoteRequest.Description;
					//quatationRequestDetailModel.RefId = quoteRequestDetail.RefId;
					//quatationRequestDetailModel.ServiceProviderId = quoteRequestDetail.ServiceProviderId;
					quatationRequestDetailModel.CreatedAt = DateTime.Now;
					quatationRequestDetailModel.isQuotationCreated = false;

					return new ServiceResponse<QuatationRequestDetail>(quatationRequestDetailModel);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuatationRequestDetail>(null)
				{
					HasError = true,
				};
			}
		}


		public ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderId(int serviceProviderId)
		{
			var quoteRequestList = new List<QuatationRequestDetail>();

			var quoteRequest = _quoteRequestDetailRepository.Get(a => a.ServiceProvider.Id == serviceProviderId);
			foreach (var request in quoteRequest)
			{
				var quatationRequestDetailModel = new QuatationRequestDetail()
				{
					RequestName = request.QuoteRequest.Name,
					RequestDescription = request.QuoteRequest.Description,
					DueDate = request.QuoteRequest.DueDate,
					CreatedAt = request.QuoteRequest.CreatedAt,
					RefId = request.RefId,
					QuatationRequestId = request.QuoteRequestId,
					ServiceProviderId = request.ServiceProviderId,
					isQuotationCreated = request.QuoteRequest.isQuotationCreated
				};

				quoteRequestList.Add(quatationRequestDetailModel);
			}

			return new ServiceResponseList<QuatationRequestDetail>(quoteRequestList);
		}


		public ServiceResponseList<QuatationRequestDetail> GetAllQuoteRequest()
		{
			var quoteRequestList = new List<QuatationRequestDetail>();

			var quoteRequest = _quatationRequestRepository.GetAll();
			foreach (var request in quoteRequest)
			{
				var category = _jobCategoryRepository.Get(a => a.Id == request.JobCategoryId).FirstOrDefault();
				var jobCategory = new JobCategoryDetail();
				if (category != null)
				{
					jobCategory.Id = category.Id;
					jobCategory.Name = category.Name;
				};

				var quatationRequestDetailModel = new QuatationRequestDetail()
				{
					RequestName = request.Name,
					RequestDescription = request.Description,
					DueDate = request.DueDate,
					CreatedAt = request.CreatedAt,
					//RefId = request.RefId,
					QuatationRequestId = request.Id,
					//ServiceProviderId = request.ServiceProviderId,
					isQuotationCreated = request.isQuotationCreated,
					JobCategoryId = request.JobCategoryId,
					PropertyHolderName = request.PropertyHolderName,
					PHPhoneNumber = request.PHPhoneNumber,
					PHAddress = request.PHAddress,
					JobCategoryDetail = jobCategory
				};

				quoteRequestList.Add(quatationRequestDetailModel);
			}

			return new ServiceResponseList<QuatationRequestDetail>(quoteRequestList);
		}


		public ServiceResponse<QuotationDetail> CreateQuatation(QuotationDetail quotationDetail)
		{
			try
			{
				var QuotationDetailModel = new QuotationDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					// Create Quatation
					var quotation = new Quotation()
					{
						RefferenceId = RandomNumber(100, 10000),
						ApprovedAdminId = quotationDetail.ApprovedAdminId,
						EstimatedSubTotal = quotationDetail.EstimatedSubTotal,
						EstimatedServiceFee = quotationDetail.EstimatedServiceFee,
						EstimatedTotal = quotationDetail.EstimatedTotal,
						JobDescription = quotationDetail.JobDescription,
						ServiceProviderId = quotationDetail.ServiceProviderId,
						Status = quotationDetail.Status, // 0: not accepted, 1: accepted
						CreatedAt = DateTime.Now,
						QuatationRequestId = quotationDetail.QuatationRequestId
					};
					_quotationRepository.AddOrUpdate(quotation);
					_quotationRepository.Save();

					// Update the quatation request table
					var quatationRequest = _quatationRequestRepository.Get(x => x.Id == quotationDetail.QuatationRequestId).FirstOrDefault();
					if (quatationRequest != null)
					{
						quatationRequest.isQuotationCreated = true;

						_quatationRequestRepository.AddOrUpdate(quatationRequest);
						_quatationRequestRepository.Save();
					}


					var itemList = new List<Models.Item>();
					// Create items
					foreach (var item in quotationDetail.Items)
					{
						var itemData = new Entity.Item()
						{
							ItemDescription = item.ItemDescription,
							EstimatedQuentity = item.EstimatedQuentity,
							UnitPrice = item.UnitPrice,
							ItemCode = item.ItemCode,
							ItemName = item.ItemName,
							CreatedAt = DateTime.Now
						};

						_itemRepository.AddOrUpdate(itemData);
						_itemRepository.Save();


						// Create QuatationItems
						var quotationItem = new Entity.QuotationItem()
						{
							QuotationId = quotation.Id,
							ItemId = itemData.Id,
							CreatedAt = DateTime.Now
						};
						_quotationItemRepository.AddOrUpdate(quotationItem);
						_quotationItemRepository.Save();

						// Convery entityItem to Item
						var itemModel = new Models.Item()
						{
							Id = itemData.Id,
							ItemDescription = itemData.ItemDescription,
							UnitPrice = itemData.UnitPrice,
							ItemCode = itemData.ItemCode,
							ItemName = itemData.ItemName,
							EstimatedQuentity = itemData.EstimatedQuentity.HasValue ? itemData.EstimatedQuentity.Value : 0,
							CreatedAt = DateTime.Now
						};
						itemList.Add(itemModel);
					}

					scope.Complete();

					// Genarate QuotationDetailModel
					QuotationDetailModel.Items = itemList;
					QuotationDetailModel.RefferenceId = quotation.RefferenceId;
					QuotationDetailModel.ApprovedAdminId = quotation.ApprovedAdminId;
					QuotationDetailModel.EstimatedSubTotal = quotation.EstimatedSubTotal;
					QuotationDetailModel.EstimatedServiceFee = quotation.EstimatedServiceFee;
					QuotationDetailModel.EstimatedTotal = quotation.EstimatedTotal;
					QuotationDetailModel.JobDescription = quotation.JobDescription;
					QuotationDetailModel.ServiceProviderId = quotation.ServiceProviderId;
					QuotationDetailModel.Status = quotation.Status;
					QuotationDetailModel.CreatedAt = DateTime.Now;
					QuotationDetailModel.QuatationRequestId = quotation.QuatationRequestId;

					return new ServiceResponse<QuotationDetail>(QuotationDetailModel);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuotationDetail>(null)
				{
					HasError = true,
				};
			}
		}


		public ServiceResponseList<QuotationDetail> GetAllQuatations()
		{
			var quatationList = new List<QuotationDetail>();

			var quatations = _quotationRepository.GetAll();
			foreach (var quatation in quatations)
			{
				var itemList = new List<Models.Item>();
				var quatationItems = _quotationItemRepository.Get(x => x.QuotationId == quatation.Id);

				foreach (var qItem in quatationItems)
				{
					var itemModel = new Models.Item()
					{
						Id = qItem.Item.Id,
						EstimatedQuentity = qItem.Item.EstimatedQuentity.HasValue ? qItem.Item.EstimatedQuentity.Value : 0,
						ItemCode = qItem.Item.ItemCode,
						ItemDescription = qItem.Item.ItemDescription,
						ItemName = qItem.Item.ItemName,
						UnitPrice = qItem.Item.UnitPrice,
						CreatedAt = qItem.Item.CreatedAt
					};
					itemList.Add(itemModel);
				}

				var requestDetail = _quatationRequestRepository.Get(x => x.Id == quatation.QuatationRequestId).FirstOrDefault();
				var quatationRequestDetailModel = new QuatationRequestDetail()
				{
					RequestName = requestDetail.Name,
					RequestDescription = requestDetail.Description,
					DueDate = requestDetail.DueDate,
					CreatedAt = requestDetail.CreatedAt,
					QuatationRequestId = requestDetail.Id
				};

				var quotationDetailModel = new QuotationDetail()
				{
					Id = quatation.Id,
					RefferenceId = quatation.RefferenceId,
					ApprovedAdminId = quatation.ApprovedAdminId,
					EstimatedSubTotal = quatation.EstimatedSubTotal,
					EstimatedServiceFee = quatation.EstimatedServiceFee,
					EstimatedTotal = quatation.EstimatedTotal,
					JobDescription = quatation.JobDescription,
					ServiceProviderId = quatation.ServiceProviderId,
					Status = quatation.Status,
					CreatedAt = quatation.CreatedAt,
					ServiceProviderName = quatation.ServiceProvider.CompanyName,
					ServiceProviderAddress = quatation.ServiceProvider.Address,
					Items = itemList,
					QuatationRequestId = quatation.QuatationRequestId,
					QuatationRequestDetail = quatationRequestDetailModel
				};

				quatationList.Add(quotationDetailModel);
			}

			return new ServiceResponseList<QuotationDetail>(quatationList);
		}


		public ServiceResponseList<QuotationDetail> GetAllQuatationsByServiceProviderId(int serviceProviderId)
		{
			var quatationList = new List<QuotationDetail>();

			var quatations = _quotationRepository.Get(x => x.ServiceProviderId == serviceProviderId);
			foreach (var quatation in quatations)
			{
				var requestDetail = _quatationRequestRepository.Get(x => x.Id == quatation.QuatationRequestId).FirstOrDefault();
				var quatationRequestDetailModel = new QuatationRequestDetail()
				{
					RequestName = requestDetail.Name,
					RequestDescription = requestDetail.Description,
					DueDate = requestDetail.DueDate,
					CreatedAt = requestDetail.CreatedAt,
					QuatationRequestId = requestDetail.Id
				};


				var quotationDetailModel = new QuotationDetail()
				{
					RefferenceId = quatation.RefferenceId,
					ApprovedAdminId = quatation.ApprovedAdminId,
					EstimatedSubTotal = quatation.EstimatedSubTotal,
					EstimatedServiceFee = quatation.EstimatedServiceFee,
					EstimatedTotal = quatation.EstimatedTotal,
					JobDescription = quatation.JobDescription,
					ServiceProviderId = quatation.ServiceProviderId,
					Status = quatation.Status,
					CreatedAt = quatation.CreatedAt,
					QuatationRequestId = quatationRequestDetailModel.QuatationRequestId,
					QuatationRequestDetail = quatationRequestDetailModel
				};

				quatationList.Add(quotationDetailModel);
			}

			return new ServiceResponseList<QuotationDetail>(quatationList);
		}


		public ServiceResponse<QuotationDetail> UpdateQuatationStatus(int quatationId, int statusId)
		{
			try
			{
				var QuotationDetailModel = new QuotationDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					var existingQuatation = _quotationRepository.Get(quatationId);

					if (existingQuatation != null)
					{
						existingQuatation.Status = statusId;

						_quotationRepository.AddOrUpdate(existingQuatation);
						_quotationRepository.Save();

						//Add activity log
						var activityLog = new ActivityLog()
						{
							CreatedAt = DateTime.Now,
							EntityId = existingQuatation.Id,
							Description = existingQuatation.JobDescription,
							Name = "Quatation updated",
						};

						_activityLogRepository.AddOrUpdate(activityLog);
						_activityLogRepository.Save();

						scope.Complete();

						// Genarate QuotationDetailModel
						QuotationDetailModel.RefferenceId = existingQuatation.RefferenceId;
						QuotationDetailModel.ApprovedAdminId = existingQuatation.ApprovedAdminId;
						QuotationDetailModel.EstimatedSubTotal = existingQuatation.EstimatedSubTotal;
						QuotationDetailModel.EstimatedServiceFee = existingQuatation.EstimatedServiceFee;
						QuotationDetailModel.EstimatedTotal = existingQuatation.EstimatedTotal;
						QuotationDetailModel.JobDescription = existingQuatation.JobDescription;
						QuotationDetailModel.ServiceProviderId = existingQuatation.ServiceProviderId;
						QuotationDetailModel.Status = existingQuatation.Status;
					}

					return new ServiceResponse<QuotationDetail>(QuotationDetailModel);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<QuotationDetail>(null)
				{
					HasError = true,
				};
			}
		}


		public ServiceResponseList<QuatationRequestDetail> GetQuoteRequestByServiceProviderCategoryId(int categoryId)
		{
			var quoteRequestList = new List<QuatationRequestDetail>();

			var quoteRequest = _quatationRequestRepository.Get(a => a.JobCategoryId == categoryId);
			foreach (var request in quoteRequest)
			{
				var category = _jobCategoryRepository.Get(a => a.Id == request.JobCategoryId).FirstOrDefault();
				var jobCategory = new JobCategoryDetail();
				if (category != null)
				{
					jobCategory.Id = category.Id;
					jobCategory.Name = category.Name;
				};

				var quatationRequestDetailModel = new QuatationRequestDetail()
				{
					RequestName = request.Name,
					RequestDescription = request.Description,
					DueDate = request.DueDate,
					CreatedAt = request.CreatedAt,
					//RefId = request.RefId,
					QuatationRequestId = request.Id,
					//ServiceProviderId = request.ServiceProviderId,
					isQuotationCreated = request.isQuotationCreated,
					JobCategoryId = request.JobCategoryId,
					PropertyHolderName = request.PropertyHolderName,
					PHPhoneNumber = request.PHPhoneNumber,
					PHAddress = request.PHAddress,
					JobCategoryDetail = jobCategory
				};

				quoteRequestList.Add(quatationRequestDetailModel);
			}

			return new ServiceResponseList<QuatationRequestDetail>(quoteRequestList);
		}


		private int RandomNumber(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max);
		}
	}
}
