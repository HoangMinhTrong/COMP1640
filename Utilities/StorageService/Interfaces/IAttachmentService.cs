using Microsoft.AspNetCore.Http;

namespace Utilities.StorageService.Interfaces
{
    public interface IAttachmentService
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
