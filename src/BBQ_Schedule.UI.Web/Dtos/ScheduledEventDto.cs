using BBQ_Schedule.UI.Web.Validations;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace BBQ_Schedule.UI.Web.Dtos
{
    public record ScheduledEventDto
    {
 
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TotalPeople { get; set; }
        public decimal TotalCollected { get; set; }
        public int Capacity { get; set; }
        public List<GuestDto> Guests { get; set; }
        public void AddPeople(decimal contribuition)
        {
            TotalPeople++;
            CalculateValueAdded(contribuition);
        }
        private void CalculateValueAdded(decimal contribuition) => TotalCollected += contribuition;
        public ValidationResult Validate()
        {
            return new ScheduleEventValidation().Validate(this);
        }
    }

    public record GuestDto
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public decimal Contribution { get; set; }
        public bool WithDrink { get; set; }
        public ValidationResult Validate()
        {
            return new InviteEventValidations().Validate(this);
        }
    }
}
