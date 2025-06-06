﻿using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.UseCases.Users
{
    public interface IRegisterNewUserUseCase
    {
        Task<(FinalValidationResultDto, UserResumeDto)> Process(RegisterNewUserDto registerNewUserDto);
    }
}
