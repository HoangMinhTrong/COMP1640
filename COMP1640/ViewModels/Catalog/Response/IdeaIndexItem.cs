using System.Linq.Expressions;
using Domain;

namespace COMP1640.ViewModels.Catalog.Response;

public class IdeaIndexItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int CategoryId { get; set; }
    public string Category { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public IdeaStatusEnum Status { get; set; }
    public int ThumbsUp { get; set; }
    public int ThumbsDown { get; set; }
    public int Views { get; set; }
    public int TotalComment { get; set; }
    public bool IsAnonymous { get; set; }
    public IdeaAuthor? Author { get; set; }
    public UserReaction? UserReacted { get; set; }

    public Expression<Func<Domain.Idea, IdeaIndexItem>> GetSelection(int userId)
    {
        var userReactionSelection = new UserReaction().GetSelection().Compile();

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
            Author = _.IsAnonymous ? null : new IdeaAuthor
            {
                Id = _.CreatedByNavigation.Id,
                UserName = _.CreatedByNavigation.UserName,
            },
            TotalComment = _.Comments.Count,
            Views = _.Views,
            IsAnonymous = _.IsAnonymous,
            Status = _.Status,
            UserReacted = userReactionSelection(_.Reactions.FirstOrDefault(r => r.UserId == userId && r.IdeaId == _.Id)),
        };
    }
}

public class IdeaAuthor
{
    public int Id { get; set; }
    public string UserName { get; set; }
}

public class UserReaction
{
    public int Id { get; set; }
    public ReactionStatusEnum Status { get; set; }

    public Expression<Func<Domain.Reaction?, UserReaction?>> GetSelection()
    {
        return _ => _ == null ? null : new UserReaction
        {
            Id = _.Id,
            Status = _.Status,
        };
    }
}