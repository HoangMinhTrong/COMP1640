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
    public int? DepartmentFilterOption { get; set; }


    public Expression<Func<Domain.Idea, bool>> Filter(int? createdById = null
        , int? departmentId = null
        , IdeaStatusEnum status = IdeaStatusEnum.Approved)
    {
        return _ =>
            (departmentId == null || _.DepartmentId == departmentId)
            &&
            (createdById == null || _.CreatedBy == createdById)
            &&
            (CategoryFilterOption == null || _.CategoryId == CategoryFilterOption)
            &&
            (DepartmentFilterOption == null || _.DepartmentId == DepartmentFilterOption)
            &&
            (
                string.IsNullOrWhiteSpace(SearchString)
                    || (EF.Functions.ILike(_.Title, $"%{SearchString}%")
                    || (EF.Functions.ILike(_.Content, $"%{SearchString}%")))
            )
            && !_.IsDeactive 
            && !_.IsDeleted
            && (createdById.HasValue ? true : _.Status == status);
    }

    public Func<IQueryable<Domain.Idea>, IQueryable<Domain.Idea>> Sort()
    {
        return SortOption switch
        {
            IdeaIndexSortingEnum.LatestIdea => q => q.OrderByDescending(i => i.CreatedOn),
            IdeaIndexSortingEnum.MostPopularPoint => q => q.OrderByDescending(i =>
                i.Reactions.Count(r => r.Status == ReactionStatusEnum.Like) -
                i.Reactions.Count(r => r.Status == ReactionStatusEnum.DisLike)),
            IdeaIndexSortingEnum.LatestComment => q => q.OrderByDescending(i => i.Comments.Any())
                .ThenByDescending(i => i.Comments.OrderByDescending(c => c.Id).FirstOrDefault()),
            IdeaIndexSortingEnum.MostViewIdea => q => q.OrderByDescending(i => i.Views),
            _ => q => q.OrderByDescending(i => i.CreatedOn)
        };
    }
}

public enum IdeaIndexSortingEnum : int
{
    [EnumMember(Value = "Most popular")]
    MostPopularPoint = 1,

    [EnumMember(Value = "Latest comment")]
    LatestComment = 2,

    [EnumMember(Value = "Latest idea")]
    LatestIdea = 3,

    [EnumMember(Value = "Most views")]
    MostViewIdea = 4
}