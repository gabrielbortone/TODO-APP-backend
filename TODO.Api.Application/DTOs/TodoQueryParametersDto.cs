namespace TODO.Api.Application.DTOs
{
    public class TodoQueryParametersDto
    {
        public string Search { get; set; }
        public string OrderBy { get; set; } = "Title";
        public string OrderDirection { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 10;
        public int? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IncludeCompleted { get; set; }

    }
}
