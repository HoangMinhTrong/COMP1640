namespace COMP1640.ViewModels.Common;

public class GetIdeaIndexRequest
{
    public string SortOption { get; set; } = String.Empty;
    public string CurrentSearch { get; set; } = String.Empty;
    public string SearchString { get; set; } = String.Empty;
    public int? FilterOption { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    
}