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
	public class InvoiceService : IInvoiceService
	{
		private readonly ICommonRepository<Invoice> _invoiceRepository;
		private readonly ICommonRepository<Entity.Item> _itemRepository;
		private readonly ICommonRepository<Entity.InvoiceItem> _invoiceItemRepository;
		private readonly ICommonRepository<ActivityLog> _activityLogRepository;

		public InvoiceService(ICommonRepository<Invoice> invoiceRepository, ICommonRepository<Entity.Item> itemRepository,
			ICommonRepository<Entity.InvoiceItem> invoiceItemRepository, ICommonRepository<ActivityLog> activityLogRepository)
		{
			_invoiceRepository = invoiceRepository;
			_itemRepository = itemRepository;
			_invoiceItemRepository = invoiceItemRepository;
			_activityLogRepository = activityLogRepository;
		}


		public InvoiceService() : this(new CommonRepository<Invoice>(), new CommonRepository<Entity.Item>(),
			new CommonRepository<Entity.InvoiceItem>(), new CommonRepository<ActivityLog>())
		{
		}

		public ServiceResponse<InvoiceDetail> CreateInvoice(InvoiceDetail invoice)
		{
			try
			{
				var invoideDetail = new InvoiceDetail();
				using (TransactionScope scope = new TransactionScope())
				{

					// Create invoice
					var invoiceData = new Invoice()
					{
						CreatedAt = DateTime.Now,
						Description = invoice.Description,
						//Discount = invoide.Discount,						
						//InvoiceNumber = invoide.InvoiceNumber,
						InvoiceStatus = invoice.InvoiceStatus != null ? invoice.InvoiceStatus : 0,
						InvoiceSubject = invoice.InvoiceSubject,
						ModifiedAt = DateTime.Now,
						Quentity = invoice.Quentity,
						ServiceFee = invoice.ServiceFee,
						ServiceProvideId = invoice.ServiceProvideId,
						SubTotal = invoice.SubTotal,
						Total = invoice.Total
					};
					_invoiceRepository.AddOrUpdate(invoiceData);
					_invoiceRepository.Save();


					// Create items
					foreach (var item in invoice.Items)
					{
						var itemDetails = new Entity.Item()
						{
							CreatedAt = DateTime.Now,
							ItemCode = item.ItemCode,
							ItemDescription = item.ItemDescription,
							ItemName = item.ItemName,
							UnitPrice = item.UnitPrice,
							EstimatedQuentity = item.EstimatedQuentity
						};

						_itemRepository.AddOrUpdate(itemDetails);
						_itemRepository.Save();


						// Create invoice items
						var invoiceItem = new Entity.InvoiceItem()
						{
							CreatedAt = DateTime.Now,
							InvoiceId = invoiceData.Id,
							ItemId = itemDetails.Id,
						};

						_invoiceItemRepository.AddOrUpdate(invoiceItem);
						_invoiceItemRepository.Save();
					}

					//Add activity log
					var activityLog = new ActivityLog()
					{
						CreatedAt = DateTime.Now,
						EntityId = invoiceData.Id,
						Description = invoiceData.InvoiceSubject,
						Name = "Invoice Received",
					};

					_activityLogRepository.AddOrUpdate(activityLog);
					_activityLogRepository.Save();


					scope.Complete();

					invoideDetail.Id = invoiceData.Id;
					invoideDetail.Date = invoiceData.CreatedAt;
					invoideDetail.Description = invoiceData.Description;
					//invoideDetail.Discount = invoiceData.Discount,
					invoideDetail.InvoiceNumber = invoiceData.Id;
					invoideDetail.InvoiceStatus = invoiceData.InvoiceStatus != null ? invoiceData.InvoiceStatus : 0;
					invoideDetail.InvoiceSubject = invoiceData.InvoiceSubject;
					//invoideDetail.ItemCode = invoiceData.;
					//invoideDetail.Items = signup.FirstName;
					invoideDetail.Quentity = invoiceData.Quentity;
					invoideDetail.ServiceFee = invoiceData.ServiceFee;
					invoideDetail.ServiceProvideId = invoiceData.ServiceProvideId;
					invoideDetail.SubTotal = invoiceData.SubTotal;
					invoideDetail.Total = invoiceData.Total;

					return new ServiceResponse<InvoiceDetail>(invoideDetail);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponseList<InvoiceDetail> GetAllInvoice()
		{
			var invoiceList = new List<InvoiceDetail>();
			try
			{
				var invoicesRequest = _invoiceRepository.GetAll();
				foreach (var invoice in invoicesRequest)
				{
					var invoiceItems = _invoiceItemRepository.Get(x => x.InvoiceId == invoice.Id);
					var invoiceItemList = new List<Models.Item>();

					foreach (var iitem in invoiceItems)
					{
						var itemDetails = new Models.Item()
						{
							CreatedAt = DateTime.Now,
							ItemCode = iitem.Item.ItemCode,
							ItemDescription = iitem.Item.ItemDescription,
							ItemName = iitem.Item.ItemName,
							ModifiedAt = DateTime.Now,
							UnitPrice = iitem.Item.UnitPrice,
							EstimatedQuentity = iitem.Item.EstimatedQuentity.HasValue ? iitem.Item.EstimatedQuentity.Value : 0
						};

						invoiceItemList.Add(itemDetails);
					}

					var InvoiceDetailModel = new InvoiceDetail()
					{
						Id = invoice.Id,
						Date = invoice.CreatedAt,
						Description = invoice.Description,
						//Discount = invoice.di,
						//InvoiceNumber = invoice.CatId,
						InvoiceStatus = invoice.InvoiceStatus,
						InvoiceSubject = invoice.InvoiceSubject,
						ServiceProviderName = invoice.ServiceProvider.CompanyName,
						Quentity = invoice.Quentity,
						ServiceFee = invoice.ServiceFee,
						ServiceProvideId = invoice.ServiceProvideId,
						SubTotal = invoice.SubTotal,
						Total = invoice.Total,
						Items = invoiceItemList,
						ServiceProviderAddress = invoice.ServiceProvider.Address
					};

					invoiceList.Add(InvoiceDetailModel);
				}

				return new ServiceResponseList<InvoiceDetail>(invoiceList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<InvoiceDetail> GetInvoiceById(int invoiceId)
		{
			try
			{
				var invoiceRequest = _invoiceRepository.Get(x => x.Id == invoiceId).FirstOrDefault();
				var invoiceItems = _invoiceItemRepository.Get(x => x.InvoiceId == invoiceId);
				var invoiceItemList = new List<Models.Item>();

				foreach (var iitem in invoiceItems)
				{
					var itemDetails = new Models.Item()
					{
						CreatedAt = DateTime.Now,
						ItemCode = iitem.Item.ItemCode,
						ItemDescription = iitem.Item.ItemDescription,
						ItemName = iitem.Item.ItemName,
						ModifiedAt = DateTime.Now,
						UnitPrice = iitem.Item.UnitPrice
					};

					invoiceItemList.Add(itemDetails);
				}

				var InvoiceDetailModel = new InvoiceDetail()
				{
					Id = invoiceRequest.Id,
					Date = invoiceRequest.CreatedAt,
					Description = invoiceRequest.Description,
					//Discount = invoice.di,
					//InvoiceNumber = invoice.CatId,
					InvoiceStatus = invoiceRequest.InvoiceStatus,
					InvoiceSubject = invoiceRequest.InvoiceSubject,
					//ItemCode = invoice.,
					Quentity = invoiceRequest.Quentity,
					ServiceFee = invoiceRequest.ServiceFee,
					ServiceProvideId = invoiceRequest.ServiceProvideId,
					SubTotal = invoiceRequest.SubTotal,
					Total = invoiceRequest.Total,
					Items = invoiceItemList
				};

				return new ServiceResponse<InvoiceDetail>(InvoiceDetailModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponseList<InvoiceDetail> GetInvoiceByServiceProviderId(int serviceProviderId)
		{
			var invoiceList = new List<InvoiceDetail>();
			try
			{
				var invoicesRequest = _invoiceRepository.Get(x => x.ServiceProvideId == serviceProviderId);
				foreach (var invoice in invoicesRequest)
				{
					var invoiceItems = _invoiceItemRepository.Get(x => x.InvoiceId == invoice.Id);
					var invoiceItemList = new List<Models.Item>();

					foreach (var iitem in invoiceItems)
					{
						var itemDetails = new Models.Item()
						{
							CreatedAt = DateTime.Now,
							ItemCode = iitem.Item.ItemCode,
							ItemDescription = iitem.Item.ItemDescription,
							ItemName = iitem.Item.ItemName,
							ModifiedAt = DateTime.Now,
							UnitPrice = iitem.Item.UnitPrice
						};

						invoiceItemList.Add(itemDetails);
					}

					var InvoiceDetailModel = new InvoiceDetail()
					{
						Id = invoice.Id,
						Date = invoice.CreatedAt,
						Description = invoice.Description,
						//Discount = invoice.di,
						//InvoiceNumber = invoice.CatId,
						InvoiceStatus = invoice.InvoiceStatus,
						InvoiceSubject = invoice.InvoiceSubject,
						//ItemCode = invoice.,
						Quentity = invoice.Quentity,
						ServiceFee = invoice.ServiceFee,
						ServiceProvideId = invoice.ServiceProvideId,
						SubTotal = invoice.SubTotal,
						Total = invoice.Total,
						Items = invoiceItemList
					};

					invoiceList.Add(InvoiceDetailModel);
				}

				return new ServiceResponseList<InvoiceDetail>(invoiceList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<InvoiceDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<bool> UpdateInvoiceStatus(int invoiceId, int invoiceStatus)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{

					// Update invoice status
					var updatingInvoice = _invoiceRepository.Get(x => x.Id == invoiceId).FirstOrDefault();

					if (updatingInvoice != null)
					{
						updatingInvoice.InvoiceStatus = invoiceStatus;
						updatingInvoice.ModifiedAt = DateTime.Now;

						_invoiceRepository.AddOrUpdate(updatingInvoice);
						_invoiceRepository.Save();

						//Add activity log
						var activityLog = new ActivityLog()
						{
							CreatedAt = DateTime.Now,
							EntityId = updatingInvoice.Id,
							Description = updatingInvoice.InvoiceSubject,
							Name = "Payment completed",
						};

						_activityLogRepository.AddOrUpdate(activityLog);
						_activityLogRepository.Save();

						scope.Complete();

						return new ServiceResponse<bool>(true);
					}
					return new ServiceResponse<bool>(false);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<bool>(false)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}
	}
}
