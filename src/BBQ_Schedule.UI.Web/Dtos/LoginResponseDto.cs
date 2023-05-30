namespace BBQ_Schedule.UI.Web.Dtos
{
	public record LoginResponseDto
	{
		public string AccessToken { get; set; }
		public double ExpiresIn { get; set; }
	}
}
