namespace TODO.Api.Application.DTOs
{
    public class PaginationResultDto
    {
        public PaginationResultDto(int totalPages, int totalItems, int pageIndex=1, int itemsPerPage = 10)
        {
            PageIndex = pageIndex;
            TotalPages = totalPages;
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
        }

        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; } = 10;
    }
}
