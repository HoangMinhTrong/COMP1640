using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP1640.Utils;

public static class IdeaIndexOption
{
    public const int PageSize = 5;

    public const string ReactionSort = "reactSort";
    public const string ReactionSortDesc = "reactSort_desc";
    public const string ReactionSortName = "Reaction";

    public const string LatestCommentSort = "latestComment";
    public const string LatestCommentSortDesc = "latestComment_desc";
    public const string LatestCommentSortName = "Latest Comment";


    public const string LatestIdeaSort = "latestIdea";
    public const string LatestIdeaSortDesc = "latestIdea_desc";
    public const string LatestIdeaSortName = "Latest Idea";


    public const string PopularIdeaSort = "popular";
    public const string PopularIdeaSortDesc = "popular_desc";
    public const string PopularIdeaSortName = "Must popular";


    public static readonly List<SelectListItem> SortOptionPicklist = new()
    {
        new(ReactionSortName, ReactionSort),
        new(PopularIdeaSortName, PopularIdeaSort),
        new(LatestCommentSortName, LatestCommentSort),
        new(LatestIdeaSortName, LatestIdeaSort)
    };
}