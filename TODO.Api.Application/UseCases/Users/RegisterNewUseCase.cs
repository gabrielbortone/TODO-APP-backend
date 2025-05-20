using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Domain.Entities;

namespace TODO.Api.Application.UseCases.Users
{
    public class RegisterNewUseCase : IRegisterNewUserUseCase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        public RegisterNewUseCase(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService;
        }
        public async Task<FinalValidationResultDto> Process(RegisterNewUserDto userDto)
        {
            var validationResult = new FinalValidationResultDto(true, new List<FinalErrorDto>());

            var user = new IdentityUser { UserName = userDto.UserName,  Email = userDto.Email,  };
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    validationResult.AddError(error.Code, error.Description, error.Code);
                }
                return validationResult;
            }

            // save and add image
            string pictureUrl = string.Empty;
            var todoUser = new User(userDto.UserName, pictureUrl);



            return validationResult;
        }
    }
}
