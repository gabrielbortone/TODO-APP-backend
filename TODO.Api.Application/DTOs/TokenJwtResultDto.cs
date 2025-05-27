namespace TODO.Api.Application.DTOs
{
    public class TokenJwtResultDto
    {
        public TokenJwtResultDto(string userId, string userName, string? pictureUrl, string token)
        {
            UserId = userId;
            UserName = userName;
            PictureUrl = pictureUrl;
            Token = token;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string? PictureUrl { get; set; }
        public string Token { get; set; }
    }
}
