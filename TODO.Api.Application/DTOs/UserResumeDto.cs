using Microsoft.AspNetCore.Identity;
using TODO.Api.Domain.Entities;

namespace TODO.Api.Application.DTOs
{
    public class UserResumeDto
    {
        public string IdentityUserId { get; private set; }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PictureUrl { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public ToDoGeneralResumeDto ToDoGeneralResumeDto { get; private set; }

        public void SetToDoGeneralResumeDto(ToDoGeneralResumeDto toDoGeneralResumeDto)
        {
            ToDoGeneralResumeDto = toDoGeneralResumeDto;
        }
        public static UserResumeDto FromUser(User user, IdentityUser identityUser)
        {
            return new UserResumeDto
            {
                IdentityUserId = user.IdentityUserId,
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PictureUrl = user.PictureUrl,
                UserName = identityUser.UserName,
                Email = identityUser.Email,
                ToDoGeneralResumeDto = new ToDoGeneralResumeDto(0, 0, 0, 0)
            };
        }
    }
}
