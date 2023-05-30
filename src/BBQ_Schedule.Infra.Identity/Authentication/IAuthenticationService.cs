namespace BBQ_Schedule.Infra.Identity.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string password);
        Task<bool> Login(string email, string password);
        Task<(string AccessToken, double ExpiresIn)> GenerateJwt(string email);
    }
}
