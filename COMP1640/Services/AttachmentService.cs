using Domain;
using Domain.Interfaces;
using Utilities.StorageService.Interfaces;

namespace COMP1640.Services
{
    public class AttachmentService
    {
        private readonly IS3Service _s3Service;
        private readonly IAttachmentRepository _attachmentRepo;

        public AttachmentService(IS3Service s3Service
            , IAttachmentRepository attachmentRepo)
        {
            _s3Service = s3Service;
            _attachmentRepo = attachmentRepo;
        }


        public async Task<Attachment> UploadAsync(IFormFile formFile)
        {
            var fileKey = await _s3Service.UploadAsync(formFile);
            var attachment = new Attachment(formFile, fileKey);

            await _attachmentRepo.InsertAsync(attachment);
            return attachment;
        }

        public async Task<List<Attachment>> UploadListAsync(List<IFormFile> formFiles)
        {
            var attachments = new List<Attachment>();
            foreach (var formFile in formFiles)
            {
                var fileKey = await _s3Service.UploadAsync(formFile);
                attachments.Add(new Attachment(formFile, fileKey));
            }

            await _attachmentRepo.InsertRangeAsync(attachments);
            return attachments;
        }
    }
}
