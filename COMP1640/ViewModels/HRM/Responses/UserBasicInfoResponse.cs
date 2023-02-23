using Domain;
using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace COMP1640.ViewModels.HRM.Responses
{
    public class UserBasicInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public int RoleId { get; set; }

        public Expression<Func<User, UserBasicInfoResponse>> GetSelection()
        {
            return _ => new UserBasicInfoResponse
            {
                Id = _.Id,
                UserName = _.UserName,
                Email = _.Email,
                Department = _.UserDepartments.Select(_ => _.Department.Name).FirstOrDefault(),
                Role = _.RoleUsers.Select(_ => _.Role.Name).FirstOrDefault(),
                RoleId = _.RoleUsers.Select(_ => _.RoleId).FirstOrDefault(),
                IsActive = !_.LockoutEnabled,
            };
        }
    }
}
