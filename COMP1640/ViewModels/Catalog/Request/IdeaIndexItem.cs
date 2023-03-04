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
            Author = authorSelection(_.CreatedByNavigation, _.IsAnonymous),
            TotalComment = _.Comments.Count,
            Views = _.Views
            
        };
    }
}

public class IdeaAuthor
{
    public int Id { get; set; }
    public string UserName { get; set; }
    
    public Expression<Func<User, bool?, IdeaAuthor>> GetSelection()
    {
        return (user, isAnonymous ) =>  isAnonymous != null && isAnonymous == true ? AnonymousAuthor() : new IdeaAuthor
        {
            Id = user.Id,
            UserName = user.UserName,
        };
    }

    private static IdeaAuthor AnonymousAuthor()
    {
        return new IdeaAuthor()
        {
            Id = 0,
            UserName = "Anonymous"
        };
    }
}