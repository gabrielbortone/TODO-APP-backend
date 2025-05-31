using Microsoft.AspNetCore.Identity;
using TODO.Api.Application.DTOs;
using TODO.Api.Application.Services;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Users
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser<string>> _userManager;
        private readonly IDashboardService _dashboardService;

        public GetUserUseCase(IUserRepository userRepository, 
            UserManager<IdentityUser<string>> userManager,
            IDashboardService dashboardService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
        }

        public async Task<(FinalValidationResultDto, UserResumeDto)> Process(string userId)
        {
            var validationResult = new FinalValidationResultDto(true, new List<FinalErrorDto>());

            try
            {
                var user = await _userRepository.GetByIdentityUserId(userId);
                var identityUser = await _userManager.FindByIdAsync(userId);

                if (user == null || identityUser == null)
                {
                    validationResult.AddError("User", "User not found", "UserNotFound");
                    return (validationResult, null);
                }

                var userResume = UserResumeDto.FromUser(user, identityUser);

                return (validationResult, userResume);
            }
            catch (Exception ex)
            {
                validationResult.AddError("User", ex.Message, "UserNotFound");
                return (validationResult, null);
            }
        }
    }
}
