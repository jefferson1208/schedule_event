using BBQ_Schedule.Domain.Models;

namespace BBQ_Schedule.Domain.Interfaces.Repositories
{
    public interface IScheduleRepository: IRepository<Schedule>
    {
        void AddEvent(Schedule schedule);
        void UpdateEvent(Schedule schedule);
        void AddGuest(Guest guest);
        Task<Schedule> GetEventByDateAsync(DateOnly date);
        Task<Schedule> GetEventByIdAsync(Guid id);
        Task<List<Schedule>> GetEventsAsync();
        Task<Schedule> GetEventWitGuestsByIdAsync(Guid id);
        void RemoveEvent(Schedule schedule);
    }
}
