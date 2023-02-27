using System.Linq.Expressions;
using Domain;

namespace COMP1640.ViewModels.Common;

public class IdeaIndexItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int ThumbsUp { get; set; }
    public int ThumbsDown { get; set; }
    public int Views { get; set; }
    public int TotalComment { get; set; }
    public IdeaAuthor Author { get; set; }


    public Expression<Func<Domain.Idea, IdeaIndexItem>> GetSelection()
    {
        var authorSelection = new IdeaAuthor().GetSelection().Compile();
        return _ => new IdeaIndexItem
        {
            Id = _.Id,
            Title = _.Title,
            Content = _.Content,
            CategoryId = _.CategoryId,
            Category = _.Category.Name,
            CreatedOn = _.CreatedOn,
            UpdatedOn = _.CreatedOn, // TODO: Add field updated on
            ThumbsDown = _.Reactions.Count(r => r.Status == ReactionStatusEnum.DisLike),
            ThumbsUp = _.Reactions.Count(r => r.Status == ReactionStatusEnum.Like),
            Author = authorSelection(_.CreatedByNavigation),
            // Comments = _.Comment TODO: Add total comments and views count
            
        };
    }
}

public class IdeaAuthor
{
    public int Id { get; set; }
    public string UserName { get; set; }
    
    public Expression<Func<User, IdeaAuthor>> GetSelection()
    {
        return _ => new IdeaAuthor
        {
           Id = _.Id,
           UserName = _.UserName,
        };
    }
}