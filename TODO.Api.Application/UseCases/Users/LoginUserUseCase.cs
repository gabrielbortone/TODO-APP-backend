﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using TODO.Api.Application.AppSettings;
using TODO.Api.Application.DTOs;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Users
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly JwtAuthConfiguration _config;
        private readonly UserManager<IdentityUser<string>> _userManager;
        private readonly SignInManager<IdentityUser<string>> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        public LoginUserUseCase(
            IOptions<JwtAuthConfiguration> options,
            UserManager<IdentityUser<string>> userManager,
            SignInManager<IdentityUser<string>> signInManager,
            ITokenService tokenService,
            IUserRepository userRepository)
        {
            _config = options.Value;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService;
            _userRepository = userRepository as IUserRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<(FinalValidationResultDto, TokenJwtResultDto)> Process(string userName, string password)
        {
            var validationResult = new FinalValidationResultDto(false, new List<FinalErrorDto>());

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                validationResult.AddError("UserName", "UserName and password are required", "UserNameAndPasswordRequired");
                return (validationResult, null);
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                validationResult.AddError("UserName", "User not found", "UserNotFound");
                return (validationResult, null);
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
            {
                switch (result.IsLockedOut)
                {
                    case true:
                        validationResult.AddError("UserName", "User is locked out", "UserLockedOut");
                        break;
                    case false:
                        validationResult.AddError("UserName", "Invalid password", "InvalidPassword");
                        break;
                }

                return (validationResult, null);
            }

            var identityUserid = user.Id;

            var userProfile = await _userRepository.GetByIdentityUserId(identityUserid);

            if (validationResult.Errors.Any())
            {
                return (validationResult, new TokenJwtResultDto("", "", "", ""));
            }

            var token = _tokenService.GenerateToken(user.UserName, identityUserid);

            if(string.IsNullOrEmpty(token))
            {
                validationResult.AddError("Token", "Invalid Token", "InvalidToken");
                return (validationResult, new TokenJwtResultDto("", "", "", ""));
            }

            validationResult.IsValid = true;

            return (validationResult, new TokenJwtResultDto(identityUserid, user.UserName, userProfile.PictureUrl, token));
        }
    }
}
