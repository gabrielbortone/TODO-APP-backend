using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.Services
{
    public interface IImageService
    {
        Task<ImageResultDto> UploadFile(string base64String);
        Task<bool> DeleteFile(string fileName);
    }
}
