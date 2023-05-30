namespace BBQ_Schedule.Application.Schedule
{
    public interface IScheduleApplicationService
    {
        Task<Guid> CreateNewEvent(DateOnly date,
            string description, string location, int capacity);
        Task AddGuest(Guid eventId, string name, decimal contribuition, bool withDrink);
        Task<Domain.Models.Schedule> GetEventByIdAsync(Guid id);
        Task<Domain.Models.Schedule> GetEventWithGuestsByIdAsync(Guid id);
        Task<List<Domain.Models.Schedule>> GetEventsAsync();
    }
}
