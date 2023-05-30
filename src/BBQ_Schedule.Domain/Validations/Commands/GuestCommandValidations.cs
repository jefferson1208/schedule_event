using BBQ_Schedule.Domain.Commands;
using FluentValidation;

namespace BBQ_Schedule.Domain.Validations.Commands
{
    public class GuestCommandValidations : AbstractValidator<GuestCommand>
    {
        public GuestCommandValidations()
        {
            RuleFor(x => x.EventId)
                .NotNull().WithMessage("O id do evento é obrigatório")
                .NotEqual(Guid.Empty).WithMessage("O id do evento é obrigatório");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("O nome do convidado é obriagório")
                .NotEmpty().WithMessage("O nome do convidado é obriagório")
                .MinimumLength(5).WithMessage("O nome deve ter no minimo 5 caracteres")
                .MaximumLength(30).WithMessage("O nome do convidado deve ter no maximo 30 caracteres");

            RuleFor(x => x.Contribution)
                .GreaterThanOrEqualTo(50).WithMessage("O valor da contribuição dever ser igual ou mair que 50");
        }
    }
}
