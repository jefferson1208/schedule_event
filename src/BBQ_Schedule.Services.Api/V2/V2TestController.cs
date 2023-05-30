using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BBQ_Schedule.Services.Api.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/test")]
    public class V2TestController : MainController
    {
        public V2TestController(INotifier notifier, IUser appUser) : base(notifier, appUser) { }

        [HttpGet]
        public string CheckV2() => "V2 está funcionando";
    }
}
