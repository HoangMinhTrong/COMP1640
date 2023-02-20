using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.HRM.Responses
{
    public class UserDetailInfoResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int? Gender { get; set; }
        public DateTime? Birthday { get; set; }

        public Expression<Func<User, UserDetailInfoResponse>> GetSelection()
        {
            return _ => new UserDetailInfoResponse
            {
                Id = _.Id,
                UserName = _.UserName,
                Email = _.Email,
                RoleId = _.RoleUsers.Select(_ => _.RoleId).FirstOrDefault(),
                DepartmentId = _.UserDepartments.Select(_ => _.DepartmentId).FirstOrDefault(),
                Gender = (int)_.Gender,
                Birthday =  _.Birthday
            };
        }
    }
}
