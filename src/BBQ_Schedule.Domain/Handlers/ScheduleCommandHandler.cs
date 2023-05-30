using BBQ_Schedule.Domain.Commands;
using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Interfaces.Repositories;
using BBQ_Schedule.Domain.Models;
using MediatR;

namespace BBQ_Schedule.Domain.Handlers
{
    public class ScheduleCommandHandler : CommandHandler,
        IRequestHandler<ScheduleCommand, Command>,
        IRequestHandler<GuestCommand, Command>
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleCommandHandler(IScheduleRepository scheduleRepository, INotifier notifier) : base(notifier) 
        {
            _scheduleRepository = scheduleRepository;
        }
        public async Task<Command> Handle(ScheduleCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid)
            {
                Notify(command.ValidationResult);
                return await Task.FromResult(command);
            }

            var schedule = new Schedule(command.Date, command.Description, command.Location, command.Capacity);

            _scheduleRepository.AddEvent(schedule);

            await CommitAsync(_scheduleRepository.UnitOfWork);

            command.Id = schedule.Id;

            return await Task.FromResult(command);
        }

        public async Task<Command> Handle(GuestCommand command, CancellationToken cancellationToken)
        {
            if (!command.IsValid)
            {
                Notify(command.ValidationResult);
                return await Task.FromResult(command);
            }

            var schedule = await _scheduleRepository.GetEventByIdAsync(command.EventId);

            if(schedule is null)
            {
                Notify($"Evento {command.EventId} não encontrado");
                return await Task.FromResult(command);
            }

            var guest = new Guest(command.Name, command.Contribution, command.WithDrink, command.EventId);

            schedule.AddGuest(guest);

            _scheduleRepository.UpdateEvent(schedule);
            _scheduleRepository.AddGuest(guest);

            await CommitAsync(_scheduleRepository.UnitOfWork);

            return await Task.FromResult(command);
        }
    }
}
