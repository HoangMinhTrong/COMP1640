namespace COMP1640.ViewModels.Common;

public class PaginationInfo
{
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int ActualPage { get; set; }
    public int TotalPages { get; set; }
    public bool Previous { get; set; }
    public bool Next { get; set; }
}