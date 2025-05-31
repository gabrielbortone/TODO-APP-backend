using TODO.Api.Application.DTOs;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ITodoRepository _repository;
        public DashboardService(ITodoRepository todoRepository)
        {
            _repository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<ToDoGeneralResumeDto> GetDashboardAsync(string userId)
        {
            var (todoCreated, todoUpdated, todoCompleted, todoRemoved) = await _repository.GetDashboardAsync(userId);
            return new ToDoGeneralResumeDto(todoCreated, todoUpdated, todoCompleted, todoRemoved);
        }
    }
}
