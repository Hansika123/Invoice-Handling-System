using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Entity;
using InvoiceHandling.Models;
using InvoiceHandling.Repository.Interfaces;
using InvoiceHandling.Repository.Services;
using System;
using System.Linq;
using System.Transactions;

namespace InvoiceHandling.Business.Services
{
	public class AuthService : IAuthService
	{
		private readonly ICommonRepository<SystemUser> _userRepository;
		private readonly ICommonRepository<AccountProfile> _acountProfileRepository;
		private readonly ICommonRepository<Entity.ServiceProvider> _serviceProviderRepository;


		public AuthService(ICommonRepository<SystemUser> userRepository, ICommonRepository<AccountProfile> acountProfileRepository,
			ICommonRepository<Entity.ServiceProvider> serviceProviderRepository)
		{
			_userRepository = userRepository;
			_acountProfileRepository = acountProfileRepository;
			_serviceProviderRepository = serviceProviderRepository;
		}


		public AuthService() : this(new CommonRepository<SystemUser>(), new CommonRepository<AccountProfile>(),
			new CommonRepository<Entity.ServiceProvider>())
		{
		}


		public ServiceResponse<UserAccountDetails> SignIn(Signin signin)
		{
			try
			{
				int? serviceProviderId = null;
				var loginUser = _userRepository.Get(a => a.Email == signin.UserName && a.Password == signin.Password).FirstOrDefault();

				if (loginUser != null)
				{
					var userAccount = new UserAccountDetails();

					var accountProfile = loginUser.AccountProfiles.FirstOrDefault();
					if (accountProfile.UserRoleId == 2)
					{
						//var serviceProviderProfile = _serviceProviderRepository.Get(sp => sp.AccountProfileId == loginUser.Id).FirstOrDefault();
						// var test = accountProfile.ServiceProviders.FirstOrDefault();

						var serviceProvider = _serviceProviderRepository.Get(o => o.AccountProfileId == accountProfile.Id).FirstOrDefault();


						userAccount.ServiceProviderId = serviceProvider.Id;
						userAccount.ServiceProviderCategoryId = serviceProvider.CatId;
					}

					userAccount.ID = loginUser.Id;
					userAccount.Email = loginUser.Email;
					userAccount.IsActive = loginUser.IsActive;
					userAccount.FirstName = accountProfile.FirstName;
					userAccount.LastName = accountProfile.LastName;
					userAccount.MobileNumber = accountProfile.Mobile;
					userAccount.ProfileImage = accountProfile.ProfileImage;
					userAccount.CreatedAt = accountProfile.CreatedAt;
					userAccount.Address = accountProfile.AddressLine1;
					userAccount.UserRole = accountProfile.UserRoleId;

					return new ServiceResponse<UserAccountDetails>(userAccount);
				}
				else
				{
					return new ServiceResponse<UserAccountDetails>(null)
					{
						HasError = true,
						ErrorMessage = "Incorrect Username or Password"
					};
				}

			}
			catch (Exception e)
			{
				return new ServiceResponse<UserAccountDetails>(null)
				{
					HasError = true,
					ErrorMessage = e.Message
				};
			}
		}


		public ServiceResponse<UserAccountDetails> SignUp(Signup signup)
		{
			try
			{
				var isUserExist = _userRepository.Get(a => a.Email == signup.Email).FirstOrDefault();
				if (isUserExist != null)
					return null;

				var userAccountDetails = new UserAccountDetails();
				using (TransactionScope scope = new TransactionScope())
				{
					var systemUser = new SystemUser()
					{
						UserGuid = Guid.NewGuid(),
						Email = signup.Email,
						Password = signup.Password,
						IsActive = true,
						CreatedAt = DateTime.Now,
						ModifiedAt = DateTime.Now
					};
					_userRepository.AddOrUpdate(systemUser);
					_userRepository.Save();


					var accountProfile = new AccountProfile()
					{
						FirstName = signup.FirstName,
						LastName = signup.LastName,
						CreatedAt = DateTime.Now,
						ModifiedAt = DateTime.Now,
						AddressLine1 = signup.Address,
						SystemUserId = systemUser.Id,
						UserStatusTypeId = 2,
						UserRoleId = signup.UserRole //1: Admin, 2: Caller, 3: Service Provider
					};
					_acountProfileRepository.AddOrUpdate(accountProfile);
					_acountProfileRepository.Save();

					if (signup.UserRole == 2)
					{
						var serviceProvider = new Entity.ServiceProvider()
						{
							CompanyName = signup.CompanyName,
							Address = signup.Address,
							CreatedAt = DateTime.Now,
							AccountProfileId = accountProfile.Id,
							CatId = signup.JobCategoryId
							// Category = signup.JobCategory
						};
						_serviceProviderRepository.AddOrUpdate(serviceProvider);
						_serviceProviderRepository.Save();
					}

					scope.Complete();

					userAccountDetails.ID = accountProfile.Id;
					userAccountDetails.FirstName = signup.FirstName;
					userAccountDetails.LastName = signup.LastName;
					userAccountDetails.Email = signup.Email;
					userAccountDetails.IsActive = true;
					userAccountDetails.Address = signup.Address;
					userAccountDetails.MobileNumber = signup.ContactNumber;

					return new ServiceResponse<UserAccountDetails>(userAccountDetails);
				}
			}
			catch (Exception ex)
			{
				return new ServiceResponse<UserAccountDetails>(null);
			}
		}
	}
}
