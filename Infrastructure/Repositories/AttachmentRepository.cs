using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Attachment> GetAsync(string fileKey)
        {
            return await GetAsync(_ => _.KeyName == fileKey);
        }
    }
}
