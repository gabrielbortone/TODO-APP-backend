namespace TODO.Api.Application.DTOs
{
    public class JwtToken
    {
        public string Token { get; set; }
        public JwtToken(string token)
        {
            Token = token;
        }
        public JwtToken() { }
    }
}
