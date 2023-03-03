using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Utilities.StorageService.DTOs;
using Utilities.StorageService.Interfaces;

namespace Utilities.StorageService
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly S3Setting _s3Settings;

        public S3Service(IAmazonS3 client, IOptions<S3Setting> s3Settings)
        {
            _client = client;
            _s3Settings = s3Settings.Value;
        }

        public async Task<GetObjectResponse> GetAsync(string keyName)
        {
            var result = await _client.GetObjectAsync(_s3Settings.BucketName, keyName);
            return result;
        }

        public async Task<DeleteObjectResponse> DeleteAsync(string keyName)
        {
            var result = await _client.DeleteObjectAsync(_s3Settings.BucketName, keyName);
            return result;
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var request = new PutObjectRequest
            {
                BucketName = _s3Settings.BucketName,
                Key = Guid.NewGuid().ToString(),
                InputStream = formFile.OpenReadStream(),
                ContentType = formFile.ContentType,
            };
            request.Metadata.Add("FileName", formFile.FileName);

            await _client.PutObjectAsync(request);
            return request.Key;
        }

        public async Task<string> GetPresignedUrl(string keyName)
        {
            var request = new GetPreSignedUrlRequest()
            {
                BucketName = _s3Settings.BucketName,
                Key = keyName,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };

           return _client.GetPreSignedURL(request);
        }
    }
}
