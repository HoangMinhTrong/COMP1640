using System.Linq.Expressions;

namespace COMP1640.ViewModels.Idea.Requests
{
    public class GetListIdeaRequest
    {
        public string SearchTerm { get; set; }
        public int MyProperty { get; set; }

        public Expression<Func<Domain.Idea, bool>> Filter()
        {
            return _ =>
                string.IsNullOrEmpty(SearchTerm)
                        ? true
                         : (_.Title.Contains(SearchTerm)
                            || _.Content.Contains(SearchTerm));
        }
    }
}