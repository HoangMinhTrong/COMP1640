#nullable disable

using COMP1640.ViewModels.PersonalDetail;
using COMP1640.ViewModels.PersonalDetail.Responses;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utilities.Identity.Interfaces;

namespace COMP1640.Services
{
    public class PersonalService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserInfo _currentUser;
        private readonly IUnitOfWork _unitOfWork;

        public PersonalService(IUserRepository userRepo
            , IUnitOfWork unitOfWork
            , ICurrentUserInfo currentUser)
        {
            _userRepo = userRepo;
            _currentUser = currentUser;
            _unitOfWork = unitOfWork;
        }

        public async Task<PersonalProfileInfoResponse> GetProfileDetailsAsync()
        {
            var userId = _currentUser.Id;

            return await _userRepo
                .GetById(userId)
                .Select(new PersonalProfileInfoResponse().GetSelection())
                .FirstOrDefaultAsync();
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.NewPassword) || (request.NewPassword != request.ConfirmPassword))
                return;

            var user = await _userRepo.GetById(_currentUser.Id)
                .FirstOrDefaultAsync();
            user.ChangePassword(request.NewPassword);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
