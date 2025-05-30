using TODO.Api.Application.DTOs;
using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Application.UseCases.Categories
{
    public interface IGetCategoriesUseCase
    {
        Task<GetItemsResultDto<CategoryResume>> Process();
    }
}
