﻿namespace TODO.Api.Application.DTOs
{
    public class UpdateUserDto
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PictureBase64 { get; set; }
    }
}
