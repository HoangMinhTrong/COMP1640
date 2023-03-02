using System.Linq.Expressions;
using System.Runtime.Serialization;
using COMP1640.ViewModels.Shared.Requests;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.ViewModels.Common;

public class GetIdeaIndexRequest : PagingRequest
{
    public IdeaIndexSortingEnum? SortOption { get; set; } = IdeaIndexSortingEnum.LatestIdea;
    public string SearchString { get; set; } = string.Empty;
    public int? CategoryFilterOption { get; set; }

    public Expression<Func<Domain.Idea, bool>> Filter()
    {
        return _ =>
            (CategoryFilterOption == null || _.CategoryId == CategoryFilterOption)
            &&
            (string.IsNullOrWhiteSpace(SearchString)
             || (EF.Functions.ILike(_.Title, $"%{SearchString}%")
                 || (EF.Functions.ILike(_.Content, $"%{SearchString}%"))));
    }
    
    public Func<IQueryable<Domain.Idea>, IQueryable<Domain.Idea>> Sort()
    {
        switch (SortOption)
        {
            case IdeaIndexSortingEnum.LatestIdea:
                return q => q.OrderByDescending(x => x.CreatedOn);
            case IdeaIndexSortingEnum.MostReactPoint:
                return q => q.OrderByDescending(i =>
                    i.Reactions.Count(r => r.Status == ReactionStatusEnum.Like) -
                    i.Reactions.Count(r => r.Status == ReactionStatusEnum.DisLike));

            // TODO: Sorting by latest comment, post popular
            case IdeaIndexSortingEnum.LatestComment:
            case IdeaIndexSortingEnum.MostPopularIdea:
            default:
                return q => q.OrderByDescending(x => x.CreatedOn);
        }
    }
}

public enum IdeaIndexSortingEnum : int
{
    [EnumMember(Value = "Most favo")]
    MostReactPoint = 1,
    
    [EnumMember(Value = "Latest comment")]
    LatestComment = 2,

    [EnumMember(Value = "Latest idea")]
    LatestIdea = 3,
    
    [EnumMember(Value = "Most popular")]
    MostPopularIdea = 4
}