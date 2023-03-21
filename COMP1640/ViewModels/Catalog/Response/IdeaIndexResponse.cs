using COMP1640.ViewModels.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace COMP1640.ViewModels.Catalog.Response;

public class IdeaIndexResponse
{
    public IEnumerable<IdeaIndexItem> IdeaIndexItems { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> Departments { get; set; }
    public IEnumerable<SelectListItem> SortOptionPicklist { get; set; }

    public int? CurrentCategoryFilter { get; set; }
    public int? CurrentDepartmentFilter { get; set; }
    public IdeaIndexSortingEnum? CurrentSort { get; set; }
    public string CurrentSearchString { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}