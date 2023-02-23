using COMP1640.ViewModels.Shared.Requests;
using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class GetListUserRequest : PagingRequest
    {
        public string SearchTerm { get; set; }

        public Expression<Func<User, bool>> Filter()
        {
            return _ => !_.RoleUsers.Any(_ => _.RoleId == (int)RoleTypeEnum.Admin)
                    &&
                    (
                        string.IsNullOrEmpty(SearchTerm)
                        ? true
                        : (_.Email.ToLower().Contains(SearchTerm) 
                            || _.UserName.ToLower().Contains(SearchTerm) 
                            || _.RoleUsers.Any(r => r.Role.Name.ToLower().Contains(SearchTerm))
                            || _.UserDepartments.Any(d => d.Department.Name.ToLower().Contains(SearchTerm))
                          )
                    );
        }
    }
}
