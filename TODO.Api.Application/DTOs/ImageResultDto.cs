namespace TODO.Api.Application.DTOs
{
    public class ImageResultDto
    {
        public string FileName { get; set; }
        public string? ImageUrl { get; set; }
        public List<FinalErrorDto> Errors { get; set; }
    }
}
