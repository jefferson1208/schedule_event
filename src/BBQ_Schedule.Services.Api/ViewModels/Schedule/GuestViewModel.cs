using System.ComponentModel.DataAnnotations;

namespace BBQ_Schedule.Services.Api.ViewModels.Schedule
{
    public record GuestViewModel
    {
        [Required(ErrorMessage = "O Id do evento é obrigatório")]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "O nome do convidado é obrigatório")]
        [StringLength(30, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "A contribuição da pessoa é obrigatória")]
        public decimal Contribution { get; set; }
        public bool WithDrink { get; set; }
    }
}
