using COMP1640.ViewModels.Catalog.Response;
using Domain;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace COMP1640.Services;

public class AnalysisService
{
    private readonly IIdeaRepository _ideaRepository;    
    private readonly ICommentRepository _commentRepository;
    private readonly IDepartmentRepository _departmentRepository;


    private readonly IAcademicYearRepository _academicYearRepository;
    public AnalysisService(IIdeaRepository ideaRepository, IAcademicYearRepository academicYearRepository, IDepartmentRepository departmentRepository, ICommentRepository commentRepository)
    {
        _ideaRepository = ideaRepository;
        _academicYearRepository = academicYearRepository;
        _departmentRepository = departmentRepository;
        _commentRepository = commentRepository;
    }

    public async Task<AnalysisResponse> GetAnalysisAsync(int? academicYearId)
    {
        var academicYear = academicYearId == null 
            ? await _academicYearRepository.GetCurrentAsync()
            : await _academicYearRepository.GetAsync(a => a.Id == academicYearId);

        if (academicYear == null) return new AnalysisResponse();


        var totalIdeasTimelines = await GetTotalIdeasTimelinesAsync(academicYear);
        var totalIdeaDepartments = await GetTotalIdeasDepartmentAsync(academicYear.Id);
        var totalContributorDepartments = await GetTotalContributorsDepartmentAsync();
        var exceptionReport = await GetExceptionsReportAsync(academicYear.Id);
        return new AnalysisResponse(totalIdeasTimelines, totalIdeaDepartments, totalContributorDepartments,
            exceptionReport);

    }

    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalIdeasTimelinesAsync(AcademicYear academicYear)
    {
        var months = Enumerable.Range(0, (academicYear.ClosureDate.Year - academicYear.OpenDate.Year) * 12 + (academicYear.ClosureDate.Month - academicYear.OpenDate.Month) + 1)
            .Select(m => new DateTime(academicYear.OpenDate.Year, academicYear.OpenDate.Month, 1).AddMonths(m))
            .ToList();

        var data = await _ideaRepository.GetAllQuery()
            .Where(i => i.AcademicYearId == academicYear.Id && !i.IsDeleted)
            .GroupBy(x => new { x.CreatedOn.Year, x.CreatedOn.Month })
            .OrderBy(k => k.Key.Year)
            .ThenBy(k => k.Key.Month)
            .Select(x => new { Timeline = x.Key, TotalIdea = x.Count() })
            .ToListAsync();

        return months
            .GroupJoin(data, 
                month => new { month.Year, month.Month },
                dataItem => new { dataItem.Timeline.Year, dataItem.Timeline.Month },
                (month, dataItems) => new { Month = month, DataItems = dataItems })
            .SelectMany(
                x => x.DataItems.DefaultIfEmpty(),
                (month, dataItem) => new AnalysisLabelValueItem<int>
                {
                    Label = month.Month.ToString("MM/yy"),
                    Value = dataItem?.TotalIdea ?? 0
                })
            .ToList();
    }
    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalIdeasDepartmentAsync(int academicYearId)
    {
        return await _departmentRepository.GetAllQuery()
            .Where(d => !d.IsDeleted)
            .Select(d => new AnalysisLabelValueItem<int>
            {
                Label = d.Name,
                Value = d.Ideas.Count(i => i.AcademicYearId == academicYearId)
            })
            .ToListAsync();
    }
    private async Task<List<AnalysisLabelValueItem<int>>> GetTotalContributorsDepartmentAsync()
    
    {
        return await _departmentRepository.GetAllQuery()
            .Where(d => !d.IsDeleted)
            .Select(g => new AnalysisLabelValueItem<int>()
            {
                Label = g.Name,
                Value = g.UserDepartments.Count()
            })
            .ToListAsync();
    }
    
    private async Task<ExceptionReport> GetExceptionsReportAsync(int academicYearId)
    {
        var ideas = await _ideaRepository
            .GetQuery(i => i.AcademicYearId == academicYearId).ToListAsync();
        var comment = ideas.SelectMany(i => i.Comments).ToList();
        var totalIdeas = new AnalysisLabelValueItem<int>()
        {
            Label = "Total Ideas",
            Value = ideas.Count
        };

        var totalIdeaWithoutComment = new AnalysisLabelValueItem<int>()
        {
            Label = "Total ideas without comment",
            Value = ideas.Count(i => !i.Comments.Any())
        };

        var totalAnonymousIdeas = new AnalysisLabelValueItem<int>()
        {
            Label = "Total anonymous ideas",
            Value = ideas.Count(i => i.IsAnonymous)
        };

        var totalComments = new AnalysisLabelValueItem<int>()
        {
            Label = "Total comment",
            Value = comment.Count
        };
        var totalAnonymousComment = new AnalysisLabelValueItem<int>()
        {
            Label = "Total anonymous comment",
            Value = comment.Count(c => c.IsAnonymous)
        };

        return new ExceptionReport(totalIdeas, totalIdeaWithoutComment, totalAnonymousIdeas,
            totalComments, totalAnonymousComment);
    }

}