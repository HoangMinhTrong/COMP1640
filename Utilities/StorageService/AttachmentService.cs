using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Utilities.StorageService.DTOs;
using Utilities.StorageService.Interfaces;

namespace Utilities.StorageService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAmazonS3 _client;
        private readonly S3Setting _s3Settings;

        public AttachmentService(IAmazonS3 client, IOptions<S3Setting> s3Settings)
        {
            _client = client;
            _s3Settings = s3Settings.Value;
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var request = new PutObjectRequest
            {
                BucketName = _s3Settings.BucketName,
                Key = Guid.NewGuid().ToString(),
                InputStream = formFile.OpenReadStream(),
            };

            var result = await _client.PutObjectAsync(request);
            return request.Key;
        }
    }
}
