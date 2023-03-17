using System.Linq.Expressions;

namespace COMP1640.ViewModels.Catalog.Response;

public class IdeaHistoryResponse
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }

    public DateTime CreatedOn { get; set; }

    public Expression<Func<Domain.IdeaHistory, IdeaHistoryResponse>> GetSelection()
    {
        return _ => new IdeaHistoryResponse()
        {
            Title = _.Title,
            Content = _.Content,
            Category = _.Category.Name,
            CreatedOn = _.UpdatedOn
        };
    }
}