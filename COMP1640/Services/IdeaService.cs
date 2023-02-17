using COMP1640.ViewModels.Idea.Requests;
using Domain.Interfaces;

namespace COMP1640.Services
{
    public class IdeaService
    {
        private readonly IIdeaRepository _ideaRepo;
        private readonly IUnitOfWork _unitOfWork;

        public IdeaService(IIdeaRepository ideaRepo, IUnitOfWork unitOfWork)
        {
            _ideaRepo = ideaRepo;
            _unitOfWork = unitOfWork;
        }

        public Task CreateIdeaAsync(CreateIdeaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
