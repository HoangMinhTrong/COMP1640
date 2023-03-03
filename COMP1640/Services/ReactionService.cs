using COMP1640.ViewModels.Reaction.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CurrentStatusReactionReponse> CheckStatusBeforeAction(int ideaId)
        {
            var userId = _current.Id;
            var currentReaction = await _reactionRepo
                .GetByUserAndIdeaAsync(ideaId, userId);
            
            if (currentReaction == null) return new CurrentStatusReactionReponse { status = 0 };
            //return (int)currentReaction.Status;
            return new CurrentStatusReactionReponse { status = (int)currentReaction.Status };
        }

        public async Task<bool> DeleteThumbUpAsync(int ideaId)
        {
            var userId = _current.Id;
            var reaction = await _reactionRepo
                .GetByUserAndIdeaAsync(ideaId, userId);

            if (reaction == null)
                return false;

            await _reactionRepo.DeleteAsync(reaction);
            await _unitOfWork.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteThumbDownAsync(int ideaId)
        {
            var userId = _current.Id;
            var reaction = await _reactionRepo
                .GetByUserAndIdeaAsync(ideaId, userId);

            if (reaction == null)
                return false;

            await _reactionRepo.DeleteAsync(reaction);
            await _unitOfWork.SaveChangesAsync();

            return true;

        }
        
        public async Task<bool> HandleReaction(int ideaId, ReactionStatusEnum statusEnum)
        {
            var existedReaction = await  _reactionRepo.GetAsync(r => r.UserId == _current.Id && r.IdeaId == ideaId);
            if (existedReaction == null)
            {
                // TODO: Create record
                return true;
            }

            // Check 
            if (statusEnum == existedReaction.Status)
            {
                // TODO: Delete record
                return true;
            }

            existedReaction.Status = statusEnum;
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<Tuple<int, int>> GetIdeaReaction(int ideaId)
        {
            var reactions = await _reactionRepo.GetQuery(r => r.IdeaId == ideaId).ToListAsync();

            return new Tuple<int, int>
            (reactions.Count(r => r.Status == ReactionStatusEnum.Like),
                reactions.Count(r => r.Status == ReactionStatusEnum.DisLike));
        }
        
    }
}
