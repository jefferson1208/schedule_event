using BBQ_Schedule.UI.Web.Dtos;
using FluentValidation;

namespace BBQ_Schedule.UI.Web.Validations
{
    public class ScheduleEventValidation: AbstractValidator<ScheduledEventDto>
    {
        public ScheduleEventValidation()
        {
            RuleFor(s => s.Date)
                .NotNull().WithMessage("A data do evento deve ser informada");

            RuleFor(s => s.Capacity)
                .GreaterThan(1).WithMessage("A quantidade máxima de pessoas deve ser maior que 1");

            RuleFor(s => s.Location)
                .NotNull().WithMessage("O local do evento deve ser informado")
                .NotEmpty().WithMessage("O local do evento deve ser informado");
        }

        private DateTime ConvertStringToDate(string value)
        {
            DateTime.TryParse(value, out var date);

            return date;
        }
    }
}
