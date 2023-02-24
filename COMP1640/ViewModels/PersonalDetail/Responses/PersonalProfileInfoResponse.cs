using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.PersonalDetail.Responses
{
    public class PersonalProfileInfoResponse
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public int DepartmentId { get; set; }
        public string? Department { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? PhoneNumber { get; set; }



        public Expression<Func<User, PersonalProfileInfoResponse>> GetSelection()
        {
            return _ => new PersonalProfileInfoResponse
            {
                Id = _.Id,
                UserName = _.UserName,
                Email = _.Email,
                RoleId = _.RoleUsers.Select(_ => _.RoleId).FirstOrDefault(),
                Role = _.RoleUsers.Select(_ => _.Role.Name).FirstOrDefault(),
                DepartmentId = _.UserDepartments.Select(_ => _.DepartmentId).FirstOrDefault(),
                Department = _.UserDepartments.Select(_ => _.Department.Name).FirstOrDefault(),
                Gender = _.Gender.ToString(),
                Birthday = _.Birthday,
                PhoneNumber = _.PhoneNumber
            };
        }




    }





}
