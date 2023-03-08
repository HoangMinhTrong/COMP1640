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
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserInfo _current;
        private readonly IServiceProvider _serviceProvider;
        private readonly AttachmentService attachmentService;

        public IdeaService(
            IIdeaRepository ideaRepo,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepo,
            ICurrentUserInfo current,
            IServiceProvider serviceProvider,
            AttachmentService attachmentService)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
            _current = current;
            _serviceProvider = serviceProvider;
            this.attachmentService = attachmentService;
        }

        public async Task<bool> CreateIdeaAsync(CreateIdeaRequest request)
        {
            var category = await _categoryRepo.GetAsync(request.CategoryId);
            if (category == null)
                return false;

            var idea = new Idea
                (
                    request.Title,
                    request.Content,
                    request.IsAnonymous,
                    request.CategoryId,
                    1,
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
                    Name = c.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginatedList<IdeaIndexItem>> GetIdeaIndexAsync(GetIdeaIndexRequest request, int? userId = null)
        {

            var queryable = request.Sort()(_ideaRepo.GetQuery(request.Filter(userId)));

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
                .Select(new GetIdeaDetailResponse().GetSelection())
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

            existIdea.EditInfo(
                request.Title, 
                request.Content, 
                request.IsAnonymous, 
                request.CategoryId
            );

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteIdeaAsync(int ideaId)
        {
            var existIdea = await _ideaRepo.GetById(ideaId).FirstOrDefaultAsync();
            if (existIdea == null) return false;

            existIdea.ToggleSoftDelete();

            await _unitOfWork.SaveChangesAsync();
            return true;
        }


        public async Task<List<IdeaDetailsResponse>> GetDeletedIdeaAsync()
        {
            var deletedIdeas = await _ideaRepo
                .GetDeleted()
                .Select(new IdeaDetailsResponse().GetSelection())
                .ToListAsync();

            return deletedIdeas;
        }

        
    }
}
