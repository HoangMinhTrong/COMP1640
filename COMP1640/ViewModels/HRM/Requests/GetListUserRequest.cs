using COMP1640.ViewModels.HRM.Responses;
using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.HRM.Requests
{
    public class GetListUserRequest
    {
        public string SearchTerm { get; set; }

        public Expression<Func<User, bool>> Filter()
        {
            return _ =>
                    string.IsNullOrEmpty(SearchTerm)
                    ? true
                    : (_.Email.Contains(SearchTerm) || _.UserName.Contains(SearchTerm));
        }
    }
}
