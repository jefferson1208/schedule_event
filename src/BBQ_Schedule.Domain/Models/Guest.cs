namespace BBQ_Schedule.Domain.Models
{
    public class Guest : Entity
    {
        public string Name { get; private set; }
        public decimal Contribution { get; private set; }
        public bool WithDrink { get; private set; }


        protected Guest() { }

        public Guest(string name, decimal contribution, bool withDrink, Guid scheduleId)
        {
            Name = name;
            Contribution = contribution;
            WithDrink = withDrink;
            ScheduleId = scheduleId;
        }

        /* EF Relations */
        public Guid ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
