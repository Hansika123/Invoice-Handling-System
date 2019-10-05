using InvoiceHandling.Business.Interfaces;
using InvoiceHandling.Business.Services;
using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;
using System.Web.Http;

namespace InvoiceHandling.API.Controllers
{
	public class AuthController : ApiController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		public AuthController() : this(new AuthService())
		{
		}

		[HttpPost]
		[Route("SignIn")]
		public ServiceResponse<UserAccountDetails> SignIn([FromBody]Signin singIn)
		{
			var user = _authService.SignIn(singIn).Entity;
			return new ServiceResponse<UserAccountDetails>(user);
		}


		[HttpPost]
		[Route("SignUp")]
		public ServiceResponse<UserAccountDetails> SignUp([FromBody]Signup signUp)
		{
			var user = _authService.SignUp(signUp).Entity;
			if (user == null)
			{
				return new ServiceResponse<UserAccountDetails>(null)
				{
					HasError = true,
					ErrorMessage = "User with " + signUp.Email + " email address already exist"
				};
			}
			return new ServiceResponse<UserAccountDetails>(user);
		}
	}
}
