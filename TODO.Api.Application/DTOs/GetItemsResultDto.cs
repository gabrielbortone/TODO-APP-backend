using TODO.Api.Domain.ResumeObject;

namespace TODO.Api.Application.DTOs
{
    public class GetItemsResultDto<T> where T : IItemGet
    {
        public List<T> Data { get; private set; }
        public PaginationResultDto Pagination { get; private set; }
        public FinalValidationResultDto ValidationResult { get; private set; }

        public void SetPagination(int totalPages, int totalItems, int pageIndex = 1, int itemsPerPage = 10)
        {
            Pagination = new PaginationResultDto(totalPages, totalItems, pageIndex, itemsPerPage);
        }

        public void SetData(IList<T> data)
        {
            Data = data?.ToList() ?? new List<T>();
        }
    }
}
