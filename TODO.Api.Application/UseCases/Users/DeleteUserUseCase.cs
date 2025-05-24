using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Users
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public DeleteUserUseCase(
            IUserRepository userRepository,
            UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<FinalValidationResultDto> Process(string id)
        {
            var resultValidation = new FinalValidationResultDto();
            try
            {
                await _userRepository.Delete(id);
                var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        resultValidation.AddError("Id", "Error", error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                resultValidation.AddError("Id", "Error", ex.Message);
            }

            return resultValidation;
        }
    }
}
