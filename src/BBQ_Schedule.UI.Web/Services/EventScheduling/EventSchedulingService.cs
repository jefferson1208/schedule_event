using BBQ_Schedule.UI.Web.Dtos;
using BBQ_Schedule.UI.Web.Extensions;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text;

namespace BBQ_Schedule.UI.Web.Services.EventScheduling
{
    public class EventSchedulingService : IEventSchedulingService
    {
        private static HttpClient _httpClient;
		private readonly IJSRuntime _jsRuntime;
		public EventSchedulingService(IHttpClientFactory httpClientFactory, IJSRuntime jsRuntime)
        {
            _httpClient = httpClientFactory.CreateClient("Schedule_Event");
			_jsRuntime = jsRuntime;

		}
        public async Task<Response> CreateEventAsync(ScheduledEventDto schedule)
        {

			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwt());

			var result = await _httpClient.PostAsync("/api/v1/schedule/new-event", 
                new StringContent(Util.ConvertToJson(schedule), Encoding.UTF8, "application/json"));

            var content = Util.ConvertFromJson<Response>(await result.Content.ReadAsStringAsync());

            return content;
        }

        public async Task<Response> InviteAsync(GuestDto guest)
        {

			_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwt());

			var result = await _httpClient.PostAsync("/api/v1/schedule/event/invite",
                new StringContent(Util.ConvertToJson(guest), Encoding.UTF8, "application/json"));

            var content = Util.ConvertFromJson<Response>(await result.Content.ReadAsStringAsync());

            return content;
        }
        public async Task<Response> GetEventAsync(string id)
        {
			_httpClient.DefaultRequestHeaders.Add("accept", "*/*");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwt());

			var result = await _httpClient.GetAsync($"/api/v1/schedule/event-details/{id}");

            var content = Util.ConvertFromJson<Response>(await result.Content.ReadAsStringAsync());

            return content;
        }

        public async Task<Response> GetEventsAsync()
        {
			_httpClient.DefaultRequestHeaders.Add("accept", "*/*");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwt());

			var result = await _httpClient.GetAsync($"/api/v1/schedule/events");

			var content = Util.ConvertFromJson<Response>(await result.Content.ReadAsStringAsync());

            return content;
        }
        
        private async Task<string> GetJwt()
        {
            var accessToken = await _jsRuntime.GetFromLocalStorage("accessToken");
			return accessToken;
		}
    }
}
