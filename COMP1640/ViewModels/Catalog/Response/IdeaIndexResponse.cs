using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP1640.ViewModels.Common;

public class IdeaIndexResponse
{
    public IEnumerable<IdeaIndexItem> IdeaIndexItems { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> Departments { get; set; }
    public IEnumerable<SelectListItem> SortOptionPicklist { get; set; }

    public int? CurrentCategoryFilter { get; set; }
    public string CurrentSort { get; set; }
    public string CurrentSearchString { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
    public string NameSortParm { get; set; }

    public string ReactionSortParm { get; set; }
    public string PopularSortParm { get; set; }
    public string LatestCommentSortParm { get; set; }
    public string LatestIdeaSortParm { get; set; }
}