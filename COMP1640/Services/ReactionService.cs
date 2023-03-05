using COMP1640.ViewModels.Reaction.Requests;
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

        public async Task<ReactionReponse> HandleReactionAsync(ReactRequest request)
        {
            ReactionStatusEnum? statusResult = request.ReactionStatusEnum;
            var existedReaction = await _reactionRepo.GetByUserAndIdeaAsync(request.IdeaId, _current.Id);

           
            if (existedReaction == null)
            {
                await _reactionRepo.InsertAsync(
                    new Reaction
                    {
                        UserId = _current.Id,
                        IdeaId = request.IdeaId,
                        Status = request.ReactionStatusEnum
                    });
            }
            else
            {
                if (request.ReactionStatusEnum == existedReaction.Status)
                {
                    await _reactionRepo.DeleteAsync(existedReaction);
                    statusResult = null;
                }
                existedReaction.Status = request.ReactionStatusEnum;
            }
            await _unitOfWork.SaveChangesAsync();

         
            var reactions = await _reactionRepo.GetQuery(r => r.IdeaId == request.IdeaId).ToListAsync();

            return new ReactionReponse
            {
                Status = statusResult,
                TotalLike = reactions.Count(r => r.Status == ReactionStatusEnum.Like),
                TotalDisLike = reactions.Count(r => r.Status == ReactionStatusEnum.DisLike),
            };
        }
    }
}
