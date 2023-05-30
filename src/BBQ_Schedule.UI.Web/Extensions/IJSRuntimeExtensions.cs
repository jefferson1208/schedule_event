using Microsoft.JSInterop;

namespace BBQ_Schedule.UI.Web.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
            => js.InvokeAsync<object>("localStorage.setItem", key, content);

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>("localStorage.getItem", key);

        public static ValueTask<string> RemoveItemFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>("localStorage.removeItem", key);
    }
}
