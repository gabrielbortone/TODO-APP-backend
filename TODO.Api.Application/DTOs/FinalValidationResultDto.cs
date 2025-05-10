namespace TODO.Api.Application.DTOs
{
    public class FinalValidationResultDto
    {
        public bool IsValid { get; set; }
        public List<FinalErrorDto> Errors { get; set; }
        public FinalValidationResultDto(bool isValid, List<FinalErrorDto> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
        public FinalValidationResultDto()
        {
            IsValid = true;
            Errors = new List<FinalErrorDto>();
        }
        public void AddError(string property, string errorMessage, string errorCode)
        {
            Errors.Add(new FinalErrorDto(property, errorMessage, errorCode));
            IsValid = false;
        }
    }
}
