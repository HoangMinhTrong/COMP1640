﻿using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

namespace Utilities.StorageService.Interfaces
{
    public interface IS3Service
    {
        Task<GetObjectResponse> GetAsync(string keyName);
        Task<DeleteObjectResponse> DeleteAsync(string keyName);
        Task<string> UploadAsync(IFormFile formFile);
        Task<string> GetPresignedUrl(string keyName);
    }
}
