using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using TODO.Api.Application.AppSettings;
using TODO.Api.Application.DTOs;

namespace TODO.Api.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly MinioSettings _settings;
        private readonly TransferUtility _transferUtility;

        public ImageService(IOptions<MinioSettings> settings)
        {
            _settings = settings.Value;

            var config = new AmazonS3Config
            {
                ServiceURL = _settings.ServiceURL,
                ForcePathStyle = true
            };

            _s3Client = new AmazonS3Client(_settings.AccessKey, _settings.SecretKey, config);
            _transferUtility = new TransferUtility(_s3Client);
        }

        private Stream Base64ToStream(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            return new MemoryStream(bytes);
        }

        private string GetNewFileName()
        {
            return Guid.NewGuid().ToString() + ".png";
        }

        public async Task<ImageResultDto> UploadFile(string base64String)
        {
            try
            {
                var fileName = GetNewFileName();

                using (var stream = Base64ToStream(base64String))
                {
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = stream,
                        Key = fileName,
                        BucketName = _settings.BucketName,
                        ContentType = _settings.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    await _transferUtility.UploadAsync(uploadRequest);
                }

                return new ImageResultDto
                {
                    FileName = fileName,
                    ImageUrl = $"{_settings.ServiceURL}/{_settings.BucketName}/{fileName}"
                };

            }
            catch (Exception ex)
            {
                return new ImageResultDto
                {
                    FileName = string.Empty,
                    Errors = new List<FinalErrorDto> { new FinalErrorDto("PictureUrl", ex.Message, "ErrorPicture") }
                };
            }
        }

        public async Task<bool> DeleteFile(string fileName)
        {
            try
            {
                var response = await _transferUtility.S3Client.DeleteObjectAsync(_settings.BucketName, fileName);
                return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
