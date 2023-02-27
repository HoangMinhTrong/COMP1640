using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Idea.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utilities.Identity.Interfaces;

namespace COMP1640.Services
{
    public class IdeaService
    {
        private readonly IIdeaRepository _ideaRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserInfo _current;
        public IdeaService(
            IIdeaRepository ideaRepo, 
            IUnitOfWork unitOfWork, 
            ICategoryRepository categoryRepo,
            ICurrentUserInfo current)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _current = current;

        }

        public async Task<bool> CreateIdeaAsync(CreateIdeaRequest request)
        {
            var category = await _categoryRepo.GetAsync(request.CategoryId);
            if (category == null)
                return false;

            var departmentId = _current.DepartmentId;

            var idea = new Idea
                (
                    request.Title,
                    request.Content,
                    request.IsAnonymous,
                    request.CategoryId,
                    1,
                    departmentId
                );

            await _ideaRepo.InsertAsync(idea);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<CategoryForCreateIdeaResponse> GetCategoryForCreateIdeaAsync()
        {
            var categories = await _categoryRepo.GetAll()
                .Select(c => new DropDownListBaseResponse()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return new CategoryForCreateIdeaResponse()
            {
                Categories = categories,
            };
        }


        public async Task<List<IdeaContentResponse>> GetListIdeas(GetListIdeaRequest request)
        {
            return await _ideaRepo
                .GetQuery(request.Filter())
                .Select(_ => new IdeaContentResponse
                {
                    Id = _.Id,
                    Title = _.Title,
                    Content = _.Content,
                    Department = _.Department.Name,
                    CreatedBy = _.CreatedByNavigation.UserName,
                    CreatedOn = _.CreatedOn,
                    IsAnomymous = _.IsAnonymous,
                    UserRole = _.CreatedByNavigation
                    .RoleUsers.Select(r => r.Role.Name).FirstOrDefault(),
                    LikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.Like)
                    .Count(),
                    DislikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.DisLike)
                    .Count(),
                    Category = _.Category.Name,
                })
                .ToListAsync();
        }




    }
}
