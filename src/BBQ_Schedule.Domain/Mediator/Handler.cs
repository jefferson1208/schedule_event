using BBQ_Schedule.Domain.Commands;
using BBQ_Schedule.Domain.Interfaces;
using MediatR;

namespace BBQ_Schedule.Domain.Mediator
{
    public class Handler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        public Handler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Command> SendCommandAsync(Command command,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
