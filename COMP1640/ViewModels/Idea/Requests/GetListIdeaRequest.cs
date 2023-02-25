using System.Linq.Expressions;
using Domain.Base;
using COMP1640.ViewModels.Idea.Requests;

namespace COMP1640.ViewModels.Idea.Requests
{
    public class GetListIdeaRequest
    {
        public string SearchTerm { get; set; }

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
