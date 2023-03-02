using COMP1640.ViewModels.Attachment.Responses;
using Domain;
using Domain.Interfaces;
using Utilities.StorageService.Interfaces;

namespace COMP1640.Services
{
    public class AttachmentService
    {
        private readonly IS3Service _s3Service;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly IIdeaRepository _ideaRepo;

        public AttachmentService(IS3Service s3Service
            , IAttachmentRepository attachmentRepo
            , IIdeaRepository ideaRepo)
        {
            _s3Service = s3Service;
            _attachmentRepo = attachmentRepo;
            _ideaRepo = ideaRepo;
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

        public async Task<List<AttachmentResponse>> GetAttachmentsAsync(int ideaId)
        {
            var idea = await _ideaRepo.GetAsync(ideaId);
            if(idea == null || !idea.IdeaAttachments.Any())
                return null;

            var attachmentsResponse = new List<AttachmentResponse>();
            foreach (var ideaAttachment in idea.IdeaAttachments)
            {
                var presignedUrl = await _s3Service.GetPresignedUrl(ideaAttachment.Attachment.KeyName);
                attachmentsResponse.Add(new AttachmentResponse(ideaAttachment.Attachment.Name, presignedUrl));
            }

            return attachmentsResponse;
        }
    }
}
