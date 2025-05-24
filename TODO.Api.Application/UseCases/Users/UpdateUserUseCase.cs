using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Application.Services;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Users
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IImageService _imageService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UpdateUserUseCase(
            IUserRepository userRepository, 
            UserManager<IdentityUser> userManager, 
            IImageService imageService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _imageService = imageService;
        }

        public async Task<FinalValidationResultDto> Process(UpdateUserDto updateUser)
        {
            var validationResult = new FinalValidationResultDto();
            var user = await _userRepository.GetByIdentityUserId(updateUser.IdentityId);
            var identityUser = await _userManager.FindByIdAsync(updateUser.IdentityId);

            if (user == null || identityUser == null)
            {
                validationResult.AddError("User", "User not found", "UserNotFound");
                return validationResult;
            }

            if (!string.IsNullOrEmpty(updateUser.PictureBase64))
            {
                var imageResult = await _imageService.UploadFile(updateUser.PictureBase64);
                if (imageResult.Errors.Any())
                {
                    validationResult.AddError("Image", "Image upload failed", "ImageUploadFailed");
                    return validationResult;
                }
            }

            if (!string.IsNullOrEmpty(updateUser.Email) && updateUser.Email != identityUser.Email)
            {
                var emailExists = await _userManager.FindByEmailAsync(updateUser.Email);
                if (emailExists != null)
                {
                    validationResult.AddError("Email", "Email already exists", "EmailAlreadyExists");
                    return validationResult;
                }
                else
                {
                    var emailChangedResult = await _userManager.ChangeEmailAsync(identityUser, updateUser.Email, null);
                    if (!emailChangedResult.Succeeded)
                    {
                        validationResult.AddError("Email", "Email change failed", "EmailChangeFailed");
                        return validationResult;
                    }
                }
            }

            if (!string.IsNullOrEmpty(updateUser.UserName) && updateUser.UserName != identityUser.UserName)
            {
                var userNameExists = await _userManager.FindByNameAsync(updateUser.UserName);
                if (userNameExists != null)
                {
                    validationResult.AddError("UserName", "UserName already exists", "UserNameAlreadyExists");
                    return validationResult;
                }
                else
                {
                    var userNameChangedResult = await _userManager.SetUserNameAsync(identityUser, updateUser.UserName);
                    if (!userNameChangedResult.Succeeded)
                    {
                        validationResult.AddError("UserName", "UserName change failed", "UserNameChangeFailed");
                        return validationResult;
                    }
                }
            }

            if ((!string.IsNullOrEmpty(updateUser.FirstName) && updateUser.FirstName != user.FirstName)
                || (!string.IsNullOrEmpty(updateUser.LastName) && updateUser.LastName != user.LastName))
            {
                user.ChangeName(updateUser.FirstName, updateUser.LastName);
                var userNameChangedResult = await _userRepository.Update(user);
                if (!userNameChangedResult)
                {
                    validationResult.AddError("UserName", "UserName change failed", "UserNameChangeFailed");
                    return validationResult;
                }
            }

            return validationResult;
        }
    }
}
