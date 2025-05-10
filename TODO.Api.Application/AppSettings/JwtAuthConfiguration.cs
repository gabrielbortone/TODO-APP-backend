namespace TODO.Api.Application.AppSettings
{
    public class JwtAuthConfiguration
    {
        
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 0;
        public string Secret { get; set; } = string.Empty;
    }

}
