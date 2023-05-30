using Newtonsoft.Json.Linq;

namespace BBQ_Schedule.UI.Web.Services
{
    public record Response
    {
        public bool Success { get; set; }
        public dynamic Data { get; set; }
        public List<string> Errors { get; set; }    

        public T ToObject<T>()
        {
            return Data.ToObject<T>();
        }

        public List<T> ToListObject<T>()
        {
            return Data.ToObject<List<T>>();
        }
    }
}
