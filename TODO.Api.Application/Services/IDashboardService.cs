using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.Services
{
    public interface IDashboardService
    {
        Task<ToDoGeneralResumeDto> GetDashboardAsync(string userId);
    }
}
