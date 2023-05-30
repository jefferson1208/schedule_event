using BBQ_Schedule.Domain.Interfaces.Repositories;

namespace BBQ_Schedule.Domain.Models
{
    public class Schedule : Entity, IAggregationRoot
    {
        public Schedule(DateOnly date, string description, string location, int capacity)
        {
            Date = date.ToDateTime(TimeOnly.MinValue);
            Description = description;
            Location = location;
            Capacity = capacity;
            TotalCollected = 0;
            TotalPeople = 0;
        }

        protected Schedule() { }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public int Capacity { get; private set; }
        public int TotalPeople { get; set; }
        public decimal TotalCollected { get; set; }
        public IEnumerable<Guest> Guests { get; set; }

        public void AddGuest(Guest guest)
        {
            TotalPeople += 1;
            TotalCollected += guest.Contribution;
        }
    }
}
