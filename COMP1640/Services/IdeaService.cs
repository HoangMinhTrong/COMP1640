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

            await _ideaRepo.InsertAsync(idea, false);
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
        public async Task<IdeaDetailsResponse> GetIdeaByIdAsync(int ideaId)
        {
            return await _ideaRepo
                 .GetById(ideaId)
                .Select(new IdeaDetailsResponse().GetSelection())
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditIdeaAsync(EditIdeaRequest request)
        {
            var existIdea = await _ideaRepo.GetAsync(_ => _.Id == request.Id);
            if (existIdea == null) return false;

            existIdea.EditInfo(request.Title, request.Content, request.IsAnonymous, request.CategoryId);

            await _unitOfWork.SaveChangesAsync();
            return true;

        }
    }
}
