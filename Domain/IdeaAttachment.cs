namespace Domain
{
    public class IdeaAttachment
    {
        public IdeaAttachment()
        {

        }

        public int IdeaId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Attachment Attachment { get; set; }
        public virtual Idea Idea { get; set; }
    }
}
