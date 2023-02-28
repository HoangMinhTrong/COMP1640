using Domain;
using Domain.Interfaces;
using Utilities.Identity.Interfaces;

namespace COMP1640.Services
{
    public class ReactionService
    {
        private readonly IReactionRepository _reactionRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserInfo _current;

        public ReactionService(
            IReactionRepository reactionRepo,
            IUnitOfWork unitOfWork, 
            ICurrentUserInfo current)
        {
            _reactionRepo = reactionRepo;
            _unitOfWork = unitOfWork;
            _current = current;
        }

        public async Task<bool> CreateThumbUpAsync(int ideaId)
        {
            var userId = _current.Id;
            var reaction = new Reaction
                (
                    ideaId,
                    userId,
                    ReactionStatusEnum.Like
                );

            await _reactionRepo.InsertAsync(reaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateThumbDownAsync(int ideaId)
        {
            var userId = _current.Id;
            var reaction = new Reaction
                (
                    ideaId,
                    userId,
                    ReactionStatusEnum.DisLike
                );

            await _reactionRepo.InsertAsync(reaction);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
