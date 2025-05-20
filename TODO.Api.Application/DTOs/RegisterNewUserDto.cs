namespace TODO.Api.Application.DTOs
{
    public class RegisterNewUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string? PictureUrlBase64 { get; set; }
    }
}
