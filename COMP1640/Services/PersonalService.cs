#nullable disable

using COMP1640.ViewModels.HRM.Requests;
using COMP1640.ViewModels.HRM.Responses;
using COMP1640.ViewModels.PersonalDetail.Requests;
using COMP1640.ViewModels.PersonalDetail.Responses;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using Utilities.Identity.Interfaces;

namespace COMP1640.Services
{
    public class PersonalService
    {
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserInfo _currentUser;
        private readonly IUnitOfWork _unitOfWork;


        public PersonalService(IUserRepository userRepo, IUnitOfWork unitOfWork,
            IRoleRepository roleRepo, ICurrentUserInfo currentUser)
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


      




    }
}
