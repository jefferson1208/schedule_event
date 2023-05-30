namespace BBQ_Schedule.Services.Api.ViewModels.User
{
    public record LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
    }
}
