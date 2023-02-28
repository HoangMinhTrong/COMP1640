﻿using COMP1640.ViewModels.Common;
using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Idea.Responses;
using Domain;
using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
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
        private readonly IServiceProvider _serviceProvider;

        public IdeaService(
            IIdeaRepository ideaRepo,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepo,
            ICurrentUserInfo current
            , IServiceProvider serviceProvider)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _current = current;
            _serviceProvider = serviceProvider;
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
            await HandleSendMailOnCreateIdeaAsync(idea);

            return true;
        }

        public async Task<List<CategoryForCreateIdeaResponse>> GetCategoryForCreateIdeaAsync()
        {
            return await _categoryRepo.GetAllQuery()
                .Select(c => new CategoryForCreateIdeaResponse()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .AsNoTracking()
                .ToListAsync();
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
                    UserRole = _.CreatedByNavigation.RoleUsers.Select(r => r.Role.Name).FirstOrDefault(),
                    LikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.Like).Count(),
                    DislikeCount = _.Reactions.Where(r => r.Status == ReactionStatusEnum.DisLike).Count(),
                    Category = _.Category.Name,
                })
                .ToListAsync();
        }

        
        public async Task<PaginatedList<IdeaIndexItem>> GetIdeaIndexAsync(GetIdeaIndexRequest request)
        {
            var queryable = request.Sort()(_ideaRepo.GetQuery(request.Filter()));
            
            var totalCount = queryable.Count();
            
            queryable = PaginatedList<Idea>.CreatePangingQueryAsync(queryable, request.PageNumber ?? 1,
                request.PageSize);

            var ideaIndexItems = await queryable
                .Select(new IdeaIndexItem().GetSelection())
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
            
            return await PaginatedList<IdeaIndexItem>.GetPagingResult(ideaIndexItems, totalCount, request.PageNumber ?? 1,
                request.PageSize);
        }

        #region Send Mail
        private async Task HandleSendMailOnCreateIdeaAsync(Idea idea)
        {
            var mediator = _serviceProvider.GetService<IMediator>();
            if (mediator != null)
                await mediator.Publish(new CreateIdeaDomainEvent(idea));
        }
        #endregion

    }
}
