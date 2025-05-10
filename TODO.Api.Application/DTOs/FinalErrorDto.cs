namespace TODO.Api.Application.DTOs
{
    public class FinalErrorDto
    {
        public FinalErrorDto(string property, string errorMessage, string errorCode)
        {
            Property = property;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public string Property { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }   
    }
}
