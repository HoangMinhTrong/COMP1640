using Microsoft.AspNetCore.Http;

namespace Utilities.StorageService.Interfaces
{
    public interface IS3Service
    {
        Task<string> UploadAsync(IFormFile formFile);
        Task<Stream> GetAsync(string keyName);
        Task<string> GetPresignedUrl(string keyName);
    }
}
