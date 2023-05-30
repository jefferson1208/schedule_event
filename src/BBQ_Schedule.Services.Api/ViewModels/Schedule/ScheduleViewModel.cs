using System.ComponentModel.DataAnnotations;

namespace BBQ_Schedule.Services.Api.ViewModels.Schedule
{
    public record ScheduleViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A data do evento é obrigatória")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "A descrição do evento é obrigatória")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O local do evento é obrigatório")]
        [StringLength(250, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 10)]
        public string Location { get; set; }

        [Required(ErrorMessage = "A capacidade total de pessoas")]
        public int Capacity { get; set; }
        public int TotalPeople { get; set; }
        public decimal TotalCollected { get; set; }
        public List<GuestViewModel> Guests { get; set; }
    }
}
