namespace Domain.Interfaces
{
    public interface IAttachmentRepository : IBaseRepository<Attachment>
    {
        Task<Attachment> GetAsync(string fileKey);
    }
}
