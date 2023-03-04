namespace Domain
{
    public class IdeaAttachment
    {
        public IdeaAttachment()
        {

        }

        public IdeaAttachment(Idea idea, Attachment attachment)
        {
            Idea = idea;
            Attachment = attachment;
        }

        public int IdeaId { get; set; }
        public int AttachmentId { get; set; }

        public virtual Idea Idea { get; set; }
        public virtual Attachment Attachment { get; set; }
    }
}
