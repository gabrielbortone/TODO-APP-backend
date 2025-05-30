using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Application.DTOs
{
    public class GetItemsResultDto<T> where T : IItemGet
    {
        public List<T> Data { get; set; }
        public PaginationResultDto Pagination { get; set; }
        public FinalValidationResultDto ValidationResult { get; set; }
    }
}
