using BBQ_Schedule.UI.Web.Dtos;

namespace BBQ_Schedule.UI.Web.Services.Authentication
{
	public interface IAuthorizationService
	{
		Task Login(string accessToken);
		Task<HttpResponseMessage> Login(LoginDto login);
		Task Logout();
	}
}
