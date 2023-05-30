using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BBQ_Schedule.Services.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        public readonly IUser AppUser;
        protected Guid UserId { get; set; }
        protected bool IsAuthenticated { get; set; }
        protected MainController(INotifier notifier,
                                 IUser appUser)
        {
            _notifier = notifier;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                IsAuthenticated = true;
            }
        }

        protected bool IsValid() => !_notifier.HasNotification();
        protected ActionResult CustomizeResponse(object result = null)
        {
            if (IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected ActionResult CustomizeResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) AddError(modelState);
            return CustomizeResponse();
        }

        protected void AddError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in errors)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AddError(errorMsg);
            }
        }

        protected void AddError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
