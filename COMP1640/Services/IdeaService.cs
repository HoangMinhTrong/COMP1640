﻿using COMP1640.ViewModels.Catalog.Response;
using COMP1640.ViewModels.Common;
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
        private readonly IIdeaHistoryRepository _ideaHistoryRepository;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserInfo _current;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAcademicYearRepository _academicYearRepo;
        private readonly AttachmentService attachmentService;

        public IdeaService(
            IIdeaRepository ideaRepo,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepo,
            ICurrentUserInfo current,
            IServiceProvider serviceProvider,
            AttachmentService attachmentService,
            IIdeaHistoryRepository ideaHistoryRepository, 
            IAcademicYearRepository cademicYearRepo)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _current = current;
            _serviceProvider = serviceProvider;
            this.attachmentService = attachmentService;
            _ideaHistoryRepository = ideaHistoryRepository;
            _academicYearRepo = cademicYearRepo;
        }

        public async Task<bool> CreateIdeaAsync(CreateIdeaRequest request)
        {
            var category = await _categoryRepo.GetAsync(request.CategoryId);
            if (category == null)
                return false;

            var currentAcademicYear = await _academicYearRepo.GetCurrentAsync();
            if (currentAcademicYear == null)
                return false;

            var idea = new Idea
                (
                    request.Title,
                    request.Content,
                    request.IsAnonymous,
                    category.Id,
                    currentAcademicYear.Id,
                    _current.DepartmentId
                );

            if (request?.Formfiles.Count > 0)
            {
                var attachments = await attachmentService.UploadListAsync(request.Formfiles);
                foreach (var attachment in attachments)
                {
                    idea.AddAttachment(attachment);
                }
            }

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
                    Name = c.Name,
                    IsDeleted = c.IsDeleted
                }).Where(x => !x.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginatedList<IdeaIndexItem>> GetIdeaIndexAsync(GetIdeaIndexRequest request
            , int? userId = null
            , int? departmentId = null
            , IdeaStatusEnum status = IdeaStatusEnum.Approved)
        {
            var queryable = request.Sort()(_ideaRepo.GetQuery(request.Filter(userId, departmentId, status)));

            var totalCount = queryable.Count();

            queryable = PaginatedList<Idea>.CreatePangingQueryAsync(queryable, request.PageNo,
                request.PageSize);

            var ideaIndexItems = await queryable
                .Select(new IdeaIndexItem().GetSelection(_current.Id))
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            return await PaginatedList<IdeaIndexItem>.GetPagingResult(ideaIndexItems, totalCount, request.PageNo,
                request.PageSize);
        }

        public async Task<PaginatedList<IdeaIndexItem>> GetPersonalIdeaIndexAsync(GetIdeaIndexRequest request, int? userId = null)
        {
            return await GetIdeaIndexAsync(request, _current.Id);
        }

        #region Send Mail
        private async Task HandleSendMailOnCreateIdeaAsync(Idea idea)
        {
            var mediator = _serviceProvider.GetService<IMediator>();
            if (mediator != null)
                await mediator.Publish(new CreateIdeaDomainEvent(idea));
        }
        #endregion

        public async Task<GetIdeaDetailResponse> GetIdeaDetailsAsync(int ideaId)
        {
            var idea = await _ideaRepo
                .GetById(ideaId)
                .Select(new GetIdeaDetailResponse().GetSelection(_current.Id))
                .FirstOrDefaultAsync();

            if (idea != null)
                idea.Attachments = await attachmentService.GetAttachmentsAsync(idea.Id);

            return idea;
        }
        public async Task<IdeaDetailsResponse> GetIdeaByIdAsync(int ideaId)
        {
            var idea = await _ideaRepo
                .GetById(ideaId)
                .Select(new IdeaDetailsResponse().GetSelection())
                .FirstOrDefaultAsync();
            return idea;
        }

        public async Task<bool> EditIdeaAsync(int ideaId, EditIdeaRequest request)
        {
            var existIdea = await _ideaRepo.GetById(ideaId).FirstOrDefaultAsync();
            if (existIdea == null) return false;

            // Copy current version as new history record
            await _ideaHistoryRepository.InsertAsync(new IdeaHistory(existIdea));

            existIdea.EditInfo(
                request.Title,
                request.Content,
                request.IsAnonymous,
                request.CategoryId
            );

            if (request?.Formfiles.Count > 0)
            {
                var existedAttachs = existIdea.IdeaAttachments.Select(_ => _.Attachment).ToList();
                if (existedAttachs.Any())
                    await attachmentService.DeleteListAsync(existedAttachs);

                var attachments = await attachmentService.UploadListAsync(request.Formfiles);
                foreach (var attachment in attachments)
                {
                    existIdea.AddAttachment(attachment);
                }
            }
            else
            {
                var existedAttachs = existIdea.IdeaAttachments.Select(_ => _.Attachment).ToList();
                if (existedAttachs.Any())
                    await attachmentService.DeleteListAsync(existedAttachs);
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleDeactiveIdeaAsync(int ideaId)
        {
            var existIdea = await _ideaRepo.GetById(ideaId).FirstOrDefaultAsync();
            if (existIdea == null) return false;

            existIdea.ToggleIsDeactive();

            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<List<IdeaDetailsResponse>> GetDeactiveIdeaAsync()
        {
            var deletedIdeas = await _ideaRepo
                .GetDeactive(_current.Id)
                .Select(new IdeaDetailsResponse().GetSelection())
                .ToListAsync();

            return deletedIdeas;
        }

        public async Task<bool> SoftDeleteIdeaAsync(int id)
        {
            var idea = await _ideaRepo.GetById(id).FirstOrDefaultAsync();
            if (idea == null)
                return false;

            idea.SoftDelete();
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IList<IdeaHistoryResponse>?> GetIdeaHistoriesAsync(int ideaId)
        {
            return await _ideaHistoryRepository.GetQuery(_ => _.IdeaId == ideaId)
                .OrderByDescending(_ => _.RealCreatedOn)
                .Select(new IdeaHistoryResponse().GetSelection())
                .ToListAsync();
        }
        
        public async Task IncreasesViewAsync(int ideaId)
        {
            var idea = await _ideaRepo.GetAsync(i => i.Id == ideaId);
            if(idea.CreatedBy == _current.Id ) return;
            
            idea.IncreasesView();
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ApproveAsync(int ideaId)
        {
            var idea = await _ideaRepo.GetAsync(ideaId);
            if(idea == null) return;

            idea.UpdateStatus(IdeaStatusEnum.Approved);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectAsync(int ideaId)
        {
            var idea = await _ideaRepo.GetAsync(ideaId);
            if (idea == null) return;

            idea.UpdateStatus(IdeaStatusEnum.Rejected);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
