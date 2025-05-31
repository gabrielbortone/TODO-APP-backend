using System.ComponentModel.DataAnnotations;

namespace TODO.Api.Application.DTOs
{
    public class CreateToDoItemRequestDto
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; }
        
        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }
        
        [Required]
        [Range(1, 3, ErrorMessage = "Priority must be between 1 and 3.")]
        public int Priority { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
