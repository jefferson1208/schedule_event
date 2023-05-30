using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Models;
using FluentValidation.Results;

namespace BBQ_Schedule.Domain.Handlers
{
    public abstract class CommandHandler
    {
        private readonly INotifier _notifier;
        public CommandHandler(INotifier notifier)
        {
            _notifier = notifier;
        }
        protected virtual async Task CommitAsync(IUnitOfWork unitOfWork)
        {
            var save =  await unitOfWork.CommitAsync();

            if (!save)
                Notify("Falha ao adicionar evento");
        }

        protected virtual void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected virtual void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
