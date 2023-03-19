namespace COMP1640.ViewModels.Catalog.Response;

public class AnalysisResponse
{
    public AnalysisResponse(List<AnalysisLabelValueItem<int>> totalIdeasTimelines, List<AnalysisLabelValueItem<int>> totalIdeaDepartments, List<AnalysisLabelValueItem<int>> totalContributorDepartments, ExceptionReport exceptionReport)
    {
        TotalIdeasTimelines = totalIdeasTimelines;
        TotalIdeaDepartments = totalIdeaDepartments;
        TotalContributorDepartments = totalContributorDepartments;
        ExceptionReport = exceptionReport;
    }
    public AnalysisResponse()
    {
    }

    public List<AnalysisLabelValueItem<int>> TotalIdeasTimelines { get; set; }
    public List<AnalysisLabelValueItem<int>> TotalIdeaDepartments { get; set; }
    public List<AnalysisLabelValueItem<int>> TotalContributorDepartments { get; set; } 
    public ExceptionReport ExceptionReport { get; set; }
}

public class ExceptionReport
{
    public ExceptionReport(AnalysisLabelValueItem<int> totalIdeas, AnalysisLabelValueItem<int> totalIdeasWithoutComment, AnalysisLabelValueItem<int> totalAnonymousIdea, AnalysisLabelValueItem<int> totalComment, AnalysisLabelValueItem<int> totalAnonymousComment)
    {
        TotalIdeas = totalIdeas;
        TotalIdeasWithoutComment = totalIdeasWithoutComment;
        TotalAnonymousIdea = totalAnonymousIdea;
        TotalComment = totalComment;
        TotalAnonymousComment = totalAnonymousComment;
    }
    public AnalysisLabelValueItem<int> TotalIdeas { get; set; }
    public AnalysisLabelValueItem<int> TotalIdeasWithoutComment { get; set; } 
    public AnalysisLabelValueItem<int> TotalAnonymousIdea { get; set; }
    public AnalysisLabelValueItem<int> TotalComment { get; set; }
    public AnalysisLabelValueItem<int> TotalAnonymousComment { get; set; } 
    
}
public class AnalysisLabelValueItem<T>
{
    public string Label { get; set; }
    public T Value { get; set; }

}