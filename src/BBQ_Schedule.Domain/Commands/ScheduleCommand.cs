using BBQ_Schedule.Domain.Validations.Commands;

namespace BBQ_Schedule.Domain.Commands
{
    public class ScheduleCommand : Command
    {
        public ScheduleCommand(DateOnly date, string description, string location, int capacity)
        {
            Date = date;
            Description = description;
            Location = location;
            Capacity = capacity;

            Validate();
        }

        private void Validate()
        {
            base.Validate(new ScheduleCommandValidations(), this);
        }

        public Guid Id { get; set; }
        public DateOnly Date { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public int Capacity { get; private set; }
    }
}
