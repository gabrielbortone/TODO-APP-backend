using TODO.Api.Application.DTOs;
using TODO.Api.Domain.ResumeObject;
using TODO.Api.Infra.Repositories.Abstract;

namespace TODO.Api.Application.UseCases.Categories
{
    public class GetCategoriesUseCase : IGetCategoriesUseCase
    {
        private readonly ICategoryRepository _repository;

        public GetCategoriesUseCase(ICategoryRepository categoryRepository)
        {
            _repository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }


        public async Task<GetItemsResultDto<CategoryResume>> Process()
        {
            var result = new GetItemsResultDto<CategoryResume>();
            result.ValidationResult = new FinalValidationResultDto();

            try
            {
                var categories = await _repository.GetAllAsync();
                result.Data = categories.ToList();
                result.Pagination = new PaginationResultDto(1, result.Data.Count);
                result.ValidationResult.IsValid = true;

                return result;
            }
            catch (Exception ex)
            {
                result.ValidationResult.AddError("GetCategoriesUseCase", "We are having some trouble, please wait a moment ant try again", "GetCategoriesError");
                return result;
            }
        }
    }
}
