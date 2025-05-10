namespace TODO.Api.Application.DTOs
{
    public class TokenJwtResultDto
    {
        public TokenJwtResultDto(string userName, string? pictureUrl, string token)
        {
            UserName = userName;
            PictureUrl = pictureUrl;
            Token = token;
        }

        public string UserName { get; set; }
        public string? PictureUrl { get; set; }
        public string Token { get; set; }
    }
}
