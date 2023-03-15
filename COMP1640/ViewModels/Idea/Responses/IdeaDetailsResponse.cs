using System.Linq.Expressions;

namespace COMP1640.ViewModels.Idea.Responses
{
    public class IdeaDetailsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public int CategoryId { get; set; }

        public Expression<Func<Domain.Idea, IdeaDetailsResponse>> GetSelection()
        {
            return _ => new IdeaDetailsResponse
            {
                Id = _.Id,
                Title = _.Title,
                Content = _.Content,
                CategoryId = _.CategoryId,
                IsAnonymous = _.IsAnonymous,
            };
        }
    }
}
