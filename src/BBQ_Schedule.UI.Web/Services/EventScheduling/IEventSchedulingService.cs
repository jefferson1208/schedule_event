using BBQ_Schedule.UI.Web.Dtos;

namespace BBQ_Schedule.UI.Web.Services.EventScheduling
{
	public interface IEventSchedulingService
	{
		Task<Response> InviteAsync(GuestDto guest);
		Task<Response> GetEventAsync(string id);
		Task<Response> GetEventsAsync();
		Task<Response> CreateEventAsync(ScheduledEventDto schedule);
	}
}
