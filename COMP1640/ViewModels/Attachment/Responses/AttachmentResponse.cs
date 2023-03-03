namespace COMP1640.ViewModels.Attachment.Responses
{
    public class AttachmentResponse
    {

        public AttachmentResponse(string fileName, string presignedUrl)
        {
            FileName = fileName;
            PresignedUrl = presignedUrl;
        }

        public string FileName { get; set; }
        public string PresignedUrl { get; set; }
    }
}
