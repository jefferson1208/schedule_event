namespace BBQ_Schedule.Infra.Identity
{
    public record SecuritySettings
    {
        public string Key { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}
