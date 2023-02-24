#nullable disable

using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using Domain;
using Domain.DomainEvents;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Utilities.Identity.Interfaces;

namespace COMP1640.Services
{
    public class HRMService
    {
        private readonly IUserRepository _userRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserInfo _currentUser;
        private readonly IServiceProvider _serviceProvider;

        public HRMService(IUserRepository userRepo
            , IUnitOfWork unitOfWork
            , IDepartmentRepository departmentRepo
            , IRoleRepository roleRepo
            , ICurrentUserInfo currentUser
            , IServiceProvider serviceProvider)
        {
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
            _departmentRepo = departmentRepo;
            _roleRepo = roleRepo;
            _currentUser = currentUser;
            _serviceProvider = serviceProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<IPagedList<UserBasicInfoResponse>> GetListUserAsync(GetListUserRequest request)
        {
            return  _userRepo
                .GetQuery(request.Filter())
                .Select(new UserBasicInfoResponse().GetSelection())
                .OrderBy(_ => _.RoleId)
                .ThenBy(_ => _.Id)
                .ToPagedList(request.PageNo, request.PageSize);
        }

        public async Task<UserDetailInfoResponse> GetUserInfoDetailsAsync(int userId)
        {
            return await _userRepo
                .GetById(userId)
                .Select(new UserDetailInfoResponse().GetSelection())
                .FirstOrDefaultAsync();
        }
        
        public async Task<UserDetailInfoResponse> GetPersonalProfileAsync()
        {
            var userId = _currentUser.Id;
            return await _userRepo
                .GetById(userId)
                .Select(new UserDetailInfoResponse().GetSelection())
                .FirstOrDefaultAsync();
        }
        public async Task<bool> CreateUserAsync(CreateUserRequest request)
        {
            var existedEmail = await _userRepo.AnyAsync(_ => _.Email == request.Email);
            if (existedEmail)
                return false;
            
            var department = await _departmentRepo.GetAsync(request.DepartmentId);
            if (department == null)
                return false;
            
            var role = await _roleRepo.GetAsync(request.Role);
            if (role == null || role.Id == (int)RoleTypeEnum.Admin)
                return false;

            var isDepartmentQARole = role.Id == (int)RoleTypeEnum.DepartmentQA;
            if (isDepartmentQARole)
            {
                if(department.QaCoordinatorId != null) return false;
            }

            var user = new User(request.Email
                , request.Birthday
                , request.Gender
                , role
                , department);
            
            await _userRepo.InsertAsync(user);
            if (isDepartmentQARole)
            {
                department.UpdateQaCoordinator(user.Id);
            }
            await _unitOfWork.SaveChangesAsync();

            await HandleSendMailOnCreateUserAsync(user);
            return true;
        }

        public async Task<bool> EditUserInfoAsync(int id, EditUserRequest request)
        {
            var user = await _userRepo.GetById(id).FirstOrDefaultAsync();
            if (user == null)
                return false;

            user.EditInfo(request.Email
                , request.RoleId
                , request.DepartmentId
                , request.Gender
                , request.Birthday);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActivateAsync(int id)
        {
            var user = await _userRepo.GetById(id).FirstOrDefaultAsync();
            if (user == null)
                return false;

            user.ToggleActivate();
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetById(id).FirstOrDefaultAsync();
            if (user == null)
                return false;

            await _userRepo.DeleteAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<SelectPropertyForCreateAccountResponse> GetRolesForCreateAccountAsync()
        {
            var roles = await _roleRepo
                .GetQuery(r => r.Id != (int)RoleTypeEnum.Admin)
                .Select(_ => new DropDownListBaseResponse()
                {
                    Id = _.Id,
                    Name = _.Name
                })
                .AsNoTracking()
                .ToListAsync();

            var departments = await _departmentRepo.GetAllQuery()
                .Select(d => new DropDownListBaseResponse()
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return new SelectPropertyForCreateAccountResponse()
            {
                Roles = roles,
                Departments = departments
            };
        }


        #region Send Mail
        private async Task HandleSendMailOnCreateUserAsync(User user)
        {
            var mediator = _serviceProvider.GetService<IMediator>();
            if (mediator != null)
                 await mediator.Publish(new CreateUserDomainEvent(user));
        }
        #endregion
    }
}
