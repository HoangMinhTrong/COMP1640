namespace COMP1640.ViewModels.Category.Requests;

public class CreateCategoryRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TenantId { get; set; }
}