using System.Linq.Expressions;

namespace COMP1640.ViewModels.Department.Requests
{
    public class GetListDepartmentRequest
    {
        public string SearchTerm { get; set; }

        public Expression<Func<Domain.Department, bool>> Filter()
        {
            return _ =>
                string.IsNullOrEmpty(SearchTerm) || (_.Name.Contains(SearchTerm));

        }
    }

}
