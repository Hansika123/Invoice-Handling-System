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
	public class ServiceProviderService : IServiceProviderService
	{
		private readonly ICommonRepository<Entity.ServiceProvider> _serviceProviderRepository;
		private readonly ICommonRepository<BankAcount> _bankAccountRepository;
		private readonly ICommonRepository<JobCategory> _jobCategoryRepository;
		private readonly ICommonRepository<Feedback> _feedbackRepository;
		private readonly ICommonRepository<BankAcount> _bankAcountRepository;


		public ServiceProviderService(ICommonRepository<Entity.ServiceProvider> serviceProviderRepository,
			ICommonRepository<BankAcount> bankAccountRepository, ICommonRepository<JobCategory> jobCategoryRepository,
			ICommonRepository<Feedback> feedbackRepository, ICommonRepository<BankAcount> bankAcountRepository)
		{
			_serviceProviderRepository = serviceProviderRepository;
			_bankAccountRepository = bankAccountRepository;
			_jobCategoryRepository = jobCategoryRepository;
			_feedbackRepository = feedbackRepository;
			_bankAcountRepository = bankAcountRepository;
		}


		public ServiceProviderService() : this(new CommonRepository<Entity.ServiceProvider>(), new CommonRepository<BankAcount>(),
			new CommonRepository<JobCategory>(), new CommonRepository<Feedback>(), new CommonRepository<BankAcount>())
		{ }


		public ServiceResponseList<Models.ServiceProvider> GetAllServiceProviders()
		{
			var ServiceProvidersList = new List<Models.ServiceProvider>();
			try
			{
				var serviceProvidersRequest = _serviceProviderRepository.GetAll();
				foreach (var serviceProvider in serviceProvidersRequest)
				{
					var serviceProviderBankDetail = new BankDetail();
					var bankDetail = _bankAccountRepository.Get(x => x.ServiceProvider.Id == serviceProvider.Id).FirstOrDefault();

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

					var userAccount = new UserAccountDetails();
					if (serviceProvider.AccountProfile != null)
					{
						var accountDetail = serviceProvider.AccountProfile;

						userAccount.ID = accountDetail.Id;
						userAccount.Email = accountDetail.SystemUser.Email;
						userAccount.IsActive = accountDetail.SystemUser.IsActive;
						userAccount.FirstName = accountDetail.FirstName;
						userAccount.LastName = accountDetail.LastName;
						userAccount.MobileNumber = accountDetail.Mobile;
						userAccount.ProfileImage = accountDetail.ProfileImage;
						userAccount.CreatedAt = accountDetail.CreatedAt;
						userAccount.Address = accountDetail.AddressLine1;
						userAccount.UserRole = accountDetail.UserRoleId;
					}

					var jobCategory = _jobCategoryRepository.Get(x => x.Id == serviceProvider.CatId).FirstOrDefault();
					var jobCategoryDetail = new JobCategoryDetail();
					if (jobCategory != null)
					{
						jobCategoryDetail.Id = jobCategory.Id;
						jobCategoryDetail.Name = jobCategory.Name;
					}


					var feedback = _feedbackRepository.Get(x => x.ServiceProviderId == serviceProvider.Id).FirstOrDefault();
					var serviceProviderModel = new Models.ServiceProvider()
					{
						Id = serviceProvider.Id,
						Address = serviceProvider.Address,
						Category = serviceProvider.Category,
						CreatedAt = serviceProvider.CreatedAt,
						CatId = serviceProvider.CatId,
						CompanyName = serviceProvider.CompanyName,
						ModifiedAt = serviceProvider.ModifiedAt,
						BankDetail = serviceProviderBankDetail,
						UserAccountDetails = userAccount,
						JobCategoryDetail = jobCategoryDetail,
						FeedbackLevel = feedback != null ? feedback.RatingLevel : 1
					};

					ServiceProvidersList.Add(serviceProviderModel);
				}

				return new ServiceResponseList<Models.ServiceProvider>(ServiceProvidersList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<Models.ServiceProvider>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<Models.ServiceProvider> GetServiceProviderById(int serviceProviderId)
		{
			try
			{
				var serviceProvider = _serviceProviderRepository.Get(x => x.Id == serviceProviderId).FirstOrDefault();
				var bankDetail = _bankAccountRepository.Get(x => x.ServiceProvider.Id == serviceProvider.Id).FirstOrDefault();

				var serviceProviderBankDetail = new BankDetail()
				{
					Id = bankDetail.Id,
					AccountName = bankDetail.AccountName,
					AccountNumber = bankDetail.AccountNumber,
					Bank = bankDetail.Bank,
					Branch = bankDetail.Branch,
					Info = bankDetail.Info,
					CreatedAt = bankDetail.CreatedAt
				};

				var userAccount = new UserAccountDetails();
				if (serviceProvider.AccountProfile != null)
				{
					var accountDetail = serviceProvider.AccountProfile;

					userAccount.ID = accountDetail.Id;
					userAccount.Email = accountDetail.SystemUser.Email;
					userAccount.IsActive = accountDetail.SystemUser.IsActive;
					userAccount.FirstName = accountDetail.FirstName;
					userAccount.LastName = accountDetail.LastName;
					userAccount.MobileNumber = accountDetail.Mobile;
					userAccount.ProfileImage = accountDetail.ProfileImage;
					userAccount.CreatedAt = accountDetail.CreatedAt;
					userAccount.Address = accountDetail.AddressLine1;
					userAccount.UserRole = accountDetail.UserRoleId;
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
					BankDetail = serviceProviderBankDetail,
					UserAccountDetails = userAccount
				};

				return new ServiceResponse<Models.ServiceProvider>(serviceProviderModel);
			}
			catch (Exception e)
			{
				return new ServiceResponse<Models.ServiceProvider>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}

		}


		public ServiceResponseList<JobCategoryDetail> GetAllCategories()
		{
			var jobCategoryList = new List<JobCategoryDetail>();
			try
			{
				var jobCategoryRequest = _jobCategoryRepository.GetAll();
				foreach (var category in jobCategoryRequest)
				{
					var jobCategoryDetail = new JobCategoryDetail();
					if (jobCategoryDetail != null)
					{
						jobCategoryDetail.Id = category.Id;
						jobCategoryDetail.Name = category.Name;
						jobCategoryDetail.CreatedAt = category.CreatedAt;
					}

					jobCategoryList.Add(jobCategoryDetail);
				}

				return new ServiceResponseList<JobCategoryDetail>(jobCategoryList);
			}
			catch (Exception e)
			{
				return new ServiceResponseList<JobCategoryDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<FeedbackDetails> CreateFeedback(FeedbackDetails feedbackDetails)
		{
			try
			{
				var feedbackDetail = new FeedbackDetails();
				using (TransactionScope scope = new TransactionScope())
				{
					// Create Task
					var feedback = new Feedback()
					{
						PropertyHolderName = feedbackDetails.PropertyHolderName,
						JobType = feedbackDetails.JobType,
						ServiceProviderId = feedbackDetails.ServiceProviderId,
						RatingLevel = feedbackDetails.RatingLevel,
						CreatedAt = DateTime.Now,
						ModifiedAt = DateTime.Now
					};

					_feedbackRepository.AddOrUpdate(feedback);
					_feedbackRepository.Save();


					scope.Complete();

					feedbackDetail.Id = feedback.Id;
					feedbackDetail.PropertyHolderName = feedback.PropertyHolderName;
					feedbackDetail.JobType = feedback.JobType;
					feedbackDetail.ServiceProviderId = feedback.ServiceProviderId;
					feedbackDetail.RatingLevel = feedback.RatingLevel;
					feedbackDetail.CreatedAt = feedback.CreatedAt;

					return new ServiceResponse<FeedbackDetails>(feedbackDetail);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<FeedbackDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<FeedbackDetails> GetFeedbackByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var feedbackList = new List<FeedbackDetails>();
				var feedback = _feedbackRepository.Get(x => x.ServiceProviderId == serviceProviderId).FirstOrDefault();

				var userAccount = new UserAccountDetails();
				if (feedback.ServiceProvider.AccountProfile != null)
				{
					var accountDetail = feedback.ServiceProvider.AccountProfile;

					userAccount.ID = accountDetail.Id;
					userAccount.Email = accountDetail.SystemUser.Email;
					userAccount.IsActive = accountDetail.SystemUser.IsActive;
					userAccount.FirstName = accountDetail.FirstName;
					userAccount.LastName = accountDetail.LastName;
					userAccount.MobileNumber = accountDetail.Mobile;
					userAccount.ProfileImage = accountDetail.ProfileImage;
					userAccount.CreatedAt = accountDetail.CreatedAt;
					userAccount.Address = accountDetail.AddressLine1;
					userAccount.UserRole = accountDetail.UserRoleId;
				}

				var serviceProvider = new Models.ServiceProvider();
				if (feedback.ServiceProvider != null)
				{
					serviceProvider.Id = serviceProvider.Id;
					serviceProvider.Address = serviceProvider.Address;
					serviceProvider.Category = serviceProvider.Category;
					serviceProvider.CreatedAt = serviceProvider.CreatedAt;
					serviceProvider.CatId = serviceProvider.CatId;
					serviceProvider.CompanyName = serviceProvider.CompanyName;
					serviceProvider.ModifiedAt = serviceProvider.ModifiedAt;
					serviceProvider.UserAccountDetails = userAccount;
				}

				var feed = new FeedbackDetails()
				{
					PropertyHolderName = feedback.PropertyHolderName,
					JobType = feedback.JobType,
					ServiceProviderId = feedback.ServiceProviderId,
					CreatedAt = feedback.CreatedAt,
					RatingLevel = feedback.RatingLevel,
					ServiceProvider = serviceProvider
				};

				return new ServiceResponse<FeedbackDetails>(feed);
			}
			catch (Exception e)
			{
				return new ServiceResponse<FeedbackDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<BankAcountDetail> CreateBankAccount(BankAcountDetail bankAcountDetail)
		{
			try
			{
				var bankAcountDetailObj = new BankAcountDetail();
				using (TransactionScope scope = new TransactionScope())
				{
					var existingAccount = _bankAcountRepository.Get(x => x.ServiceProviderId == bankAcountDetail.ServiceProviderId).FirstOrDefault();
					if (existingAccount == null)
					{
						var bank = new BankAcount();

						// Create new Account
						bank.AccountName = bankAcountDetail.AccountName;
						bank.AccountNumber = bankAcountDetail.AccountNumber;
						bank.Bank = bankAcountDetail.Bank;
						bank.Branch = bankAcountDetail.Branch;
						bank.CreatedAt = DateTime.Now;
						bank.ModifiedAt = DateTime.Now;
						bank.ServiceProviderId = bankAcountDetail.ServiceProviderId;
						bank.Info = bankAcountDetail.Info;

						_bankAcountRepository.AddOrUpdate(bank);
						_bankAcountRepository.Save();

						bankAcountDetailObj.Id = bank.Id;
						bankAcountDetailObj.AccountName = bank.AccountName;
						bankAcountDetailObj.AccountNumber = bank.AccountNumber;
						bankAcountDetailObj.ServiceProviderId = bank.ServiceProviderId;
						bankAcountDetailObj.Bank = bank.Bank;
						bankAcountDetailObj.CreatedAt = bank.CreatedAt;
						bankAcountDetailObj.Branch = bank.Branch;
					}
					else
					{
						existingAccount.AccountName = bankAcountDetail.AccountName;
						existingAccount.AccountNumber = bankAcountDetail.AccountNumber;
						existingAccount.Bank = bankAcountDetail.Bank;
						existingAccount.Branch = bankAcountDetail.Branch;
						existingAccount.ModifiedAt = DateTime.Now;
						existingAccount.ServiceProviderId = bankAcountDetail.ServiceProviderId;
						existingAccount.Info = bankAcountDetail.Info;

						_bankAcountRepository.AddOrUpdate(existingAccount);
						_bankAcountRepository.Save();

						bankAcountDetailObj.Id = existingAccount.Id;
						bankAcountDetailObj.AccountName = existingAccount.AccountName;
						bankAcountDetailObj.AccountNumber = existingAccount.AccountNumber;
						bankAcountDetailObj.ServiceProviderId = existingAccount.ServiceProviderId;
						bankAcountDetailObj.Bank = existingAccount.Bank;
						bankAcountDetailObj.CreatedAt = existingAccount.CreatedAt;
						bankAcountDetailObj.Branch = existingAccount.Branch;
					}

					scope.Complete();
					return new ServiceResponse<BankAcountDetail>(bankAcountDetailObj);
				}
			}
			catch (Exception e)
			{
				return new ServiceResponse<BankAcountDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<BankAcountDetail> GetBankAccountByServiceProviderId(int serviceProviderId)
		{
			try
			{
				var accountDetail = _bankAcountRepository.Get(x => x.ServiceProviderId == serviceProviderId).FirstOrDefault();

				var bankAcountDetail = new BankAcountDetail();
				if (accountDetail != null)
				{
					bankAcountDetail.Id = accountDetail.Id;
					bankAcountDetail.AccountName = accountDetail.AccountName;
					bankAcountDetail.AccountNumber = accountDetail.AccountNumber;
					bankAcountDetail.Bank = accountDetail.Bank;
					bankAcountDetail.Branch = accountDetail.Branch;
					bankAcountDetail.Info = accountDetail.Info;
					bankAcountDetail.ServiceProviderId = accountDetail.ServiceProviderId;
					bankAcountDetail.CreatedAt = accountDetail.CreatedAt;
				}

				return new ServiceResponse<BankAcountDetail>(bankAcountDetail);
			}
			catch (Exception e)
			{
				return new ServiceResponse<BankAcountDetail>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}

	}
}
