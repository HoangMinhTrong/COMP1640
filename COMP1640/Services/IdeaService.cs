using COMP1640.Utils;
using COMP1640.ViewModels.Common;
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
            var categories = await _categoryRepo.GetAllQuery()
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

        
        public async Task<PaginatedList<IdeaIndexItem>> GetIdeaIndexAsync(GetIdeaIndexRequest request)
        {
            var queryable = _ideaRepo.GetAllQuery();
        
            // Filter
            queryable = FilterQuery(request, queryable);
            
            // Sort
            queryable = SortingQuery(request, queryable);
            var totalCount = queryable.Count();
            
            queryable = PaginatedList<Idea>.CreatePangingQueryAsync(queryable, request.PageNumber ?? 1,
                request.PageSize ?? IdeaPagingOption.DefaultPageSize);

            var ideaIndexItems = await queryable
                .Select(new IdeaIndexItem().GetSelection())
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            
            return await PaginatedList<IdeaIndexItem>.GetPagingResult(ideaIndexItems, totalCount, request.PageNumber ?? 1,
                request.PageSize ?? IdeaPagingOption.DefaultPageSize);
        }
        private static IQueryable<Idea> FilterQuery(GetIdeaIndexRequest request, IQueryable<Idea> queryable)
        {
            if (request.FilterOption.HasValue)
                queryable = queryable.Where(idea => idea.CategoryId == request.FilterOption.Value);

            // Check search string
            if (!string.IsNullOrEmpty(request.SearchString))
                request.PageNumber = 1;
            else
                request.SearchString = request.CurrentSearch;

            // Filter by search string
            if (!string.IsNullOrWhiteSpace(request.SearchString))
                queryable = queryable.Where(idea =>
                    idea.Title.ToLower().Contains(request.SearchString.ToLower()));

            return queryable;
        }
        
        private static IQueryable<Idea> SortingQuery(GetIdeaIndexRequest request, IQueryable<Idea> queryable)
        {
            queryable = request.SortOption switch
            {
                // TODO: Implement Views count
                // case IdeaIndexOption.PopularIdeaSort:
                // queryable = queryable.OrderByDescending(x => x.View); 
                // break;
                IdeaIndexOption.ReactionSort => queryable.OrderByDescending(x =>
                    x.Reactions.Count(r => r.Status == ReactionStatusEnum.Like) -
                    x.Reactions.Count(r => r.Status == ReactionStatusEnum.DisLike)),
                
                // TODO: Implement comment model
                // case IdeaIndexOption.LatestCommentSort:
                //     queryable = queryable.OrderBy(x => x.Price);
                //     break;
                IdeaIndexOption.LatestIdeaSort => queryable.OrderByDescending(x => x.CreatedOn),
                _ => queryable.OrderByDescending(x => x.CreatedOn)
            };
            return queryable;
        }
    }
}
