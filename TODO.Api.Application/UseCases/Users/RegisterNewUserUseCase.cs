using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Domain.Entities;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Users
{
    public class RegisterNewUserUseCase : IRegisterNewUserUseCase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public RegisterNewUserUseCase(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

            try
            {
                var todoUser = new User(user.Id, userDto.UserName, pictureUrl);
                var resultTodoUser = await _userRepository.Create(todoUser);
                if (!resultTodoUser)
                {
                    throw new Exception("Error creating user in database");
                }
            }
            catch (Exception ex)
            {
                validationResult.AddError("User", "Error creating user", ex.Message);
                return validationResult;
            }




            return validationResult;
        }
    }
}
