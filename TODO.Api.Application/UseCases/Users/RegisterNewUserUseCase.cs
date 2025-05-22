using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Application.Services;
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
        private readonly IImageService _imageService;

        public RegisterNewUserUseCase(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenService tokenService,
            IUserRepository userRepository,
            IImageService imageService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService;
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
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

            string pictureUrl = string.Empty;

            if (!string.IsNullOrEmpty(userDto.PictureUrlBase64))
            {
                try
                {
                    var imageResult = await _imageService.UploadFile(userDto.PictureUrlBase64);
                    if (imageResult == null || string.IsNullOrEmpty(imageResult.ImageUrl))
                    {
                        validationResult.AddError("User", "Error uploading image", "ImageUploadError");
                        return validationResult;
                    }

                    pictureUrl = imageResult.ImageUrl;
                }
                catch (Exception ex)
                {
                    validationResult.AddError("User", "Error saving image", ex.Message);
                    return validationResult;
                }
            }

            try
            {
                var todoUser = new User(user.Id, userDto.UserName, pictureUrl);
                var resultTodoUser = await _userRepository.Create(todoUser);
                if (!resultTodoUser)
                {
                    this.RevertUserRegister(user.UserName);
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

        private async Task<bool> RevertUserRegister(string userName)
        {
            var result = _userManager.DeleteAsync(_userManager.Users.FirstOrDefault(x => x.UserName == userName));
            return result.Result.Succeeded;
        }
    }
}
