using COMP1640.ViewModels.Shared.Requests;
using Domain;
using System.Linq.Expressions;

namespace COMP1640.ViewModels.Category.Requests
{
    public class GetListCategoryRequest
    {
        public string SearchTerm { get; set; }

        public Expression<Func<Domain.Category, bool>> Filter()
        {
            return _ =>
                string.IsNullOrEmpty(SearchTerm)
                        ? true
                         : (_.Name.Contains(SearchTerm));

        }
    }
}
