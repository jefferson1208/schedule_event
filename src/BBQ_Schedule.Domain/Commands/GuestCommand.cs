using BBQ_Schedule.Domain.Validations.Commands;

namespace BBQ_Schedule.Domain.Commands
{
    public class GuestCommand : Command
    {
        public GuestCommand(Guid eventId, string name, decimal contribution, bool withDrink)
        {
            EventId = eventId;
            Name = name;
            Contribution = contribution;
            WithDrink = withDrink;

            Validate();
        }

        private void Validate()
        {
            base.Validate(new GuestCommandValidations(), this);
        }
        public Guid EventId { get; private set; }
        public string Name { get; private set; }
        public decimal Contribution { get; private set; }
        public bool WithDrink { get; private set; }
    }
}
