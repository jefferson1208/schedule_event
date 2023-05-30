using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BBQ_Schedule.Domain.Commands
{
    public abstract class Command : IRequest<Command>
    {
        public ValidationResult ValidationResult { get; private set; }
        public bool IsValid => ValidationResult.IsValid;
        protected Command()
        {
            ValidationResult = new();
        }

        protected virtual bool Validate<TV, TE>(TV validator, TE validate) where TV : AbstractValidator<TE> where TE: Command
        {
            var validation = validator.Validate(validate);

            ValidationResult = validation;

            return validation.IsValid;
        }
    }
}
