using BBQ_Schedule.Application.Schedule;
using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Services.Api.Controllers;
using BBQ_Schedule.Services.Api.Extensions;
using BBQ_Schedule.Services.Api.ViewModels.Schedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BBQ_Schedule.Services.Api.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/schedule")]
    public class ScheduleController : MainController
    {
        private readonly IScheduleApplicationService _scheduleApplicationService;
        public ScheduleController(INotifier notifier, IUser appUser,
            IScheduleApplicationService scheduleApplicationService) : base(notifier, appUser)
        {
            _scheduleApplicationService = scheduleApplicationService;
        }

        [HttpPost("new-event")]
        public async Task<ActionResult<ScheduleViewModel>> CreateEvent(ScheduleViewModel schedule)
        {
            if (!ModelState.IsValid) return CustomizeResponse(ModelState);

            var dateOnly = DateOnly.FromDateTime(schedule.Date);
            var result = await _scheduleApplicationService.CreateNewEvent(dateOnly,
                schedule.Description, schedule.Location, schedule.Capacity);

            schedule.Id = result;

            return CustomizeResponse(schedule);
        }

        [HttpPost("event/invite")]
        public async Task<ActionResult<GuestViewModel>> Invite(GuestViewModel guest)
        {
            if (!ModelState.IsValid) return CustomizeResponse(ModelState);

            await _scheduleApplicationService.AddGuest(guest.EventId, guest.Name,
                guest.Contribution, guest.WithDrink);

            return CustomizeResponse(guest);
        }

        [HttpGet("event-details/{id:guid}")]
        public async Task<ActionResult<ScheduleViewModel>> GetScheduleById(Guid id)
        {
            var schedule = await _scheduleApplicationService.GetEventWithGuestsByIdAsync(id);

            if (schedule is not null)
                return CustomizeResponse(schedule.ConvertModelToViewModel());

            AddError($"Nenhum agendamento encontrado para o id {id}");

            return CustomizeResponse();
        }

        [HttpGet("events")]
        public async Task<ActionResult<List<ScheduleViewModel>>> GetSchedules()
        {
            var schedules = await _scheduleApplicationService.GetEventsAsync();

            if (schedules is not null)
                return CustomizeResponse(schedules.ConvertModelToViewModel());

            AddError($"Nenhum evento agendado");

            return CustomizeResponse();
        }
    }
}
