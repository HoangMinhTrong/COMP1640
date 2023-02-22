using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.HRM.Responses
{
    public class UserBasicInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public Expression<Func<User, UserBasicInfoResponse>> GetSelection()
        {
            return _ => new UserBasicInfoResponse
            {
                Id = _.Id,
                UserName = _.UserName,
                Email = _.Email,
                Role = _.RoleUsers.Select(_ => _.Role.Name).FirstOrDefault(),
            };
        }
    }
}
