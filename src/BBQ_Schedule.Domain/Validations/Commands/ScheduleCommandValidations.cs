using BBQ_Schedule.Domain.Commands;
using FluentValidation;

namespace BBQ_Schedule.Domain.Validations.Commands
{
    public class ScheduleCommandValidations : AbstractValidator<ScheduleCommand>
    {
        public ScheduleCommandValidations()
        {
            RuleFor(s => s.Date)
                .NotNull().WithMessage("A data do evento deve ser informada")
                .NotEmpty().WithMessage("A data do evento deve ser informada")
                .GreaterThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("A data do evento deve ser maior que hoje");

            RuleFor(s => s.Capacity)
                .GreaterThan(1).WithMessage("A quantidade máxima de pessoas deve ser maior que 1");

            RuleFor(s => s.Location)
                .NotNull().WithMessage("O local do evento deve ser informado")
                .NotEmpty().WithMessage("O local do evento deve ser informado");

            RuleFor(s => s.Description)
                .NotNull().WithMessage("A descrição do evento deve ser informada")
                .NotEmpty().WithMessage("A descrição do evento deve ser informada");
        }
    }
}
