using BBQ_Schedule.Domain.Commands;

namespace BBQ_Schedule.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task<Command> SendCommandAsync(Command command, CancellationToken cancellationToken = default);
    }
}
