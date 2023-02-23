using COMP1640.ViewModels.Idea.Requests;
using COMP1640.ViewModels.Idea.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services
{
    public class IdeaService
    {
        private readonly IIdeaRepository _ideaRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public IdeaService(IIdeaRepository ideaRepo, IUnitOfWork unitOfWork, ICategoryRepository categoryRepo)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
            _categoryRepo = categoryRepo;
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
                    1
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
    }
}
