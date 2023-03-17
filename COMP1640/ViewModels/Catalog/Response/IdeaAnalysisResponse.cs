namespace COMP1640.ViewModels.Catalog.Response;

public class AnalysisResponse
{
    public List<AnalysisLabelValueItem<int>> TotalIdeasTimelines { get; set; } = new();
    public List<AnalysisLabelValueItem<int>> TotalIdeaDepartments { get; set; } = new();
    public List<AnalysisLabelValueItem<int>> TotalContributorDepartments { get; set; } = new();
}

public class AnalysisLabelValueItem<T>
{
    public string Label { get; set; }
    public T Value { get; set; }

}