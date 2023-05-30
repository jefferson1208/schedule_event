namespace BBQ_Schedule.UI.Web
{
    public static class Util
    {
        public static T ConvertFromJson<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

		public static string ConvertToJson<T>(T @object)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(@object);
		}
	}
}
