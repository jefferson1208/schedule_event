using BBQ_Schedule.Domain.Commands;
using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Interfaces.Repositories;

namespace BBQ_Schedule.Application.Schedule
{
    public class ScheduleApplicationService : IScheduleApplicationService
    {
        private readonly IUser _user;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleApplicationService(IUser user, IMediatorHandler mediatorHandler,
            IScheduleRepository scheduleRepository)
        {
            _user = user;
            _mediatorHandler = mediatorHandler;
            _scheduleRepository = scheduleRepository;
        }
        public async Task<Guid> CreateNewEvent(DateOnly date, string description, string location, int capacity)
        {
            var command = new ScheduleCommand(date, description, location, capacity);

            var result = await _mediatorHandler.SendCommandAsync(command) as ScheduleCommand;

            return result.Id;
        }

        public async Task AddGuest(Guid eventId, string name, decimal contribuition, bool withDrink)
        {
            var command = new GuestCommand(eventId, name, contribuition, withDrink);

            await _mediatorHandler.SendCommandAsync(command);
        }

        public async Task<Domain.Models.Schedule> GetEventByIdAsync(Guid id)
        {
            return await _scheduleRepository.GetEventByIdAsync(id);
        }

        public async Task<Domain.Models.Schedule> GetEventWithGuestsByIdAsync(Guid id)
        {
            return await _scheduleRepository.GetEventWitGuestsByIdAsync(id);
        }

        public async Task<List<Domain.Models.Schedule>> GetEventsAsync()
        {
            return await _scheduleRepository.GetEventsAsync();
        }
    }
}
