using InvoiceHandling.Common.EntityWrapper;
using InvoiceHandling.Models;

namespace InvoiceHandling.Business.Interfaces
{
	public interface IAuthService
	{
		ServiceResponse<UserAccountDetails> SignIn(Signin signin);

		ServiceResponse<UserAccountDetails> SignUp(Signup signup);
	}
}
