using BBQ_Schedule.UI.Web.Dtos;
using FluentValidation;

namespace BBQ_Schedule.UI.Web.Validations
{
    public class InviteEventValidations : AbstractValidator<GuestDto>
    {
        public InviteEventValidations()
        {
            RuleFor(g => g.Name)
                .NotNull().WithMessage("O nome do convidado deve ser informado")
                .NotEmpty().WithMessage("O nome do convidado deve ser informado")
                .MinimumLength(5).WithMessage("O nome do convidado deve ter pelo menos 5 caracteres");

            RuleFor(g => g.Contribution)
                .GreaterThanOrEqualTo(50).WithMessage("O valor mínimo da contribuição é de 50 R$");
        }
    }
}
