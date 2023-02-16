#nullable disable

using System.Transactions;
using COMP1640.Constants;
using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services
{
    public class HRMService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<HRMService> _logger;




        public HRMService(IUserRepository userRepo, IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<HRMService> logger)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
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

        public async Task<UserBasicInfoResponse> GetUserInfoDetailsAsync(int userId)
        {
            return await _userRepo
                .GetById(userId)
                .Select(_ => new UserBasicInfoResponse
                {
                    Id = _.Id,
                    UserName = _.UserName,
                    Email = _.Email,
                    Role = _.Roles.Select(_ => (RoleTypeEnum)_.Id).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateUserAsync(CreateUserRequest request)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                IdentityResult result;
                var user = new User()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    Birthday = request.Birthday,
                    Gender = request.Gender,
                };

                result = await _userManager.CreateAsync(user, DefaultUserProperty.DefaultAccountPassword);
                if (!result.Succeeded)
                {
                    _logger.LogInformation($"Failure to create account for {user.Email}.");
                    return false;
                }

                result = await _userManager.AddToRoleAsync(user, request.Role.ToString());

                if (result.Succeeded)
                {                    
                    transaction.Complete();
                    return true;
                }
                
                _logger.LogInformation($"Failure to add user to role {user.Roles.ToString()}.");
                transaction.Dispose();
                return false;
            }
            catch (Exception e)
            {
                _logger.LogInformation($"An exception occurred while creating account for {request.Email}. {e.Message}");
                transaction.Dispose();
                throw;
            }
        }

        public Task EditUserInfoAsync(EditUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<RoleForCreateAccountResponse>> GetRolesForCreateAccountAsync()
        {
            try
            {
                return await _roleManager.Roles
                    .Where(r => r.Name != RoleTypeEnum.Admin.ToString())
                    .Select(_ => new RoleForCreateAccountResponse()
                    {
                        Id = _.Id,
                        Name = _.Name
                    })
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"An exception occurred while getting roles for create an account. {e.Message}");
                throw;
            }
            
        }
    }
}
