using BBQ_Schedule.Domain.Models;
using BBQ_Schedule.Services.Api.ViewModels.Schedule;

namespace BBQ_Schedule.Services.Api.Extensions
{
    public static class ScheduleExtensions
    {
        public static ScheduleViewModel ConvertModelToViewModel(this Schedule model)
        {
            var schedule = new ScheduleViewModel
            {
                Id = model.Id,
                Date = model.Date,
                Description = model.Description,
                Location = model.Location,
                Capacity = model.Capacity,
                TotalPeople = model.TotalPeople,
                TotalCollected = model.TotalCollected,
            };

            if (model.Guests is not null)
            {
                schedule.Guests = new List<GuestViewModel>(1);

                foreach (var guest in model.Guests)
                {
                    schedule.Guests.Add(new GuestViewModel
                    {
                        EventId = model.Id,
                        Name = guest.Name,
                        Contribution = guest.Contribution,
                        WithDrink = guest.WithDrink
                    });

                    schedule.Guests.Capacity += 1;
                }
            }

            return schedule;


        }

        public static List<ScheduleViewModel> ConvertModelToViewModel(this List<Schedule> models)
        {
            return models
                .Select(_ =>
                        new ScheduleViewModel
                        {
                            Id = _.Id,
                            Date = _.Date,
                            Description = _.Description,
                            Location = _.Location,
                            Capacity = _.Capacity,
                            TotalCollected = _.TotalCollected,
                            TotalPeople = _.TotalPeople
                        })
                .ToList();
        }
    }
}
