#nullable disable

using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HRMService(IUserRepository userRepo, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
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
                    Role = _.Roles.Select(_ => (RoleTypeEnum)_.Id).FirstOrDefault(),
                })
                .ToListAsync();
        }

        public async Task<UserProfileResponse> GetUserInfoDetailsAsync(int userId)
        {
/*            var objId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            userId = Int32.Parse(objId);*/
            return await _userRepo
                .GetById(userId)
                .Select(_ => new UserProfileResponse
                {
                    Id = _.Id,
                    UserName = _.UserName,
                    Email = _.Email,
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
