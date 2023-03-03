using Domain;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    internal class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
