#nullable disable

using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services
{
    public class HRMService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public HRMService(IUserRepository userRepo, IUnitOfWork unitOfWork)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserBasicInfoResponse>> GetListUserAsync(GetListUserRequest request)
        {
            return await _userRepo
                .GetQuery(request.Filter())
                .Select(_ => new UserBasicInfoResponse
                {
                    Id = _.Id,
                    UserName = _.UserName,
                    Email = _.Email,
                    Role = _.Role.Select(_ => (RoleTypeEnum)_.Id).FirstOrDefault(),
                })
                .ToListAsync();
        }

        public async Task<UserBasicInfoResponse> GetUserInfoDetailsAsync(int userId)
        {
            return await _userRepo
                .GetById(userId)
                .Select(_ => new UserBasicInfoResponse
                {
                    Id = _.Id,
                    UserName = _.UserName,
                    Email = _.Email,
                    Role = _.Role.Select(_ => (RoleTypeEnum)_.Id).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();
        }

        public Task CreateUserAsync(CreateUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task EditUserInfoAsync(EditUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
